using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComboSystem : MonoBehaviour
{
    private float currentHit;
    public float maxHit = 100f;
    private bool canUseSpecial = false;
    public Slider comboBar;
    // HEADER Multiplier
    public TMP_Text multiplierUI;
    public TMP_Text multiplierStatusUI;
    private int multiplierCount = 1;
    private bool multiplierApplied = false;
    private float decayDuration = 5f;
    private Coroutine decayCoroutine;   
    private Coroutine pulseCoroutine;
    public Slider multiplierTimerBar;
    // HEADER Especial Gauger
    public Image fillImage;
    public Image cooldownOverlay;
    public float cooldownDuration = 5f;
    private float cooldownTimer = 0f;
    private bool isInCooldown = false;
    private bool specialEnabled = false;

    void Start()
    {
        UpdateMaxHit();
    }

    void Update()
    {
        CheckHit();
        UpdateSpecialUI();
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
                if (!isInCooldown) canUseSpecial = true;
                multiplierCount++;
                multiplierApplied = true;
                UpdateMaxHit();
                StartDecayTimer();
                EnableSpecial();
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
            case 1: multiplierStatusUI.text = "Fine"; break;
            case 2: multiplierStatusUI.text = "Cool"; break;
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

    public void ResetMultiplier()
    {
        currentHit = 0;
        multiplierCount = 1;
        UpdateUI();
        multiplierTimerBar.gameObject.SetActive(false); // esconde a barra
        multiplierTimerBar.value = decayDuration; // reseta valor
        UpdateMaxHit();
        ResetSpecial();
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

    public void EnableSpecial()
    {
        specialEnabled = true;
        fillImage.fillAmount = 1f;
    }

    public void StartCooldown()
    {
        isInCooldown = true;
        cooldownTimer = cooldownDuration;
        cooldownOverlay.fillAmount = 1f;
    }

    public void ResetSpecial()
    {
        specialEnabled = false;
        isInCooldown = false;
        fillImage.fillAmount = 0f;
        cooldownOverlay.fillAmount = 0f;
    }

    private void UpdateSpecialUI()
    {
        if (isInCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            cooldownOverlay.fillAmount = cooldownTimer / cooldownDuration;

            if (cooldownTimer <= 0f)
            {
                isInCooldown = false;
                cooldownOverlay.fillAmount = 0f;
                if (specialEnabled) canUseSpecial = true;
            }
        }
    }

    public void UseSpecial()
    {
        canUseSpecial = false;
        if (specialEnabled && !isInCooldown)
        {
            // Executar o golpe especial aqui
            StartCooldown();
        }
    }
    
    public bool GetSpecialStatus()
    {
        return canUseSpecial;
    }
}
