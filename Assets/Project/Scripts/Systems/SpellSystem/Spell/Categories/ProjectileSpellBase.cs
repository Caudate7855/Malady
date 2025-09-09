using Zenject;

namespace Project.Scripts
{
    public class ProjectileSpellBase : SpellBase
    {
        [Inject] private GlobalFactory _globalFactory;

        private const string BloodSpearAddress = "BloodSpear";
        private const string BoneSpearAddress = "BoneSpear";
        
        public override void Initialize()
        {
            
        }

        public override void Cast()
        {
            
        }

        public override void Clear()
        {
            
        }
    }
}