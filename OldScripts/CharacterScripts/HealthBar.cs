using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Slider healthBarNormal, healthBarDelayed;
    //public GameObject slider;
    //public Text cHealthText;
    public TMP_Text cHealthText;
    protected Health health;
    void Start()
    {
        health = GetComponent<Health>();
        healthBarNormal.value = health.currentHealth;
        healthBarNormal.maxValue = health.maxHealth;
        healthBarDelayed.value = health.currentHealth;
        healthBarDelayed.maxValue = health.maxHealth;

        cHealthText.text = " " + health.currentHealth + "%";// + "/" + " " + health.maxHealth;
    }

    public void UpdateHealthHud(float cHealth, float mHealth, bool add)
    {
        StopCoroutine(DelayedBarUpdate(add));

        if (cHealth < 0)
        {
            cHealth = 0;
        }

        if (!add)
        {
            healthBarNormal.value = cHealth;
            healthBarNormal.maxValue = mHealth;
            healthBarDelayed.maxValue = mHealth;
        }
        else
        {
            healthBarDelayed.value = cHealth;
            healthBarNormal.maxValue = mHealth;
            healthBarDelayed.maxValue = mHealth;
        }

        cHealthText.text = " " + cHealth;// + "/" + " " + mHealth;

        StartCoroutine(DelayedBarUpdate(add));

    }
    IEnumerator DelayedBarUpdate(bool add)
    {
        if (!add)
        {
            if (healthBarDelayed.value > healthBarNormal.value)
            {
                while (healthBarDelayed.value > healthBarNormal.value)
                {
                    yield return new WaitForSeconds(0.1f);
                    healthBarDelayed.value -= 5;
                    if (healthBarDelayed.value < healthBarNormal.value)
                    {
                        healthBarDelayed.value = healthBarNormal.value;
                    }
                }
            }
        }
        else
        {
            if (healthBarDelayed.value > healthBarNormal.value)
            {
                while (healthBarDelayed.value > healthBarNormal.value)
                {
                    yield return new WaitForSeconds(0.1f);
                    healthBarNormal.value += 5;
                    if (healthBarDelayed.value < healthBarNormal.value)
                    {
                        healthBarNormal.value = healthBarDelayed.value;
                    }
                }
            }
        }

        if (healthBarDelayed.value < healthBarNormal.value)
        {
            while (healthBarDelayed.value < healthBarNormal.value)
            {
                yield return new WaitForSeconds(0.1f);
                healthBarDelayed.value += 5;
                if (healthBarDelayed.value > healthBarNormal.value)
                {
                    healthBarDelayed.value = healthBarNormal.value;
                }
            }
        }

        yield return 0;
    }
    IEnumerator HideSlider()
    {
        yield return new WaitForSeconds(3);
        //slider.SetActive(false);
    }

}
