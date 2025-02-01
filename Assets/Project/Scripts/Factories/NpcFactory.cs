using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Itibsoft.PanelManager;
using JetBrains.Annotations;
using Project.Scripts.Core;
using Project.Scripts.Services;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Project.Scripts
{
    [UsedImplicitly]
    public class NpcFactory
    {
        private const string UNDERTAKER_ADDRESS = "Undertaker";
        private const string BLACKSMITH_ADDRESS = "Blacksmith";
        private const string TRADER_ADDRESS = "Trader";

        private readonly IAssetLoader _assetLoader;
        private readonly IPanelManager _panelManger;
        private DiContainer _diContainer;

        private readonly Dictionary<NpcTypes, string> _npcAddresses = new()
        {
            { NpcTypes.Undertaker, UNDERTAKER_ADDRESS },
            { NpcTypes.Blacksmith, BLACKSMITH_ADDRESS },
            { NpcTypes.Trader, TRADER_ADDRESS },
        };

        public NpcFactory(IAssetLoader assetLoader, IPanelManager panelManager, DiContainer diContainer)
        {
            _assetLoader = assetLoader;
            _panelManger = panelManager;
            _diContainer = diContainer;
        }
        
        public async Task<T> CreateNpcAsync<T>(NpcTypes npcType, Vector3 spawnPosition) where T : NpcBase
        {
            if (_npcAddresses.TryGetValue(npcType, out var enemyAddress))
            {
                var prefab = await _assetLoader.LoadGameObjectAsync<NpcBase>(enemyAddress);
                var gameObject = Object.Instantiate(prefab, spawnPosition, Quaternion.identity);
                var component = gameObject.GetComponent<T>();
                _diContainer.Inject(component);
                
                return component;
            }

            throw new Exception("Cannot find requested NPC address.");
        }
    }
}