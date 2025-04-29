using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    [Header("Player Settings")]
    public Transform player; // Referência ao player
    public PlayerController playerController; // Script de controle do player

    [Header("Interaction Settings")]
    public Transform interactionPoint; // Posição onde o player deve ficar
    public List<string> dialogueLines = new List<string>(); // Linhas de diálogo
    public float dialogueDelay = 2f; // Tempo entre cada fala

    [Header("NPC Settings")]
    public GameObject npc; // NPC que desaparecerá no final

    [Header("UI Settings")]
    public SpeechBubbleUI speechBubbleUI; // Referência para o balão

    private bool interactionStarted = false;

    private bool isTalking = false;
    private int currentDialogueIndex = 0;

    void Update()
    {
        if (isTalking && Input.GetKeyDown(KeyCode.E))
        {
            AdvanceDialogue();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !interactionStarted)
        {
            interactionStarted = true;
            StartCoroutine(HandleInteraction());
        }
    }

    IEnumerator HandleInteraction()
    {
        // Move o player para o ponto de interação
        player.position = interactionPoint.position;

        // Desativa o controle do player
        if (playerController != null)
            playerController.enabled = false;

         isTalking = true;
        currentDialogueIndex = 0;

        if (speechBubbleUI != null)
            speechBubbleUI.ShowSpeech(dialogueLines[currentDialogueIndex]);

        yield return null;
    }

    void AdvanceDialogue()
    {
        currentDialogueIndex++;

        if (currentDialogueIndex >= dialogueLines.Count)
        {
            EndInteraction();
        }
        else
        {
            if (speechBubbleUI != null)
                speechBubbleUI.ShowSpeech(dialogueLines[currentDialogueIndex]);
        }
    }

    void EndInteraction()
    {
        isTalking = false;

        if (speechBubbleUI != null)
            speechBubbleUI.HideSpeech();

        if (npc != null)
            Destroy(npc);

        if (playerController != null)
            playerController.enabled = true;
    }
}
