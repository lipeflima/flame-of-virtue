using UnityEngine;
using UnityEngine.UI; // Para controlar o botão de interação (se necessário)

public class AreaBossKeyUnlocker : MonoBehaviour
{
    public GameObject interactionUI; // UI que mostra o "Pressione [E]"
    public GameObject bossKey; // O objeto da Boss Key que será ativado
    public PlayerInventory playerInventory; // Referência ao inventário do player

    public string[] requiredItems; // Nomes dos 4 itens necessários

    private bool playerInArea = false;

    private void Start()
    {
        interactionUI.SetActive(false); // Garante que o botão de interação não aparece no começo
        bossKey.SetActive(false); // Garante que a boss key começa desativada
    }

    private void Update()
    {
        if (playerInArea)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                TryUnlockBossKey();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactionUI.SetActive(true);
            playerInArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactionUI.SetActive(false);
            playerInArea = false;
        }
    }

    private void TryUnlockBossKey()
    {
        if (HasRequiredItems())
        {
            bossKey.SetActive(true);
            interactionUI.SetActive(false); // Oculta a UI após conseguir
            this.enabled = false; // Desativa o script para não interagir novamente
        }
        else
        {
            GetComponent<HintUIManager>().MostrarAviso();
        }
    }

    private bool HasRequiredItems()
    {
        foreach (string itemName in requiredItems)
        {
            if (!playerInventory.HasItem(itemName))
            {
                return false;
            }
        }
        return true;
    }
}
