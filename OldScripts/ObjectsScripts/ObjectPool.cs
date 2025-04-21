using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public static ObjectPool Instance { get; private set; }

    public List<Pool> pools;
    private Dictionary<string, Queue<GameObject>> poolDictionary;
    public Transform container;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Garante que apenas uma instância do pool exista
            return;
        }
    }

    private void Start()
    {
        InitializePools();
    }

    private void InitializePools()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (var pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            if (pool.prefab == null)
            {
                Debug.LogError($"Prefab for tag '{pool.tag}' is missing.");
                continue;
            }

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, container);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (poolDictionary.TryGetValue(tag, out Queue<GameObject> objectPool))
        {
            GameObject objectToSpawn = objectPool.Dequeue();

            objectToSpawn.transform.SetPositionAndRotation(position, rotation);
            objectToSpawn.SetActive(true);

            // Obtém todos os componentes que implementam IObjectPool
            IObjectPool[] pooledObjects = objectToSpawn.GetComponents<IObjectPool>();

            if (pooledObjects.Length > 0)
            {
                // Executa OnObjectSpawn em cada um dos componentes
                foreach (var pooledObj in pooledObjects)
                {
                    pooledObj.OnObjectSpawn();
                }
            }
            else
            {
                Debug.LogError("Nenhum componente IObjectPool foi encontrado no objeto.");
            }

            objectPool.Enqueue(objectToSpawn);
            return objectToSpawn;
        }

        Debug.LogWarning($"Pool with tag '{tag}' doesn't exist.");
        return null;
    }

}
