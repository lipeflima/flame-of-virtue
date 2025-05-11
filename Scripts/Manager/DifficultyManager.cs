using UnityEngine;
using UnityEngine.UI;

public enum DifficultyLevel
{
    Easy,
    Moderate,
    Hard,
    Hardcore,
    Godmode
}

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance;

    [Header("Tempo Total (segundos)")]
    public float totalTime = 1500f; // 25 minutos

    [Header("UI")]
    public Image difficultyBar; // Image com tipo 'Filled'
    public Gradient difficultyColor; // De azul a vermelho

    private float elapsedTime = 0f;
    public DifficultyLevel currentLevel { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        float progress = Mathf.Clamp01(elapsedTime / totalTime);
        UpdateDifficulty(progress);
        UpdateUI(progress);
    }

    void UpdateDifficulty(float progress)
    {
        if (progress < 0.2f)
            currentLevel = DifficultyLevel.Easy;
        else if (progress < 0.4f)
            currentLevel = DifficultyLevel.Moderate;
        else if (progress < 0.6f)
            currentLevel = DifficultyLevel.Hard;
        else if (progress < 0.8f)
            currentLevel = DifficultyLevel.Hardcore;
        else
            currentLevel = DifficultyLevel.Godmode;
    }

    void UpdateUI(float progress)
    {
        if (difficultyBar != null)
        {
            difficultyBar.fillAmount = progress;
            difficultyBar.color = difficultyColor.Evaluate(progress);
        }
    }

    public int GetLevel()
    {
        switch(currentLevel)
        {
            case DifficultyLevel.Easy:
                return 0;
            case DifficultyLevel.Moderate:
                return 3;
            case DifficultyLevel.Hard:
                return 5;
            case DifficultyLevel.Hardcore:
                return 7;
            default:
                return 10;
        }
    }
}
