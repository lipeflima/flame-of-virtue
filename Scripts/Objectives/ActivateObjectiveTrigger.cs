using System.Collections.Generic;
using UnityEngine;

public class ActivateObjectiveTrigger : MonoBehaviour
{
    [Header("ID do objetivo que será ativado")]
    public List<int> objectivesIndexes;

    [Header("Referência para o ObjectiveManager")]
    public ObjectiveManager objectiveManager;

    [Header("Configurações")]
    public bool deactivateTriggerAfterActivation = true; // Desativa o trigger após usar?

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (objectiveManager != null)
            {
                foreach (var objectiveIndex in objectivesIndexes)
                {
                    objectiveManager.ActivateObjective(objectiveIndex);
                    Debug.Log("Objetivo " + objectiveIndex + " ativado!");

                    if (deactivateTriggerAfterActivation)
                    {
                        gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                Debug.LogWarning("ObjectiveManager não atribuído no Trigger!");
            }
        }
    }
}
