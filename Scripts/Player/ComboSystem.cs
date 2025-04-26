using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboSystem : MonoBehaviour
{
    private float currentHit;
    public float maxHit = 200f;
    private bool canUseSpecial = false;
    public Slider comboBar;

    void Start()
    {
        comboBar.maxValue = maxHit;
    }

    void Update()
    {
        CheckHit();
    }

    public void AddHit(float amount)
    {
        //if(currentHit + amount <= maxHit)
        currentHit += amount; 
        
        UpdateUI();       
    }

    void CheckHit()
    {
        if(currentHit >= maxHit)
        {
            canUseSpecial = true;
        }
        else
        {
            canUseSpecial = false;
        }
    }

    void UpdateUI()
    {
        comboBar.value = currentHit;
    }

    public void UseSpecial()
    {
        currentHit = 0;
        UpdateUI();
    }
    
    public bool GetSpecialStatus()
    {
        return canUseSpecial;
    }
}
