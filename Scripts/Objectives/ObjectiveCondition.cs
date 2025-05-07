using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectiveCondition
{
    public int objectiveIndex;
    public string objectiveName;
    public ObjectiveType conditionType;

    [Header("HasItem")]
    public string requiredItemName;
    public bool invertCondition;

    [Header("EnteredArea")]
    public string requiredAreaTag;

    [Header("KilledEnemy")]
    public string requiredEnemyTag;
    public int requiredKillCount = 1;
    private int currentKillCount = 0;

    public bool HasConditionMet(PlayerInventory inventory, HashSet<string> enteredAreas, Dictionary<string, int> killCounts)
    {
        switch (conditionType)
        {
            case ObjectiveType.HasItem:
                bool hasItem = inventory.HasItem(requiredItemName);
                return invertCondition ? !hasItem : hasItem;

            case ObjectiveType.EnteredArea:
                return enteredAreas.Contains(requiredAreaTag);

            case ObjectiveType.KilledEnemy:
                return killCounts.TryGetValue(requiredEnemyTag, out int kills) && kills >= requiredKillCount;
        }

        return false;
    }

    public void RegisterKill(string enemyTag)
    {
        if (conditionType == ObjectiveType.KilledEnemy && enemyTag == requiredEnemyTag)
        {
            currentKillCount++;
        }
    }
}

public enum ObjectiveType
{
    HasItem,
    EnteredArea,
    KilledEnemy,
}
