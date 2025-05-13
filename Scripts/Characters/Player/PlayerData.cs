using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player/Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Status Base")]
    public int maxHP = 100;
    public float moveSpeed = 8f;
    public float baseDamage = 4f;
    public float specialCooldown = 5f;
    public float itemPickupRadius = 8f;
    public int maxGemSlots = 1;
    public int maxGemFragmentsPerSlot = 5;

    [Header("Sistema de Combo")]
    public int multiplierDecayDuration = 5;

    [Header("Melhorias Temporárias")]
    public float damageModifier = 1f;
    public float speedModifier = 1f;

    public float EffectiveDamage => baseDamage * damageModifier;
    public float EffectiveSpeed => moveSpeed * speedModifier;
}

public enum PlayerStat
{
    HP,
    Speed,
    CollectArea,
    MultiplierDecay,
    Dash,
    GemSlots,
    GemFragmentsPerSlot,
}
