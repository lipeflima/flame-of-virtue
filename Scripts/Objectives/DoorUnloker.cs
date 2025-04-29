using UnityEngine;

public class DoorUnloker : MonoBehaviour
{
    public GameObject interactionUI; // UI que mostra o "Pressione [E]"
    public GameObject lockedDoor; // O objeto da Boss Key que será ativado
    public PlayerInventory playerInventory; // Referência ao inventário do player

    public string[] requiredItems; // Nomes dos 4 itens necessários

    private bool playerInArea = false;

    private void Start()
    {
        interactionUI.SetActive(false); // Garante que o botão de interação não aparece no começo
        if (lockedDoor != null) lockedDoor.SetActive(false); // Garante que a boss key começa desativada
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
            if (lockedDoor != null) lockedDoor.SetActive(true);
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
