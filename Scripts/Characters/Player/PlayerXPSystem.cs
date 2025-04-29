using UnityEngine;
using UnityEngine.UI;

public class PlayerXP : MonoBehaviour
{
    public int currentLevel = 1;
    public int currentXP = 0;
    public int xpToNextLevel;
    
    public Image xpBarFill;

    void Start()
    {
        xpToNextLevel = GetXPToLevel(currentLevel);
        UpdateXPBar();
    }

    public void AddXP(int amount)
    {
        currentXP += amount;

        while (currentXP >= xpToNextLevel && currentLevel < 10)
        {
            currentXP -= xpToNextLevel;
            currentLevel++;
            xpToNextLevel = GetXPToLevel(currentLevel);
            // UnlockSkills();
            // Aqui você pode adicionar efeitos, som, etc.
        }

        UpdateXPBar();
    }

    public void UpdateXPBar()
    {
        float fillAmount = (float)currentXP / xpToNextLevel;
        xpBarFill.fillAmount = fillAmount;
    }

    int GetXPToLevel(int level)
    {
        return Mathf.FloorToInt(100 * Mathf.Pow(level, 1.5f));
    }

    /* void UnlockSkills()
    {
        switch (currentLevel)
        {
            case 2:
                FireBallSkill.instance.SetFireBallSkill(true);
                break;
        }
    } */
}
