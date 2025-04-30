using UnityEngine;
using TMPro;
using System.Collections.Generic;
public class PlayerInventory : MonoBehaviour
{
    public bool hasGoldKey = false;
    public bool hasSilverKey = false;
    public bool hasRustedKey = false;
    public bool hasBossKey = false;
    public float currentGold;
    public TMP_Text goldText;

    [SerializeField] private List<string> itens = new List<string>();
    
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

    public bool HasFourElements()
    {
        // Lista dos quatro elementos obrigatórios
        string[] requiredItems = { "Heart", "Dove", "Lamb", "Water" };

        // Verifica se todos os itens obrigatórios estão contidos na lista 'itens'
        foreach (string item in requiredItems)
        {
            if (!itens.Contains(item))
                return false; // Se faltar qualquer um, retorna falso
        }

        return true; // Todos os itens estão presentes
    }

    public List<string> GetItens()
    {
        return itens;
    }
}