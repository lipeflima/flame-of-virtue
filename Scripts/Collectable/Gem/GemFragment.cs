using UnityEngine;

public class GemFragment
{
    public GemSO gemType;
    public ProjectileStat statAffected;
    public float valueModifier;

    public GemFragment(GemSO gem, int playerLevel)
    {
        gemType = gem;
        statAffected = GetRandomStat();
        float baseMultiplier = 1f + (playerLevel * 0.1f);
        valueModifier = Random.Range(2f, 5f) * baseMultiplier;
    }

    private ProjectileStat GetRandomStat()
    {
        ProjectileStat[] stats = (ProjectileStat[])System.Enum.GetValues(typeof(ProjectileStat));
        return stats[Random.Range(0, stats.Length)];
    }

    public GemSO GetParentGem()
    {
        return gemType;
    }
}
