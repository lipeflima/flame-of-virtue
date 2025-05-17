using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GemInventoryUI : MonoBehaviour
{
    [Header("Prefabs e Containers")]
    public Transform gemGridParent;
    public GameObject gemSlotPrefab;

    [Header("Fonte de Dados")]
    public PlayerInventory playerInventory;

    private List<GemSlotUI> activeSlots = new();
    public int totalSlots = 30;

    void Start()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
    foreach (var slot in activeSlots)
    {
        Destroy(slot.gameObject);
    }
    activeSlots.Clear();

    for (int i = 0; i < totalSlots; i++)
    {
        GameObject slotGO = Instantiate(gemSlotPrefab, gemGridParent);
        GemSlotUI slotUI = slotGO.GetComponent<GemSlotUI>();

        if (i < playerInventory.GetCollectedGems().Count)
        {
            slotUI.SetGem(playerInventory.GetCollectedGems()[i]);
        }
        else
        {
            slotUI.ClearSlot();
        }

        activeSlots.Add(slotUI);
    }
}
}