using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerXP : MonoBehaviour
{
    public PlayerData playerData;
    public int currentLevel = 1;
    public int currentXP = 0;
    public int xpToNextLevel;
    
    public Image xpBarFill;
    public TMP_Text currentXpText;
    public TMP_Text xpToNextLevelText;
    public TMP_Text currentLevelText;
    public LevelUpManager levelUpManager;

    void Start()
    {
        xpToNextLevel = GetXPToLevel(currentLevel);
        UpdateXPBar();
    }

    void Update()
    {
        currentLevelText.text = "" + currentLevel;    
    }

    public void AddXP(int amount)
    {
        currentXP += amount;

        while (currentXP >= xpToNextLevel && currentLevel < 10)
        {
            LevelUp();
        }

        UpdateXPBar();
    }

    public void UpdateXPBar()
    {
        float fillAmount = (float)currentXP / xpToNextLevel;
        xpBarFill.fillAmount = fillAmount;
        currentXpText.text = "XP: " + currentXP;
        xpToNextLevelText.text = "/ " + xpToNextLevel;
    }

    private void LevelUp()
    {
        // currentXP -= xpToNextLevel;
        currentLevel++;
        ApplyLevelUpBonuses();
        xpToNextLevel = GetXPToLevel(currentLevel);
        levelUpManager.ShowText(gameObject.transform);
        levelUpManager.PlayLevelUpEffect();
    }

    int GetXPToLevel(int level)
    {
        return Mathf.FloorToInt(100 * Mathf.Pow(level, 1.5f));
    }

    private void ApplyLevelUpBonuses()
    {
        int bonusHP = Mathf.FloorToInt(50 + (currentLevel * 25)); // Exemplo: crescimento progressivo
        EnergySystem health = GetComponent<EnergySystem>();

        if (health != null)
        {
            health.IncreaseMaxHealth(bonusHP);
            health.RestoreToMax(); // opcional, cura ao subir de nível
        }

        // Aqui você pode adicionar outros bônus facilmente futuramente, ex:
        // IncreaseDamage(1);
        // IncreaseMoveSpeed(0.1f);
    }
}
