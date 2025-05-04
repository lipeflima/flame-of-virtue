using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComboSystem : MonoBehaviour
{
    private float currentHit;
    public float maxHit = 100f;
    private bool canUseSpecial = false;
    public Slider comboBar;
    public TMP_Text multiplierUI;
    public TMP_Text multiplierStatusUI;
    private int multiplierCount = 1;
    private bool multiplierApplied = false;
    private float decayDuration = 5f;
    private Coroutine decayCoroutine;   
    private Coroutine pulseCoroutine;
    public Slider multiplierTimerBar;

    void Start()
    {
        UpdateMaxHit();
    }

    void Update()
    {
        CheckHit();
    }

    public void AddHit(float amount)
    {
        //if(currentHit + amount <= maxHit)
        currentHit += amount; 
        RestartDecayTimer();
        UpdateUI();       
    }

    void CheckHit()
    {
        if(currentHit >= maxHit)
        {
            currentHit = 0;

            if (!multiplierApplied)
            {
                canUseSpecial = true;
                multiplierCount++;
                multiplierApplied = true;
                UpdateMaxHit();
                StartDecayTimer();
            }
            else
            {
                // Reinicia o contador a cada novo hit dentro da janela
                RestartDecayTimer();
            }
        }
        else
        {
            multiplierApplied = false;
        }
    }

    void UpdateUI()
    {
        comboBar.value = currentHit;
        multiplierUI.text = multiplierCount + "X";

        switch (multiplierCount)
        {
            case 1: multiplierStatusUI.text = "Ok"; break;
            case 2: multiplierStatusUI.text = "Better"; break;
            case 3: multiplierStatusUI.text = "Good"; break;
            case 4: multiplierStatusUI.text = "Awesome"; break;
            case 5: multiplierStatusUI.text = "Excelent"; break;
            case 6: multiplierStatusUI.text = "Wow"; break;
            case 7: multiplierStatusUI.text = "Unstoppable"; break;
            case 8: multiplierStatusUI.text = "MASSIVE"; break;
            default:
                multiplierStatusUI.text = "BREAK THE COUNTER";
                break;
        }

        multiplierStatusUI.color = Color.Lerp(Color.blue, Color.red, Mathf.InverseLerp(1, 8, multiplierCount));

        // Aplica efeito se for nível alto
        if (multiplierCount >= 6)
        {
            if (pulseCoroutine != null)
                StopCoroutine(pulseCoroutine);

            pulseCoroutine = StartCoroutine(PulseEffect());
        }
        else
        {
            if (pulseCoroutine != null)
            {
                StopCoroutine(pulseCoroutine);
                pulseCoroutine = null;
                multiplierStatusUI.transform.localScale = Vector3.one; // reseta escala
            }
        }
    }

    void UpdateMaxHit()
    {
        float baseHit = 100f;
        float growthRate = 1.5f;
        maxHit = Mathf.RoundToInt((baseHit + multiplierCount * 20f) * Mathf.Pow(growthRate, multiplierCount));
        comboBar.maxValue = maxHit;
    }

    IEnumerator PulseEffect()
    {
        float pulseSpeed = 2f;
        float scaleAmount = 1.2f;

        while (true)
        {
            float t = Mathf.PingPong(Time.time * pulseSpeed, 1f);
            float scale = Mathf.Lerp(1f, scaleAmount, t);
            multiplierStatusUI.transform.localScale = new Vector3(scale, scale, 1f);

            // Brilho leve na cor (mistura com branco)
            // multiplierStatusUI.color = Color.Lerp(multiplierStatusUI.color, Color.white, t * 0.3f);

            yield return null;
        }
    }

    public void UseSpecial()
    {
        currentHit = 0;
        canUseSpecial = false;
        UpdateUI();
    }
    
    public bool GetSpecialStatus()
    {
        return canUseSpecial;
    }

    public void ResetMultiplier()
    {
        currentHit = 0;
        canUseSpecial = false;
        multiplierCount = 1;
        UpdateUI();
        multiplierTimerBar.gameObject.SetActive(false); // esconde a barra
        multiplierTimerBar.value = decayDuration; // reseta valor
        UpdateMaxHit();
    }

    public int GetMultiplier()
    {
        return multiplierCount;
    }

    void StartDecayTimer()
    {
        if (decayCoroutine != null)
            StopCoroutine(decayCoroutine);

        decayCoroutine = StartCoroutine(DecayCountdown());
    }

    void RestartDecayTimer()
    {
        StartDecayTimer(); 
    }

    IEnumerator DecayCountdown()
    {
        float timer = decayDuration;
        multiplierTimerBar.gameObject.SetActive(true);
        multiplierTimerBar.maxValue = decayDuration;
        multiplierTimerBar.value = decayDuration;

        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            multiplierTimerBar.value = timer;
            yield return null;
        }

        multiplierTimerBar.gameObject.SetActive(false);
        ResetMultiplier();
    }
}
