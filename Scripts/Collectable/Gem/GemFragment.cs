using UnityEngine;
using static MagicStats;

public class GemFragment
{
    public GemSO gemType;
    public MagicStat statAffected;
    public int valueModifier;

    public GemFragment(GemSO gem, int playerLevel)
    {
        gemType = gem;
        statAffected = GetRandomStat();
        float baseMultiplier = 1f + (playerLevel * 0.1f);
        valueModifier = (int)(Random.Range(2f, 5f) * baseMultiplier);
    }

    private MagicStat GetRandomStat()
    {
        MagicStat[] stats = (MagicStat[])System.Enum.GetValues(typeof(MagicStat));
        return stats[Random.Range(0, stats.Length)];
    }

    public GemSO GetParentGem()
    {
        return gemType;
    }
}
