using System;
using System.Collections.Generic;
using Itibsoft.PanelManager;
using R3;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;
using Object = UnityEngine.Object;

namespace Project.Scripts
{
    public sealed class DragAndDropSystem : IInitializable, IDisposable
    {
        private readonly IPanelManager _panelManager;
        private readonly SpellSystem _spellSystem;

        private readonly Dictionary<IDragAndDropInput, CompositeDisposable> _subscriptions = new();
        private readonly List<RaycastResult> _raycastResults = new(32);

        private RectTransform _dragRoot;
        private Canvas _canvas;
        private Camera _uiCamera;
        private GraphicRaycaster _raycaster;
        private EventSystem _eventSystem;

        private DragAndDropItemBase _dragItem;
        private DragAndDropSlot _fromSlot;

        private bool _isSpellBookDuplicate;
        private SpellSlot _spellBookSourceSlot;

        private Vector2 _lastScreenPos;

        private MainUIController _mainUI;

        public DragAndDropSystem(IPanelManager panelManager, SpellSystem spellSystem)
        {
            _panelManager = panelManager;
            _spellSystem = spellSystem;
        }

        public void Initialize()
        {
            _canvas = _panelManager.PanelDispatcher.Canvas;
            _dragRoot = _canvas.GetComponent<RectTransform>();

            if (_dragRoot == null)
            {
                throw new Exception("DragAndDropSystem: drag root is null");
            }

            _raycaster = _canvas.GetComponent<GraphicRaycaster>();
            if (_raycaster == null)
            {
                throw new Exception("DragAndDropSystem: GraphicRaycaster not found on Canvas");
            }

            _eventSystem = EventSystem.current;
            if (_eventSystem == null)
            {
                throw new Exception("DragAndDropSystem: EventSystem.current is null");
            }

            _uiCamera = _canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : _canvas.worldCamera;

            _mainUI = _panelManager.LoadPanel<MainUIController>();
        }

        public void Dispose()
        {
            foreach (var kv in _subscriptions)
            {
                kv.Value.Dispose();
            }

            _subscriptions.Clear();
        }

        public void Register(IDragAndDropInput input)
        {
            if (input == null)
            {
                throw new Exception("DragAndDropSystem: input is null");
            }

            if (_subscriptions.ContainsKey(input))
            {
                return;
            }

            var d = new CompositeDisposable();
            _subscriptions.Add(input, d);

            input.OnBeginDrag.Subscribe(BeginDrag).AddTo(d);
            input.OnDrag.Subscribe(Drag).AddTo(d);
            input.OnEndDrag.Subscribe(_ => EndDrag()).AddTo(d);
        }

        public void Unregister(IDragAndDropInput input)
        {
            if (input == null)
            {
                return;
            }

            if (_subscriptions.TryGetValue(input, out var d))
            {
                d.Dispose();
                _subscriptions.Remove(input);
            }
        }

        private void BeginDrag(DragAndDropItemBase item)
        {
            if (item == null)
            {
                return;
            }

            _isSpellBookDuplicate = false;
            _spellBookSourceSlot = null;

            _fromSlot = item.Slot;

            if (_fromSlot is SpellSlot spellSlot && spellSlot.SpellParentType == SpellParentType.Book)
            {
                var cloneGo = Object.Instantiate(item.gameObject, _dragRoot, false);
                var clone = cloneGo.GetComponent<DragAndDropItemBase>();

                _dragItem = clone;
                _isSpellBookDuplicate = true;
                _spellBookSourceSlot = spellSlot;

                if (_dragItem is SpellItem cloneSpell && item is SpellItem srcSpell)
                {
                    cloneSpell.Spell = srcSpell.Spell;
                    cloneSpell.SpellConfig = srcSpell.SpellConfig;

                    var srcImg = srcSpell.GetComponentInChildren<Image>(true);
                    if (srcImg != null)
                    {
                        cloneSpell.SetIcon(srcImg.sprite);
                    }

                    cloneSpell.CurrentSpellSlot = null;
                }

                _dragItem.SetSlot(null);
            }
            else
            {
                _dragItem = item;
                _dragItem.transform.SetParent(_dragRoot, false);
            }

            _lastScreenPos = Input.mousePosition;
            Drag(_lastScreenPos);
        }


