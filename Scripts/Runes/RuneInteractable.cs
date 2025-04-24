using UnityEngine;
using TMPro;

public class RuneInteractable : MonoBehaviour
{
    public string storyText;
    public GameObject interactionPrompt; // UI do botão "E"
    public GameObject storyPanel;        // UI da história
    public TextMeshProUGUI storyTextUI;

    public bool playerInRange = false;
    private bool isReading = false;

    void Start()
    {
        interactionPrompt.SetActive(false);
        storyPanel.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            isReading = !isReading;
            storyPanel.SetActive(isReading);
            storyTextUI.text = storyText;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            interactionPrompt.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            interactionPrompt.SetActive(false);
            storyPanel.SetActive(false);
            isReading = false;
        }
    }
}
