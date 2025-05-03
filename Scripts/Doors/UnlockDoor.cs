using UnityEngine;

public class UnlockDoor : MonoBehaviour
{
    public GameObject interactionUI; // UI do botão a ser pressionado
    public GameObject lockedDoor; // Porta que será ativada ao adquirir itens requeridos
    public PlayerInventory playerInventory;
    public string[] requiredItems; // Itens requeridos
    private bool playerInArea = false;

    private void Start()
    {
        interactionUI.SetActive(false); 
        if (lockedDoor != null) lockedDoor.SetActive(false); 
    }

    private void Update()
    {
        if (playerInArea)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                TryUnlockDoor();
            }
        }
    }

    private void TryUnlockDoor()
    {
        if (HasRequiredItems())
        {
            Unlock();
        }
        else
        {
            GetComponent<HintUIManager>().ShowHint(false);
        }
    }

    public void Unlock()
    {
        if (lockedDoor != null) lockedDoor.SetActive(true);
        GetComponent<HintUIManager>().ShowHint(true);
        interactionUI.SetActive(false); // Oculta a UI após conseguir
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        this.enabled = false;
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactionUI.SetActive(true);
            playerInArea = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactionUI.SetActive(false);
            playerInArea = false;
        }
    }
}