        private void Drag(Vector2 screenPos)
        {
            if (_dragItem == null)
            {
                return;
            }

            _lastScreenPos = screenPos;

            var rt = (RectTransform)_dragItem.transform;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(_dragRoot, screenPos, _uiCamera, out var localPoint);
            rt.anchoredPosition = localPoint;
        }

        private void EndDrag()
        {
            if (_dragItem == null)
            {
                return;
            }

            if (_fromSlot == null)
            {
                ClearDrag();
                return;
            }

            var targetSlot = RaycastSlot(_lastScreenPos);

            if (targetSlot == null)
            {
                DropToNowhere();
                ClearDrag();
                return;
            }

            if (targetSlot == _fromSlot)
            {
                ReturnToFromSlot();
                ClearDrag();
                return;
            }

            if (targetSlot.Kind != _fromSlot.Kind)
            {
                ReturnToFromSlot();
                ClearDrag();
                return;
            }

            if (_fromSlot is InventorySlot fromInv && targetSlot is InventorySlot toInv)
            {
                EndDragInventory(fromInv, toInv);
                return;
            }

            if (_fromSlot is SpellSlot fromSpell && targetSlot is SpellSlot toSpell)
            {
                EndDragSpell(fromSpell, toSpell);
                return;
            }

            EndDragSwap(targetSlot);
        }

        private DragAndDropSlot RaycastSlot(Vector2 screenPos)
        {
            _raycastResults.Clear();

            var ped = new PointerEventData(_eventSystem)
            {
                position = screenPos
            };

            _raycaster.Raycast(ped, _raycastResults);

            for (var i = 0; i < _raycastResults.Count; i++)
            {
                var go = _raycastResults[i].gameObject;
                if (go == null)
                {
                    continue;
                }

                if (_dragItem != null && go.transform.IsChildOf(_dragItem.transform))
                {
                    continue;
                }

                var slot = go.GetComponentInParent<DragAndDropSlot>();
                if (slot != null)
                {
                    return slot;
                }
            }

            return null;
        }

        private void DropToNowhere()
        {
            if (_dragItem == null || _fromSlot == null)
            {
                return;
            }

            if (_fromSlot is SpellSlot fromSpell)
            {
                if (_isSpellBookDuplicate)
                {
                    Object.Destroy(_dragItem.gameObject);
                    return;
                }

                if (fromSpell.SpellParentType == SpellParentType.Book)
                {
                    ReturnToFromSlot();
                    return;
                }

                fromSpell.ClearItem();
                UpdateChosenFromSlot(fromSpell);
                Object.Destroy(_dragItem.gameObject);
                return;
            }

            ReturnToFromSlot();
        }

        private void EndDragInventory(InventorySlot fromSlot, InventorySlot toSlot)
        {
            var invItem = _dragItem as InventoryItem;
            if (invItem == null)
            {
                ReturnToFromSlot();
                ClearDrag();
                return;
            }

            var targetOk = toSlot._inventorySlotType == InventorySlotType.Inventory || toSlot._inventorySlotType == invItem._inventorySlotType;
            if (!targetOk)
            {
                ReturnToFromSlot();
                ClearDrag();
                return;
            }

            if (toSlot.IsContainItem)
            {
                ReturnToFromSlot();
                ClearDrag();
                return;
            }

            fromSlot.ClearItem();
            toSlot.SetItem(invItem);

            var rt = (RectTransform)invItem.transform;
            rt.anchoredPosition = Vector2.zero;

            invItem.CurrentInventorySlot = toSlot;

            ClearDrag();
        }

