using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Collector : MonoBehaviour
{
    public float currentXP, maxXP, currentMoney;
    //private XPBar xpBar;
    private Character character;
    public TMP_Text textMeshPro;
    private void Start()
    {
        character = Character.Instance;
    }
    public void SetColectItem()
    {        
                
    }
}
