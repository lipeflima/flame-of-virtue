using UnityEngine;

public class ItemColetavel : MonoBehaviour
{
    public string itemName;
    public float amount = 10f; // Quantidade de energia que o item recupera
    public float moveSpeed = 5f;
    public float triggerDistance = 8f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) < triggerDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CheckItem(other);
            GetComponent<ItemIndicator>().ShowItemIndicator(itemName, (int)amount);
        }
    }
    void CheckItem(Collider2D other)
    {
        PlayerInventory inventory = other.GetComponent<PlayerInventory>();

        switch(itemName)
        {
            case "Energia":
                EnergySystem energia = other.GetComponent<EnergySystem>();

                if (energia != null)
                {
                    energia.AddEnergy(amount);
                }
            break;

            case "Gold":
                if(inventory != null)
                {
                    inventory.AddGold(amount);
                }           
            break;

            case "Dove":
                ObjectiveManager.instance.CompleteObjective(0);
                if(inventory != null)
                {
                    inventory.AddItem("Dove");
                }
            break;

            case "Water":
                ObjectiveManager.instance.CompleteObjective(1);
                if(inventory != null)
                {
                    inventory.AddItem("Water");
                }
            break;

            case "Lamb":
                ObjectiveManager.instance.CompleteObjective(2);
                if(inventory != null)
                {
                    inventory.AddItem("Lamb");
                }
            break;

            case "Heart":
                ObjectiveManager.instance.CompleteObjective(3);
                if(inventory != null)
                {
                    inventory.AddItem("Heart");
                }
            break;

            case "BossKey":
                if(inventory != null)
                {
                    inventory.AddItem("BossKey");
                }
            break;
        }

        Destroy(gameObject); // Remove o item do cenário
    }
}
