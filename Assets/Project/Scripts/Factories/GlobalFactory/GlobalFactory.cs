using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Project.Scripts.Core;
using Project.Scripts.Services;
using Project.Scripts.UI;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;
using Edge = Project.Scripts.UI.Edge;
using Object = UnityEngine.Object;

namespace Project.Scripts
{
    [UsedImplicitly]
    public class GlobalFactory
    {
        private DiContainer _diContainer;
        private readonly IAssetLoader _assetLoader;

        [Inject] private BookInteractable _bookInteractable;
        [Inject] private ExitInteractable _exitInteractable;
        [Inject] private PlayerController _playerController;

        public GlobalFactory(IAssetLoader assetLoader, DiContainer diContainer)
        {
            _diContainer = diContainer;
            _assetLoader = assetLoader;
        }

        public async UniTask<List<Edge>> CreateSkillEdge(Skill firstSkill, Transform parent = null)
        {
            List<Edge> edgeList = new();

            var edgePrefab = await _assetLoader.LoadGameObjectAsync<Edge>("Edge");
            var linkedSkills = firstSkill.GetLinkedSkills();

            foreach (var linkedSkill in linkedSkills)
            {
                var existingEdge = firstSkill.GetLinkedEdges()
                    .Intersect(linkedSkill.GetLinkedEdges())
                    .FirstOrDefault();

                if (existingEdge != null)
                {
                    continue;
                }

                var edge = GameObject.Instantiate(edgePrefab, parent);

                var edgeRect = edge.GetComponent<RectTransform>();
                var fromRect = firstSkill.GetComponent<RectTransform>();
                var toRect = linkedSkill.GetComponent<RectTransform>();

                var fromPos = fromRect.anchoredPosition;
                var toPos = toRect.anchoredPosition;

                var dir = (toPos - fromPos).normalized;
                var distance = Vector2.Distance(fromPos, toPos);

                edgeRect.anchoredPosition = fromPos;

                edge.Initialize(distance, firstSkill, linkedSkill);

                firstSkill.AddEdge(edge);
                linkedSkill.AddEdge(edge);

                if (!firstSkill.GetLinkedSkills().Contains(linkedSkill))
                    firstSkill.GetLinkedSkills().Add(linkedSkill);
                if (!linkedSkill.GetLinkedSkills().Contains(firstSkill))
                    linkedSkill.GetLinkedSkills().Add(firstSkill);

                var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                edgeRect.rotation = Quaternion.Euler(0, 0, angle);

                edgeList.Add(edge);
            }

            return edgeList;
        }


        public async UniTask<T> CreateAsync<T>(string assetAddress, Vector3 position = default)
            where T : Object
        {
            var prefab = await _assetLoader.LoadGameObjectAsync<Object>(assetAddress);
            var gameObject = Object.Instantiate(prefab, position, Quaternion.identity);
            var component = gameObject.GetComponent<T>();

            return component;
        }

        public async UniTask<T> CreateAndInitializeAsync<T>(string assetAddress, Vector3 position = default)
            where T : Object
        {
            var prefab = await _assetLoader.LoadGameObjectAsync<Object>(assetAddress);
            var gameObject = Object.Instantiate(prefab, position, Quaternion.identity);
            var component = gameObject.GetComponent<T>();

            return component;
        }

        public async UniTask<T> CreateAndInjectAsync<T>(string assetAddress, Vector3 spawnPosition = default)
            where T : Object
        {
            var prefab = await _assetLoader.LoadGameObjectAsync<T>(assetAddress);
            var gameObject = Object.Instantiate(prefab, spawnPosition, Quaternion.identity);
            var component = gameObject.GetComponent<T>();
            _diContainer.Inject(component);

            return component;
        }

        public async UniTask<T> CreateForComponentAndInitialize<T>(string assetAddress, Vector3 spawnPosition)
            where T : Object
        {
            var prefab = await _assetLoader.LoadGameObjectAsync<PlayerController>(assetAddress);

            var gameObject = _diContainer.InstantiatePrefabForComponent<PlayerController>(prefab);
            var component = gameObject.GetComponent<PlayerController>();

            component.Initialize();

            return component as T;
        }

        #region Characters

        public async UniTask<T> CreateNpcAsync<T>(string npcAssetAddress, Vector3 spawnPosition) where T : NpcBase
        {
            return await CreateAndInjectAsync<T>(npcAssetAddress, spawnPosition);
        }

        public async UniTask<T> CreateEnemyAsync<T>(string enemyAssetAddress, Vector3 spawnPosition) where T : EnemyBase
        {
            return await CreateAndInitializeAsync<T>(enemyAssetAddress, spawnPosition);
        }

        public async UniTask<T> CreateSummonAsync<T>(string summonUnitAssetAddress, Vector3 spawnPosition)
            where T : SummonUnitBase
        {
            var summonUnit = await _assetLoader.LoadGameObjectAsync<Object>(summonUnitAssetAddress);
            var instance = _diContainer.InstantiatePrefabForComponent<T>(summonUnit);

            if (instance == null)
            {
                throw new Exception("Cannot instantiate SkeletonMage");
            }

            instance.Initialize();
            instance.transform.position = spawnPosition;

            return instance;
        }

        #endregion

        #region Corpse

        public async UniTask<Corpse> CreateDefaultCorpseAsync(Vector3 spawnPosition)
        {
            return await CreateAsync<Corpse>("Corpse", spawnPosition);
        }

        public async UniTask<Corpse> CreateCustomCorpseAsync(bool hasSoul, bool hasBlood, bool hasFlesh, bool hasBones,
            Vector3 spawnPosition)
        {
            var corpseComponent = await CreateDefaultCorpseAsync(spawnPosition);

            if (!hasSoul)
            {
                corpseComponent.RemoveResource(ResourceType.Soul);
            }

            if (!hasBlood)
            {
                corpseComponent.RemoveResource(ResourceType.Blood);
            }

            if (!hasFlesh)
            {
                corpseComponent.RemoveResource(ResourceType.Flesh);
            }

            if (!hasBones)
            {
                corpseComponent.RemoveResource(ResourceType.Bones);
            }

            return corpseComponent;
        }

        #endregion

        #region Interactable

        public BookInteractable CreateBook(Vector3 position)
        {
            var instance = _diContainer.InstantiatePrefabForComponent<BookInteractable>(_bookInteractable);
            instance.transform.position = position;
            return instance;
        }

        public ExitInteractable CreateExit(Vector3 position)
        {
            var instance = _diContainer.InstantiatePrefabForComponent<ExitInteractable>(_exitInteractable);
            instance.transform.position = position;
            return instance;
        }
        
        #endregion
    }
}