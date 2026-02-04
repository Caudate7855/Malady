using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Project.Scripts
{
    public class ItemsFactory
    {
        private readonly Random _random = new();

        private readonly ItemView _itemViewPrefabPrefab;
        private readonly ItemsConfig _itemsConfig;

        public ItemsFactory(ItemsConfig itemsConfig, ItemView itemViewPrefab)
        {
            _itemsConfig = itemsConfig;
            _itemViewPrefabPrefab = itemViewPrefab;
        }

        public Item CreateRandomItem()
        {
            var values = Enum.GetValues(typeof(ItemType));
            var randomItemType = (ItemType)values.GetValue(_random.Next(values.Length));
            return CreateItemByType(randomItemType);
        }

        public Item CreateItemByType(ItemType itemType)
        {
            var config = _itemsConfig.GetItemConfigByType(itemType);
            var type = config.Type;
            var sprite = config.Sprites[_random.Next(config.Sprites.Count)];
            var stats = GenerateRandomStats(config);
            var modifier = GenerateRandomModifier(config);
            
            return new Item(type, sprite, stats, modifier);
        }

        private List<StatBase> GenerateRandomStats(ItemConfig config)
        {
            var stats = new List<StatBase>();

            if (config.PossibleStat == null || config.PossibleStat.Count == 0)
            {
                return stats;
            }

            int statsCount = _random.Next(config.MinStatsCount, config.MaxStatsCount + 1);
            statsCount = Mathf.Clamp(statsCount, 0, config.PossibleStat.Count);
    
            var availableStats = new List<PossibleStat>(config.PossibleStat);
    
            for (int i = 0; i < statsCount && availableStats.Count > 0; i++)
            {
                var selectedStat = SelectStatByPosition(availableStats);
        
                if (selectedStat.Stat != null)
                {
                    if (Activator.CreateInstance(selectedStat.Stat.GetType()) is StatBase stat)
                    {
                        stats.Add(stat);
                        availableStats.Remove(selectedStat);
                    }
                }
            }
    
            return stats;
        }

        private ISpellModifier GenerateRandomModifier(ItemConfig config)
        {
            var selectedModif = SelectModifByPosition(config.PossibleModifs);
            
            if (selectedModif.Modifier != null)
            {
                return Activator.CreateInstance(selectedModif.Modifier.GetType()) as ISpellModifier;
            }
            
            return null;
        }

        private PossibleStat SelectStatByPosition(List<PossibleStat> possibleStats)
        {
            int totalWeight = 0;
            
            for (int i = 0; i < possibleStats.Count; i++)
            {
                totalWeight += possibleStats.Count - i;
            }
            
            int randomWeight = _random.Next(totalWeight);
            int currentWeight = 0;
            
            for (int i = 0; i < possibleStats.Count; i++)
            {
                currentWeight += possibleStats.Count - i;
                
                if (randomWeight < currentWeight)
                {
                    return possibleStats[i];
                }
            }
            
            return possibleStats.Last();
        }

        private PossibleModif SelectModifByPosition(List<PossibleModif> possibleModifs)
        {
            int totalWeight = 0;
            
            for (int i = 0; i < possibleModifs.Count; i++)
            {
                totalWeight += possibleModifs.Count - i;
            }
            
            int randomWeight = _random.Next(totalWeight);
            int currentWeight = 0;
            
            for (int i = 0; i < possibleModifs.Count; i++)
            {
                currentWeight += possibleModifs.Count - i;
                
                if (randomWeight < currentWeight)
                {
                    return possibleModifs[i];
                }
            }
            
            return possibleModifs.Last();
        }
    }
}