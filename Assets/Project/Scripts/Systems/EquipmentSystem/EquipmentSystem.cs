using System;
using System.Collections.Generic;
using Project.Scripts.Player;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Project.Scripts
{
    public class EquipmentSystem : IInitializable
    {
        [Inject] private PlayerController _playerController;

        private Dictionary<ItemType, Transform> _itemsContainers;
        private Dictionary<string, Transform> _playerBones;

        public void Initialize()
        {
            _itemsContainers = new Dictionary<ItemType, Transform>
            {
                { ItemType.Armor, _playerController.ArmorContainer },
                { ItemType.Gloves, _playerController.GlovesContainer },
                { ItemType.Pants, _playerController.PantsContainer },
                { ItemType.Boots, _playerController.BootsContainer },
            };

            _playerBones = BuildBoneMap(_playerController.transform);
        }

        public void EquipItem(ItemData itemData)
        {
            RemoveItem(itemData.Type);

            var instance = Object.Instantiate(itemData.Prefab);
            var container = _itemsContainers[itemData.Type];

            instance.transform.SetParent(container, false);
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localRotation = Quaternion.identity;
            instance.transform.localScale = Vector3.one;

            var skinnedMeshRenderers = instance.GetComponentsInChildren<SkinnedMeshRenderer>(true);

            foreach (var skinnedMeshRenderer in skinnedMeshRenderers)
            {
                BindToPlayerSkeleton(skinnedMeshRenderer);
            }
        }

        public void RemoveItem(ItemType itemType)
        {
            var container = _itemsContainers[itemType];

            for (var i = container.childCount - 1; i >= 0; i--)
            {
                Object.Destroy(container.GetChild(i).gameObject);
            }
        }

        private void BindToPlayerSkeleton(SkinnedMeshRenderer skinnedMeshRenderer)
        {
            if (skinnedMeshRenderer.bones == null || skinnedMeshRenderer.bones.Length == 0)
            {
                throw new Exception();
            }

            var remappedBones = new Transform[skinnedMeshRenderer.bones.Length];

            for (var i = 0; i < skinnedMeshRenderer.bones.Length; i++)
            {
                var sourceBone = skinnedMeshRenderer.bones[i];

                if (sourceBone == null)
                {
                    throw new Exception();
                }

                if (!_playerBones.TryGetValue(sourceBone.name, out var targetBone))
                {
                    throw new Exception();
                }

                remappedBones[i] = targetBone;
            }

            skinnedMeshRenderer.bones = remappedBones;

            if (skinnedMeshRenderer.rootBone != null)
            {
                if (!_playerBones.TryGetValue(skinnedMeshRenderer.rootBone.name, out var rootBone))
                {
                    throw new Exception();
                }

                skinnedMeshRenderer.rootBone = rootBone;
            }
        }

        private Dictionary<string, Transform> BuildBoneMap(Transform root)
        {
            var result = new Dictionary<string, Transform>();
            var transforms = root.GetComponentsInChildren<Transform>(true);

            foreach (var transform in transforms)
            {
                if (!result.ContainsKey(transform.name))
                {
                    result.Add(transform.name, transform);
                }
            }

            return result;
        }
    }
}