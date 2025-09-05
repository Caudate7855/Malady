using System.Collections.Generic;
using JetBrains.Annotations;
using Project.Scripts.Core;
using Project.Scripts.UI.Inventory;
using UnityEngine;

namespace Project.Scripts
{
    [UsedImplicitly]
    public class SummonSkeletonMageSpell : SpellBase
    {
        private SkeletonMagesCountStat _skeletonMagesCountStat;
        private List<SkeletonMage> _skeletonMages = new();
        
        public override void Initialize()
        {
            //todo: Пофиксить
            ID  = "bones_0_0_qweqweqwe";
            IsInitialized = true;
            
            _skeletonMagesCountStat = PlayerStats.GetStat<SkeletonMagesCountStat>();
            InventoryController = PanelManager.LoadPanel<InventoryController>();
            Type = _skeletonMagesCountStat.Type;
        }
        
        public async override void Cast()
        {
            if (_skeletonMagesCountStat.Value < _skeletonMagesCountStat.MaxValue)
            {
                _skeletonMages.Add(await SummonSystem.CreateSkeletonMageAsync(MouseController.MouseTarget.TargetPosition));

                _skeletonMagesCountStat.Value++;
                
                PlayerStats.UpdateStat<SkeletonMagesCountStat>(_skeletonMagesCountStat.Value);
                InventoryController.UpdateStatView(Type, _skeletonMagesCountStat.Value);
            }
            else
            {
                Object.Destroy(_skeletonMages[0].gameObject);
                _skeletonMages.RemoveAt(0);
                _skeletonMages.Add(await SummonSystem.CreateSkeletonMageAsync(MouseController.MouseTarget.TargetPosition));
                _skeletonMagesCountStat.Value++;
            }
        }

        public override void Clear()
        {
            for (int i = 0, count = _skeletonMages.Count; i < count; i++)
            {
                Object.Destroy(_skeletonMages[i].gameObject);
            }
            
            _skeletonMages.Clear();
        }
    }
}