using UnityEngine;

public class ItemColetavel : MonoBehaviour
{
    public string itemName;
    public float amount = 10f; // Quantidade de energia que o item recupera

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CheckItem(other);
        }
    }
    void CheckItem(Collider2D other)
    {
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
                PlayerInventory inventory = other.gameObject.GetComponent<PlayerInventory>();

            if(inventory != null)
            {
                inventory.AddGold(amount);
            }            

            break;
        }

        Destroy(gameObject); // Remove o item do cenário
    }
}
