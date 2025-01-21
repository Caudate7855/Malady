using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Project.Scripts.Core;
using Project.Scripts.DialogueSystem;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Project.Scripts
{
    [UsedImplicitly]
    public class DialogueFactory
    {
        private const string BLACKSMITH_ADDRESS = "Blacksmith";

        private readonly IAssetLoader _assetLoader;

        private readonly Dictionary<NpcTypes, string> _npcAddresses = new()
        {
            { NpcTypes.Blacksmith, BLACKSMITH_ADDRESS },
        };

        public DialogueFactory(IAssetLoader assetLoader)
        {
            _assetLoader = assetLoader;
        }

        public async Task<T> Create<T>(NpcTypes npcType) where T : IDialogable
        {
            if (_npcAddresses.TryGetValue(npcType, out var enemyAddress))
            {
                var prefab = await _assetLoader.Load<NpcBase>(enemyAddress);
                var gameObject = Object.Instantiate(prefab, prefab.transform.position, Quaternion.identity);
                var component = gameObject.GetComponent<T>();

                return component;
            }

            throw new Exception("Cannot find requested NPC address.");
        }
    }
}