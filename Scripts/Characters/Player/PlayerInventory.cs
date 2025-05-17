using UnityEngine;
using TMPro;
using System.Collections.Generic;
public class PlayerInventory : MonoBehaviour
{
    public float currentGold;
    public TMP_Text goldText;
    private List<GemSO> collectedGems = new List<GemSO>();
    private List<EquippedGem> equipedGems = new List<EquippedGem>();
    private List<GemFragment> collectedGemFragments = new List<GemFragment>();

    private List<string> itens = new List<string>();
    
    public void AddGold(float amount)
    {
        currentGold += amount;

        goldText.text = "" + currentGold;
    }

    public void AddItem(string itemName)
    {
        itens.Add(itemName);
    }

    public bool HasItem(string itemName)
    {
        return itens.Contains(itemName);
    }

    public List<string> GetItens()
    {
        return itens;
    }

    public void AddGem(GemSO gem)
    {
        collectedGems.Add(gem);
        FindObjectOfType<GemInventoryUI>().RefreshUI();
    }

    public void AddGemFragment(GemFragment gem)
    {
        collectedGemFragments.Add(gem);
    }

    public List<GemSO> GetCollectedGems()
    {
        return collectedGems;
    }
}