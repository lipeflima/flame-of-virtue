using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Image[] healthSegments; // Array com 3 imagens (seções da barra)
    private float maxHealth;
    private TheJudgeBoss boss;

    void Start()
    {
        boss = FindObjectOfType<TheJudgeBoss>();
        maxHealth = boss.maxHealth;
    }

    void Update()
    {
        float currentHealth = boss.GetCurrentHealth(); // você pode criar um getter
        float segmentHealth = maxHealth / 3f;

        for (int i = 0; i < 3; i++)
        {
            float healthInSegment = Mathf.Clamp(currentHealth - (segmentHealth * i), 0, segmentHealth);
            healthSegments[i].fillAmount = healthInSegment / segmentHealth;
        }
    }
}
