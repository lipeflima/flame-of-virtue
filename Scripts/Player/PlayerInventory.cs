using UnityEngine;
using TMPro;
public class PlayerInventory : MonoBehaviour
{
    public bool hasKey = false;
    public float currentGold;
    public TMP_Text goldText;
    
    public void AddGold(float amount)
    {
        currentGold += amount;

        goldText.text = "" + currentGold;
    }
}