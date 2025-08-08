using System.Collections.Generic;
using Project.Scripts.Core;
using UnityEngine;

namespace Project.Scripts
{
    public class SummonSkeletonMageSpell : SpellBase
    {
        private SkeletonsCountStat _skeletonsCountStat;
        private List<SkeletonMage> _skeletonMages = new();
        
        public override void Initialize()
        {
            ID  = "bones_0_0";
            IsInitialized = true;
            
            _skeletonsCountStat = PlayerStats.GetStat<SkeletonsCountStat>() as  SkeletonsCountStat;
        }
        
        public async override void Cast()
        {
            if (_skeletonsCountStat.Value < _skeletonsCountStat.MaxValue)
            {
                _skeletonMages.Add(await SummonSystem.CreateSkeletonMageAsync(MouseController.MouseTarget.TargetPosition));
                _skeletonsCountStat.Value++;
            }
            else
            {
                Object.Destroy(_skeletonMages[0].gameObject);
                _skeletonMages.RemoveAt(0);
                _skeletonMages.Add(await SummonSystem.CreateSkeletonMageAsync(MouseController.MouseTarget.TargetPosition));
                _skeletonsCountStat.Value++;
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