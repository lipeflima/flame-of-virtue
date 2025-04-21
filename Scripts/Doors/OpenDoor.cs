using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [Header("Mecanics")]
    public GameObject portaoVisual;       // sprite do portão
    public Collider2D portaoCollider;     // colisor do portão
    [SerializeField] private bool playerNearby = false;
    private PlayerInventory playerInventory;

    [Header("UI")]
    public HintUIManager chaveUIManager;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (playerInventory != null && playerInventory.hasKey)
            {
                AbrirPortao();
            }
            else 
            {
                chaveUIManager?.MostrarAviso();
            }
        }
    }

    private void AbrirPortao()
    {
        portaoVisual.GetComponent<Animator>().SetBool("open", true);
        portaoCollider.enabled = false;   // desativa o colisor
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            playerInventory = other.GetComponent<PlayerInventory>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            playerInventory = null;
        }
    }
}
