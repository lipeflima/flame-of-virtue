using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager instance;

    [Header("Lista de todos os objetivos (definidos no Inspector)")]
    public List<Objective> allObjectives = new List<Objective>();

    [Header("UI")]
    public GameObject objectivesPanel; // O painel inteiro (ativar/desativar)
    public Transform objectivesContainer; // Container onde os textos serão instanciados
    public GameObject objectiveTextPrefab; // Prefab do texto para cada objetivo
    public GameObject completedObjectivePanel;
    public ObjectivesUIManager uiManager;

    private Dictionary<Objective, GameObject> activeObjectivesUI = new Dictionary<Objective, GameObject>();

    private void Start()
    {
        instance = this;
        // No começo do jogo o painel de objetivos fica escondido
        if (objectivesPanel != null)
            objectivesPanel.SetActive(false);

        uiManager.InitializeObjectives(allObjectives);
    }

    private void Update()
    {
        // Verifica se o jogador apertou a tecla "O"
        if (HasActiveObjetives() && Input.GetKeyDown(KeyCode.O))
        {
            ToggleObjectivesPanel();
        }
    }

    // Alterna entre ativar/desativar o painel
    private void ToggleObjectivesPanel()
    {
        if (objectivesPanel != null)
        {
            objectivesPanel.SetActive(!objectivesPanel.activeSelf);
        }
    }

    // Ativar um objetivo (descobrir)
    public void ActivateObjective(int index)
    {
        if (index < 0 || index >= allObjectives.Count)
        {
            Debug.LogWarning("Índice de objetivo inválido!");
            return;
        }

        Objective obj = allObjectives[index];
        if (!obj.isActive && !obj.isCompleted)
        {
            obj.isActive = true;
            ToggleObjectivesPanel();
        }
    }

    // Completar um objetivo
    public void CompleteObjective(int index)
    {
        if (index < 0 || index >= allObjectives.Count)
        {
            Debug.LogWarning("Índice de objetivo inválido!");
            return;
        }

        Objective obj = allObjectives[index];
        
        if (obj.isActive && !obj.isCompleted)
        {
            obj.isCompleted = true;
            obj.isActive = false;
            uiManager.MarkObjectiveCompleted(index);
            ToggleObjectivesPanel();
        }

        StartCoroutine(CompletedObjectiveFeedback());

    }

    private IEnumerator CompletedObjectiveFeedback()
    {
        completedObjectivePanel.SetActive(true);
        yield return new WaitForSeconds(3);
        completedObjectivePanel.SetActive(false);
    }

    private bool HasActiveObjetives()
    {
        foreach(var objective in allObjectives)
        {
            if (objective.isActive == true) 
                return true;
        }

        objectivesPanel.SetActive(false);

        return false;
    }
}
