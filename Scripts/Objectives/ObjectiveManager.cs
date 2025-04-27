using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveManager : MonoBehaviour
{
    [Header("Lista de todos os objetivos (definidos no Inspector)")]
    public List<Objective> allObjectives = new List<Objective>();

    [Header("UI")]
    public GameObject objectivesPanel; // O painel inteiro (ativar/desativar)
    public Transform objectivesContainer; // Container onde os textos serão instanciados
    public GameObject objectiveTextPrefab; // Prefab do texto para cada objetivo

    private Dictionary<Objective, GameObject> activeObjectivesUI = new Dictionary<Objective, GameObject>();

    private void Start()
    {
        // No começo do jogo o painel de objetivos fica escondido
        if (objectivesPanel != null)
            objectivesPanel.SetActive(false);
    }

    private void Update()
    {
        // Verifica se o jogador apertou a tecla "O"
        if (Input.GetKeyDown(KeyCode.O))
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
            CreateObjectiveUI(obj);
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
            RemoveObjectiveUI(obj);
        }
    }

    // Criar UI de um objetivo ativo
    private void CreateObjectiveUI(Objective obj)
    {
        GameObject objUI = Instantiate(objectiveTextPrefab, objectivesContainer);
        TextMeshProUGUI text = objUI.GetComponent<TextMeshProUGUI>();
        text.text = "- " + obj.title;
        activeObjectivesUI.Add(obj, objUI);
    }

    // Remover UI de um objetivo completado
    private void RemoveObjectiveUI(Objective obj)
    {
        if (activeObjectivesUI.ContainsKey(obj))
        {
            Destroy(activeObjectivesUI[obj]);
            activeObjectivesUI.Remove(obj);
        }
    }
}
