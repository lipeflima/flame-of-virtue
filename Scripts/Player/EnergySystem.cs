using UnityEngine;
using UnityEngine.UI;

public class EnergySystem : MonoBehaviour
{
    public Image energyBar; // Referência ao componente Image da barra
    public float maxEnergy = 100f;
    public float currentEnergy;
    private float energyDrainRate = 100f / 300f; // 100 unidades em 5 min (300 segundos)
    private bool alive, damaged;

    void Start()
    {
        currentEnergy = maxEnergy;
        alive = true;
    }

    void Update()
    {
        // Reduz energia com o tempo
        if(currentEnergy <= 0) return;

        currentEnergy -= energyDrainRate * Time.deltaTime;
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);

        UpdateEnergyBar();
    }

    void UpdateEnergyBar()
    {
        float fillAmount = currentEnergy / maxEnergy;
        energyBar.fillAmount = fillAmount;

        // Atualiza a cor conforme a energia
        if (fillAmount > 0.5f)
        {
            energyBar.color = new Color(0.93f, 0.93f, 0.29f); // Amarelo
        }
        else if (fillAmount > 0.25f)
        {
            energyBar.color = new Color(0.93f, 0.71f, 0.29f); // Laranja
        }
        else
        {
            energyBar.color = new Color(0.93f, 0.29f, 0.34f); // Vermelho
        }
    }

    public void AddEnergy(float amount)
    {
        currentEnergy += amount;
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
    }

    public void DecreaseEnergy(float amount)
    {
        currentEnergy -= amount;
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
        gameObject.GetComponent<DamageIndicator>().MostrarIndicadorDeDano(amount);
    }
    public bool GetLifeStatus()
    {
        return alive;
    }
    public bool GetDamagedStatus()
    {
        return damaged;
    }
}