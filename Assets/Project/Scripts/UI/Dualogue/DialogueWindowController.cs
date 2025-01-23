using DG.Tweening;
using Itibsoft.PanelManager;
using Project.Scripts.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.Scripts
{
    [Panel(PanelType = PanelType.Overlay, Order = 0, AssetId = "DialogueWindow")]
    public class DialogueWindowController : PanelControllerBase<DialogueWindow>
    {
        private const float MOVE_DURATION = 0.8f;
        private const float FADE_DURATION = 0.8f;
        
        private const string END_DIALOGUE_KEY = "EndDialogue";
        
        public NpcTypes CurrentNpcType;
        
        [Inject] private DialogueSystemManager _dialogueSystemManager;

        private TMP_Text _text;
        private TMP_Text _npcName;
        private GameObject _textWindow;

        private RectTransform _rectTransform;
        private Vector2 _startPosition;
        private Vector2 _endPosition;

        private Button _dialogueChangeButton;
        
        protected override void Initialize()
        {
            _text = Panel.Text;
            _textWindow = Panel.TextWindow;
            _startPosition = Panel.StartPosition;
            _endPosition = Panel.EndPosition;
            _npcName = Panel.NpcName;
            _dialogueChangeButton = Panel.DialogueChangeButton;

            _dialogueChangeButton.onClick.AddListener(IncreaseDialogueStep);

            _rectTransform = _textWindow.GetComponent<RectTransform>();
        }

        public void ShowDialogueText()
        {
            if (_dialogueSystemManager.GetCurrentDialogue(CurrentNpcType) == _dialogueSystemManager.EndDialogueKey)
            {
                HideDialogueWindow();
                return;
            }
            
            _text.text = _dialogueSystemManager.GetCurrentDialogue(CurrentNpcType);
            _npcName.text = _dialogueSystemManager.GetNpcName(CurrentNpcType);

            _rectTransform.anchoredPosition = _startPosition;
             
             var color = _text.color;
             color.a = 0;
             _text.color = color;
             
             Sequence sequence = DOTween.Sequence();
             sequence.
                 Join(_rectTransform.DOAnchorPos(_endPosition, MOVE_DURATION).SetEase(Ease.Linear))
                 .Join(_text.DOFade(1f, FADE_DURATION));
        }

        public void ShowSpeakerImage()
        {
            
        }

        private void HideDialogueWindow()
        {
            Close();
        }

        private void IncreaseDialogueStep()
        {
            _dialogueSystemManager.UpgradeDialogueState(CurrentNpcType);
            ShowDialogueText();
        }
    }
}