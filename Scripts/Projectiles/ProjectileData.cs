using UnityEngine;

[CreateAssetMenu(fileName = "newProjectileData", menuName = "Data/Projectile/Projectile Data")]

public class ProjectileData : ScriptableObject
{
    public float speed = 1f;
    public float damage = 3f;
    public float attackRate = 1f;
    public float coolDown = 1f;
    public float lifetime = 1f;

    [Header("Modificadores dinâmicos")]
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
}

public enum ProjectileStat { Speed, Damage, AttackRate, CoolDown, Lifetime }
