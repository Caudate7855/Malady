namespace Project.Scripts
{
    public class BoneSpear : ProjectileBase
    {
        protected override float CalculateDamage()
        {
            var damage = PlayerStats.GetStat<BoneSpearBonusDamageStat>().Value;
            
            return damage;
        }
    }
}