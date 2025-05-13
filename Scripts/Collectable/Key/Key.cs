using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private KeyType keyType;
    public enum KeyType { RustedKey, GoldKey, SilverKey, BossKey }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();

            if (inventory != null)
            {
                switch(keyType)
                {
                    case KeyType.RustedKey:
                        inventory.AddItem("RustedKey");
                    break;
                    case KeyType.SilverKey:
                        inventory.AddItem("SilverKey");
                    break;
                    case KeyType.GoldKey:
                        inventory.AddItem("GoldKey");
                    break;
                    case KeyType.BossKey:
                        inventory.AddItem("BossKey");
                    break;
                }
                
                Destroy(gameObject); // Remove a chave do mapa
            }
        }
    }
}
