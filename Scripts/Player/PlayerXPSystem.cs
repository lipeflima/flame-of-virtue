using UnityEngine;

public class PlayerXPSystem : MonoBehaviour
{
    public static PlayerXPSystem Instance;

    public int currentXP;
    public int currentLevel = 1;
    public int xpToNextLevel = 10;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void GainXP(int amount)
    {
        currentXP += amount;
        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        currentXP -= xpToNextLevel;
        currentLevel++;
        xpToNextLevel += Mathf.RoundToInt(xpToNextLevel * 0.5f); // aumenta o custo por nível
        Debug.Log("Level Up! Novo nível: " + currentLevel);
        // Aplique melhorias aqui, tipo aumento de stats
    }
}
