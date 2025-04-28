using UnityEngine;
using TMPro;
using System.Collections.Generic;
public class PlayerInventory : MonoBehaviour
{
    public bool hasGoldKey = false;
    public bool hasSilverKey = false;
    public bool hasRustedKey = false;
    public float currentGold;
    public TMP_Text goldText;

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
}