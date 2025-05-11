using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class SpawnableEnemy
{
    public GameObject prefab;
    public int maxPerType = 5;
    public int minDifficultyLevel = 0;
    [HideInInspector] public int currentCount = 0;
}

public class EnemySpawner : MonoBehaviour
{
    [Header("Referências")]

    [Header("Configuração de Inimigos")]
    public List<SpawnableEnemy> enemiesToSpawn = new List<SpawnableEnemy>();

    [Header("Spawn Settings")]
    public float spawnRadius = 10f;
    public float spawnInterval = 3f;
    public int maxActiveEnemies = 20;
    public AnimationCurve spawnRateOverTime;
    public int maxNavMeshAttempts = 10;
    private bool playerInside = false;

    [Header("Dificuldade")]
    public int currentDifficultyLevel = 0; // Ou pegue de DifficultyManager.Instance.difficulty

    private float timer;
    private float startTime;
    private List<GameObject> activeEnemies = new List<GameObject>();

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if (!playerInside) return; 

        activeEnemies.RemoveAll(e => e == null);
        float elapsedTime = Time.time - startTime;

        // Aumenta a quantidade permitida com o tempo (ex: +1 a cada 20s)
        int scaledMaxEnemies = Mathf.Min(maxActiveEnemies, Mathf.FloorToInt(elapsedTime / 20f) + 3);

        if (activeEnemies.Count >= scaledMaxEnemies) return;

        float currentInterval = spawnInterval / Mathf.Max(0.1f, spawnRateOverTime.Evaluate(elapsedTime));
        timer += Time.deltaTime;

        if (timer >= currentInterval)
        {
            TrySpawnEnemyOnNavMesh();
            timer = 0f;
        }
    }

    void TrySpawnEnemyOnNavMesh()
    {
        for (int i = 0; i < maxNavMeshAttempts; i++)
        {
            Vector3 spawnPos = GetRandomPositionAroundPlayer();

            if (NavMesh.SamplePosition(spawnPos, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
            {
                currentDifficultyLevel = DifficultyManager.Instance.GetLevel();

                List<SpawnableEnemy> valid = enemiesToSpawn.FindAll(e =>
                    currentDifficultyLevel >= e.minDifficultyLevel && e.currentCount < e.maxPerType);

                if (valid.Count == 0) return;

                SpawnableEnemy chosen = valid[Random.Range(0, valid.Count)];
                GameObject newEnemy = Instantiate(chosen.prefab, hit.position, Quaternion.identity);
                newEnemy.GetComponent<EnemyHP>().sourceData = chosen;
                chosen.currentCount++;
                activeEnemies.Add(newEnemy);
                return;
            }
        }

        Debug.LogWarning("Falha ao encontrar posição válida no NavMesh para spawn.");
    }

    Vector3 GetRandomPositionAroundPlayer()
    {
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        Vector2 offset = randomDir * Random.Range(spawnRadius * 0.8f, spawnRadius);
        Vector3 candidatePos = transform.position + new Vector3(offset.x, offset.y, 0);
        return candidatePos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInside = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInside = false;
    }
}
