using UnityEngine;

namespace Project.Scripts
{
    public class SummonSkeletonMageSpell : SpellBase
    {
        public override void Initialize()
        {
            ID  = "bones_0_0";
            IsInitialized = true;
        }
        
        public async override void Cast()
        {
            Debug.Log("cast");
            
            Debug.Log(SummonSystem);
            Debug.Log(MouseController);
            
            await SummonSystem.CreateSkeletonMageAsync(MouseController.MouseTarget.TargetPosition);
        }
    }
}