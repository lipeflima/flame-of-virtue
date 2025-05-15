[System.Serializable]
public class MagicStats
{
    public enum MagicType { SoulFire, SpecialFire, ExplosiveFire, AuraFire }
    public enum MagicStat { Speed, Damage, AttackRate, CoolDown, Lifetime }
    public float speed = 8f;
    public float damage = 3f;
    public float attackRate = 1f;
    public float coolDown = 1f;
    public float lifetime = 3f;
    public float speedMultiplier = 1f;
    public float attackRateMultiplier = 1f;
    public float coolDownMultiplier = 1f;
    public float lifetimeMultiplier = 1f;
    public float damageMultiplier = 1f;

    public float EffectiveAttackRate => attackRate * attackRateMultiplier;
    public float EffectiveDamage => damage * damageMultiplier;
    public float EffectiveSpeed => speed * speedMultiplier;
    public float EffectiveCooldown => coolDown * coolDownMultiplier;
    public float EffectiveLifetime => lifetime * lifetimeMultiplier;
    public MagicStats() { }
    public MagicStats(MagicStats other)
    {
        damage = other.damage;
        attackRate = other.attackRate;
        speed = other.speed;
        lifetime = other.lifetime;
        coolDown = other.coolDown;
    }
}