using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Project.Scripts.Core;
using Project.Scripts.Interfaces;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Project.Scripts
{
    [UsedImplicitly]
    public class EnemyFactory
    {
        private const string ENEMY_MELEE_ADDRESS = "EnemyMelee";
        private const string ENEMY_RANGE_ADDRESS = "EnemyRange";

        private readonly IAssetLoader _assetLoader;

        private Dictionary<EnemyTypes, string> _enemyAddresses = new()
        {
            { EnemyTypes.Melee, ENEMY_MELEE_ADDRESS },
            { EnemyTypes.Range, ENEMY_RANGE_ADDRESS },
        };

        public EnemyFactory(IAssetLoader assetLoader)
        {
            _assetLoader = assetLoader;
        }

        public async Task<T> Create<T>(EnemyTypes enemyType, Vector3 spawnPosition) where T : EnemyBase, ICustomInitializable
        {
            if (_enemyAddresses.TryGetValue(enemyType, out var enemyAddress))
            {
                var prefab = await _assetLoader.Load<EnemyBase>(enemyAddress);
                var gameObject = Object.Instantiate(prefab, spawnPosition, Quaternion.identity);
                var component = gameObject.GetComponent<T>();

                component.Initialize();

                return component;
            }

            throw new Exception("Cannot find requested enemy address.");
        }
    }
}