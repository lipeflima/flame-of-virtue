using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogues : TriggerManager
{
    [Header("UI Config")]
    [Tooltip("The UI GameObject for dialogues.")]
    public GameObject dialogueUI;
    public GameObject[] dialogues;
    int dialogueIndex;
    public bool isInteractive;
    public override void ManageObjectActivation(bool status)
    {        
        if (!status)
        {
            // Deactivate the UI directly if status is false
            SetDialogueUIActive(false);
            if (hasCinematics && cinematics != null) cinematics.SetActive(false);
        }
    }
    public override void CheckInputPress()
    {
        base.CheckInputPress();

        // Check input and activation status
        if (CheckInput() && !isActivated)
        {
            SetDialogueUIActive(true);
            isActivated = true;
            if (hasPromptActivation && prompt != null) prompt.SetActive(false);
            if(hasCinematics && cinematics != null) cinematics.SetActive(true);
        }
    }
    private bool CheckInput()
    {
        // Ensure inputHandler is valid before accessing inputs
        return inputHandler != null && inputHandler.InteractInput;
    }

    private void SetDialogueUIActive(bool active)
    {
        if (dialogueUI != null)
        {
            dialogueUI.SetActive(active);
        }
        else
        {
            Debug.LogWarning("Dialogue UI GameObject is not assigned.");
        }
    }
    public void SetDialogue(int flow)
    {
        // Ajusta o índice garantindo que fique dentro dos limites válidos
        dialogueIndex = Mathf.Clamp(dialogueIndex + flow, 0, dialogues.Length - 1);

        // Atualiza os diálogos com base no índice
        UpdateDialogues();
    }

    private void UpdateDialogues()
    {
        // Ativa apenas o diálogo no índice atual e desativa os demais
        for (int i = 0; i < dialogues.Length; i++)
        {
            dialogues[i].SetActive(i == dialogueIndex);
        }
    }

}
