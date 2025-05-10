using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [Header("Referências")]
    public Transform player;
    public List<GameObject> enemyPrefabs;

    [Header("Spawn Settings")]
    public float spawnRadius = 10f;
    public float spawnInterval = 3f;
    public int maxActiveEnemies = 20;
    public AnimationCurve spawnRateOverTime;
    public int maxNavMeshAttempts = 10;

    private float timer;
    private float startTime;
    private List<GameObject> activeEnemies = new List<GameObject>();

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        startTime = Time.time;
    }

    void Update()
    {
        activeEnemies.RemoveAll(e => e == null);

        if (activeEnemies.Count >= maxActiveEnemies) return;

        float elapsedTime = Time.time - startTime;
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

            // Valida se está sobre NavMesh
            if (NavMesh.SamplePosition(spawnPos, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
            {
                GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
                GameObject newEnemy = Instantiate(prefab, hit.position, Quaternion.identity);
                activeEnemies.Add(newEnemy);
                return; // sucesso
            }
        }

        Debug.LogWarning("Falha ao encontrar posição válida no NavMesh para spawn.");
    }

    Vector3 GetRandomPositionAroundPlayer()
    {
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        Vector2 offset = randomDir * Random.Range(spawnRadius * 0.8f, spawnRadius);
        Vector3 candidatePos = player.position + new Vector3(offset.x, offset.y, 0);
        return candidatePos;
    }
}
