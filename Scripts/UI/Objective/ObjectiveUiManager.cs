using System.Collections.Generic;
using UnityEngine;

public class ObjectivesUIManager : MonoBehaviour
{
    public GameObject objectiveItemPrefab;
    public Transform objectivesParent;

    private Dictionary<int, ObjectiveUIItem> uiItems = new Dictionary<int, ObjectiveUIItem>();

    public void InitializeObjectives(List<Objective> objectives)
    {
        foreach (Transform child in objectivesParent)
            Destroy(child.gameObject);

        uiItems.Clear();

        for (int i = 0; i < objectives.Count; i++)
        {
            var obj = Instantiate(objectiveItemPrefab, objectivesParent);
            var item = obj.GetComponent<ObjectiveUIItem>();
            item.SetText(objectives[i].description);
            uiItems[i] = item;
        }
    }

    public void MarkObjectiveCompleted(int index)
    {
        if (uiItems.ContainsKey(index))
            uiItems[index].MarkCompleted();
    }
}