        private void EndDragSpell(SpellSlot fromSlot, SpellSlot toSlot)
        {
            if (toSlot.SpellParentType == SpellParentType.Book)
            {
                ReturnSpellDuplicateOrBack();
                ClearDrag();
                return;
            }

            var spellItem = _dragItem as SpellItem;
            if (spellItem == null)
            {
                ReturnSpellDuplicateOrBack();
                ClearDrag();
                return;
            }

            if (_isSpellBookDuplicate)
            {
                if (toSlot.IsContainItem)
                {
                    ReturnSpellDuplicateOrBack();
                    ClearDrag();
                    return;
                }

                toSlot.SetItem(spellItem);

                if (spellItem.SpellConfig == null && _spellBookSourceSlot != null)
                {
                    var srcItem = _spellBookSourceSlot.Item;
                    if (srcItem != null)
                    {
                        spellItem.SpellConfig = srcItem.SpellConfig;
                    }
                }

                if (spellItem.Spell == null && spellItem.SpellConfig != null)
                {
                    spellItem.Spell = spellItem.SpellConfig.Type;
                }

                var rt = (RectTransform)spellItem.transform;
                rt.anchoredPosition = Vector2.zero;

                spellItem.CurrentSpellSlot = toSlot;

                _mainUI?.WireSpellItem(spellItem);

                UpdateChosenFromSlot(toSlot);

                ClearDrag();
                return;
            }

            var targetItem = (SpellItem)toSlot.ClearItem();
            var fromItem = (SpellItem)fromSlot.ClearItem();

            toSlot.SetItem(fromItem);
            if (fromItem != null)
            {
                var rtA = (RectTransform)fromItem.transform;
                rtA.anchoredPosition = Vector2.zero;
                fromItem.CurrentSpellSlot = toSlot;
                _mainUI?.WireSpellItem(fromItem);
            }

            fromSlot.SetItem(targetItem);
            if (targetItem != null)
            {
                var rtB = (RectTransform)targetItem.transform;
                rtB.anchoredPosition = Vector2.zero;
                targetItem.CurrentSpellSlot = fromSlot;
                _mainUI?.WireSpellItem(targetItem);
            }

            UpdateChosenFromSlot(fromSlot);
            UpdateChosenFromSlot(toSlot);

            ClearDrag();
        }

        private void UpdateChosenFromSlot(SpellSlot slot)
        {
            if (slot == null)
            {
                return;
            }

            if (slot.SpellParentType == SpellParentType.Book)
            {
                return;
            }

            var index = slot.HotbarIndex;
            if (index < 0)
            {
                return;
            }

            var spellBase = slot.Item != null ? slot.Item.Spell : null;
            _spellSystem.SetChosenSpellByIndex(index, spellBase as SpellBase);
        }

        private void EndDragSwap(DragAndDropSlot targetSlot)
        {
            var targetItem = targetSlot.ClearItem();
            var fromItem = _fromSlot.ClearItem();

            targetSlot.SetItem(fromItem);
            if (fromItem != null)
            {
                var rtA = (RectTransform)fromItem.transform;
                rtA.anchoredPosition = Vector2.zero;

                if (fromItem is SpellItem s0)
                {
                    _mainUI?.WireSpellItem(s0);
                }
            }

            _fromSlot.SetItem(targetItem);
            if (targetItem != null)
            {
                var rtB = (RectTransform)targetItem.transform;
                rtB.anchoredPosition = Vector2.zero;

                if (targetItem is SpellItem s1)
                {
                    _mainUI?.WireSpellItem(s1);
                }
            }

            ClearDrag();
        }

        private void ReturnToFromSlot()
        {
            if (_fromSlot != null && _dragItem != null)
            {
                _fromSlot.SetItem(_dragItem);

                var rt = (RectTransform)_dragItem.transform;
                rt.anchoredPosition = Vector2.zero;
            }
        }

        private void ReturnSpellDuplicateOrBack()
        {
            if (_isSpellBookDuplicate)
            {
                if (_dragItem != null)
                {
                    Object.Destroy(_dragItem.gameObject);
                }

                return;
            }

            ReturnToFromSlot();
        }

        private void ClearDrag()
        {
            _dragItem = null;
            _fromSlot = null;

            _isSpellBookDuplicate = false;
            _spellBookSourceSlot = null;
        }
    }
}
