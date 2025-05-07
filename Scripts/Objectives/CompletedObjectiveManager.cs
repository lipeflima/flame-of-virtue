using System.Collections.Generic;
using UnityEngine;

public class CompletedObjectiveManager : MonoBehaviour
{
    public ObjectiveManager objManager;
    public PlayerInventory player;

    public List<ObjectiveCondition> objectivesToCheck;

    private HashSet<string> enteredAreas = new HashSet<string>();
    private Dictionary<string, int> killCounts = new Dictionary<string, int>();

    void Update()
    {
        foreach (var condition in objectivesToCheck)
        {
            if (!objManager.allObjectives[condition.objectiveIndex].isCompleted &&
                condition.HasConditionMet(player, enteredAreas, killCounts))
            {
                objManager.CompleteObjective(condition.objectiveIndex);
            }
        }
    }

    public void RegisterAreaEntry(string areaTag)
    {
        enteredAreas.Add(areaTag);
    }

    public void RegisterEnemyKill(string enemyTag)
    {
        if (!killCounts.ContainsKey(enemyTag))
            killCounts[enemyTag] = 0;

        killCounts[enemyTag]++;
    }
}
