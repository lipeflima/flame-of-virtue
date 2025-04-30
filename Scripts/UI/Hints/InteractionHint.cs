using UnityEngine;

public class InteractionHint : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject interactionUI; // UI que mostra o "Pressione [E]"
    private bool playerInArea = false;

    private void Start()
    {
        interactionUI.SetActive(false); // Garante que o botão de interação não aparece no começo
    }

    private void Update()
    {
        if (playerInArea)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GetComponent<HintUIManager>().MostrarAviso();
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
}
