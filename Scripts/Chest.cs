using UnityEngine;

public class Chest : MonoBehaviour
{
    [Header("Itens a serem gerados")]
    public GameObject[] itemsPrefabs;

    [Header("Configuração de Spawn")]
    public float maxDistance = 2f; // Distância máxima para espalhar os itens
    public float launchForce = 5f; // Força do impulso inicial

    void Update()
    {
        CheckInput();
    }

    void CheckInput()
    {
        if(interactInput)
        {

        }
    }

    public override void OnEnter()
    {

    }
    public override void OnStay()
    {
        
    }
    public override void OnExit(){}

    public void OpenChest()
    {
        foreach (GameObject itemPrefab in itemsPrefabs)
        {
            // Gera uma direção aleatória
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            // Calcula uma posição aleatória dentro da distância máxima
            Vector3 spawnPosition = transform.position + (Vector3)(randomDirection * Random.Range(0.5f, maxDistance));

            // Instancia o item
            GameObject spawnedItem = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);

            // Se o item tiver Rigidbody2D, aplicamos impulso
            Rigidbody2D rb = spawnedItem.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(randomDirection * launchForce, ForceMode2D.Impulse);
            }
        }
    }
}
