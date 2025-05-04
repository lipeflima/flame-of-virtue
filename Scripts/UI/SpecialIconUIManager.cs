using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialIconUIManager : MonoBehaviour
{
    public GameObject flameIconPrefab; // prefab da chama
    public Transform iconContainer;    // container onde as chamas aparecem
    public int maxFlames = 8;          // número máximo de níveis/ícones
    private List<GameObject> activeFlames = new();

    public void UpdateFlameIcons(int level)
    {
        ClearFlameIcons();

        float radius = 70f; // distância do centro (ajuste conforme seu layout)
        for (int i = 0; i < level; i++)
        {
            float angle = 360f / maxFlames * i * Mathf.Deg2Rad;
            Vector3 pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * radius;

            GameObject icon = Instantiate(flameIconPrefab, iconContainer);
            icon.GetComponent<RectTransform>().anchoredPosition = pos;
            activeFlames.Add(icon);
        }
    }

    public void ClearFlameIcons()
    {
        foreach (var icon in activeFlames)
        {
            Destroy(icon);
        }
        activeFlames.Clear();
    }
}

