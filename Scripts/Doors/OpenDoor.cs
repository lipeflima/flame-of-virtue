using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [Header("Mecanics")]
    public GameObject portaoVisual;       // sprite do portão
    public Collider2D portaoCollider;     // colisor do portão
    [SerializeField] private bool playerNearby = false;
    private PlayerInventory playerInventory;
    [SerializeField] private Key.KeyType keyType;

    [Header("UI")]
    public HintUIManager chaveUIManager;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (playerInventory != null)
            {
                if ((keyType == Key.KeyType.RustedKey) && playerInventory.hasRustedKey) 
                {
                    AbrirPortao();
                }
                else if ((keyType == Key.KeyType.SilverKey) && playerInventory.hasSilverKey)
                {
                    AbrirPortao();
                }
                else if ((keyType == Key.KeyType.GoldKey) && playerInventory.hasGoldKey)
                {
                    AbrirPortao();
                }
                else 
                {
                    chaveUIManager?.MostrarAviso();
                }
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
