using UnityEngine;

[CreateAssetMenu(fileName = "LevelUpBonusData", menuName = "Player/Level Up Bonus Data")]
public class LevelUpBonusData : ScriptableObject
{
    public AnimationCurve hpGrowthCurve;

    public int GetBonusHP(int level)
    {
        return Mathf.FloorToInt(hpGrowthCurve.Evaluate(level));
    }

    // Aqui você pode adicionar outros bônus futuramente
    // public int GetBonusDamage(int level) { ... }
    // public float GetMoveSpeedBonus(int level) { ... }
}