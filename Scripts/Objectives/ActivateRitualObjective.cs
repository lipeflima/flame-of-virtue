using UnityEngine;

public class ActivateRitualObjective : MonoBehaviour
{
    public PlayerInventory inventory;

    // Update is called once per frame
    void Update()
    {
        if (inventory.HasFourElements()) 
        {
            ObjectiveManager.instance.ActivateObjective(4);
        }
    }
}
