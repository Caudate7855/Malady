using System.Collections.Generic;
using JetBrains.Annotations;
using Project.Scripts.Core;
using Project.Scripts.UI.Inventory;
using Object = UnityEngine.Object;

namespace Project.Scripts
{
    [UsedImplicitly]
    public class SummonSkeletonArcherSpell : SpellBase
    {
        private SkeletonArchersCountStat _skeletonArchersCountStat;
        private List<SkeletonArcher> _skeletonArchers = new();
        
        public override void Initialize()
        {
            ID  = "bones_0_0";
            IsInitialized = true;
            
            _skeletonArchersCountStat = PlayerStats.GetStat<SkeletonArchersCountStat>();
            InventoryController = PanelManager.LoadPanel<InventoryController>();
            Type = _skeletonArchersCountStat.Type;
        }
        
        public async override void Cast()
        {
            if (_skeletonArchersCountStat.Value < _skeletonArchersCountStat.MaxValue)
            {
                _skeletonArchers.Add(await SummonSystem.CreateSkeletonArcherAsync(MouseController.MouseTarget.TargetPosition));

                _skeletonArchersCountStat.Value++;
                
                PlayerStats.UpdateStat<SkeletonMagesCountStat>(_skeletonArchersCountStat.Value);
                InventoryController.UpdateStatView(Type, _skeletonArchersCountStat.Value);
            }
            else
            {
                Object.Destroy(_skeletonArchers[0].gameObject);
                _skeletonArchers.RemoveAt(0);
                _skeletonArchers.Add(await SummonSystem.CreateSkeletonArcherAsync(MouseController.MouseTarget.TargetPosition));
                _skeletonArchersCountStat.Value++;
            }
        }

        public override void Clear()
        {
            for (int i = 0, count = _skeletonArchers.Count; i < count; i++)
            {
                Object.Destroy(_skeletonArchers[i].gameObject);
            }
            
            _skeletonArchers.Clear();
        }
    }
}