using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyIndicator : MonoBehaviour
{
    public GameObject arrowPrefab; // Prefab da seta (UI Image)
    public RectTransform arrowsContainer; // Um objeto vazio no Canvas para organizar as setas
    public Transform player; // Transform do jogador
    public Camera mainCamera;

    public float edgeOffset = 50f; // Distância da borda para a seta
    private List<GameObject> activeArrows = new List<GameObject>();
    private List<Transform> trackedEnemies = new List<Transform>();

    void Update()
    {
        // Atualiza as setas apenas se houver 5 inimigos ou menos
        if (trackedEnemies.Count <= 5)
        {
            UpdateArrows();
        }
        else
        {
            ClearArrows();
        }
    }

    public void SetTrackedEnemies(List<GameObject> enemies)
    {
        trackedEnemies.Clear();
        foreach (GameObject e in enemies)
        {
            if (e != null)
                trackedEnemies.Add(e.transform);
        }
    }

    void UpdateArrows()
    {
        // Garante número certo de setas
        while (activeArrows.Count < trackedEnemies.Count)
        {
            GameObject arrow = Instantiate(arrowPrefab, arrowsContainer);
            activeArrows.Add(arrow);
        }
        while (activeArrows.Count > trackedEnemies.Count)
        {
            Destroy(activeArrows[activeArrows.Count - 1]);
            activeArrows.RemoveAt(activeArrows.Count - 1);
        }

        for (int i = 0; i < trackedEnemies.Count; i++)
        {
            Transform enemy = trackedEnemies[i];
            GameObject arrow = activeArrows[i];

            if (enemy == null)
            {
                arrow.SetActive(false);
                continue;
            }

            Vector3 screenPos = mainCamera.WorldToScreenPoint(enemy.position);

            // Se inimigo está na tela, oculta seta
            if (screenPos.z > 0 && screenPos.x > 0 && screenPos.x < Screen.width && screenPos.y > 0 && screenPos.y < Screen.height)
            {
                arrow.SetActive(false);
                continue;
            }

            arrow.SetActive(true);

            Vector3 direction = (enemy.position - player.position).normalized;

            // Posiciona a seta nas bordas da tela
            Vector3 center = new Vector3(Screen.width, Screen.height, 0) / 2;
            Vector3 cappedPos = center + new Vector3(direction.x, direction.y, 0) * ((Screen.height / 2) - edgeOffset);
            RectTransform arrowRect = arrow.GetComponent<RectTransform>();
            arrowRect.position = cappedPos;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            arrowRect.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
    }

    void ClearArrows()
    {
        foreach (var arrow in activeArrows)
        {
            if (arrow != null)
                arrow.SetActive(false);
        }
    }
}
