using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HordeRoomManager : MonoBehaviour
{
    [Header("Player Detection")]
    public Collider2D roomTrigger; // Trigger para detectar o player entrando
    private bool playerInside = false;

    [Header("Barrier")]
    public List<GameObject> barriers; // Objeto da barreira que fecha a sala

    [Header("Horde Settings")]
    public List<Horde> hordes = new List<Horde>(); // Lista de hordas configuráveis
    private int currentHordeIndex = 0;
    private bool spawningHorde = false;

    private List<GameObject> activeEnemies = new List<GameObject>(); // Lista para controlar inimigos vivos

    [Header("Horde UI")]
    public TMP_Text totalActiveEnemiesText;
    public TMP_Text activeEnemiesText;
    public GameObject hordCountUI;

    void Start()
    {
        hordCountUI.SetActive(false);
        SetBarriers(false);
    }

    void Update()
    {
        if (playerInside && !spawningHorde && activeEnemies.Count == 0)
        {
            if (currentHordeIndex < hordes.Count)
            {
                StartCoroutine(SpawnHorde(hordes[currentHordeIndex]));
            }
            else
            {
                // Todas hordas derrotadas, liberar a barreira
                SetBarriers(false);
                hordCountUI.SetActive(false);
            }
        }

        // Limpa inimigos mortos da lista
        activeEnemies.RemoveAll(enemy => enemy == null);

        activeEnemiesText.text = "" + activeEnemies.Count;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = true;
            // Fecha a sala
            SetBarriers(true);
        }
    }

    IEnumerator SpawnHorde(Horde horde)
    {
        spawningHorde = true;

        foreach (var enemyData in horde.enemies)
        {
            for (int i = 0; i < enemyData.amount; i++)
            {
                Vector2 spawnPosition = GetRandomSpawnPosition();
                GameObject enemy = Instantiate(enemyData.enemyPrefab, spawnPosition, Quaternion.identity);
                activeEnemies.Add(enemy);
                yield return new WaitForSeconds(0.2f); // Delay entre cada inimigo spawnado
            }
        }

        currentHordeIndex++;
        spawningHorde = false;
        totalActiveEnemiesText.text = "/" + activeEnemies.Count;
        hordCountUI.SetActive(true);
    }

    Vector2 GetRandomSpawnPosition()
    {
        // Gera uma posição aleatória dentro da sala
        Vector2 roomCenter = roomTrigger.bounds.center;
        Vector2 roomSize = roomTrigger.bounds.size;

        float x = Random.Range(roomCenter.x - roomSize.x / 2 + 1f, roomCenter.x + roomSize.x / 2 - 1f);
        float y = Random.Range(roomCenter.y - roomSize.y / 2 + 1f, roomCenter.y + roomSize.y / 2 - 1f);

        return new Vector2(x, y);
    }

    private void SetBarriers(bool status)
    {
        foreach(var barrier in barriers)
        {
            if (barrier != null)
                barrier.SetActive(status);
        }
    }
}

[System.Serializable]
public class Horde
{
    public List<EnemyData> enemies; // Lista de inimigos na horda
}

[System.Serializable]
public class EnemyData
{
    public GameObject enemyPrefab; // Prefab do inimigo
    public int amount;             // Quantidade de inimigos desse tipo
}
