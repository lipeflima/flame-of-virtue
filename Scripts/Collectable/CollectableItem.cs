using UnityEngine;

public class ItemColetavel : MonoBehaviour
{
    public enum ItemType { Gem, GemFragment, Gold, Key, Energy }
    public ItemType itemType;
    public string itemName;
    public float amount = 10f;
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

        if (inventory != null)
        {
            switch(itemType)
            {
                case ItemType.Energy:
                    EnergySystem energia = other.GetComponent<EnergySystem>();

                    if (energia != null)
                    {
                        energia.AddEnergy(amount);
                    }
                break;

                case ItemType.Gold:
                    inventory.AddGold(amount);          
                break;
                case ItemType.Gem:
                    GemSO gem = GetComponent<GemData>().gemData;

                    if (gem != null)
                    {
                        inventory.AddGem(gem);
                    }
                break;
                case ItemType.GemFragment:
                    GemSO gemParent = GetComponent<GemData>().gemData;
                    if (gemParent != null)
                    {
                        GemFragment fragmentInstance = new GemFragment(gemParent, player.GetComponent<PlayerXP>().currentLevel);
                        inventory.AddGemFragment(fragmentInstance);
                    }
                break;
                default:
                    inventory.AddItem(itemName);
                break;
            }

            Destroy(gameObject); // Remove o item do cenário
        }
        else {
            Debug.Log("Inventario não existente!");
        }
    }
}
