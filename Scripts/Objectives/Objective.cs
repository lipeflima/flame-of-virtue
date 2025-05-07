using UnityEngine;

[System.Serializable]
public class Objective
{
    public string title; // Nome do objetivo
    [TextArea]
    public string description; // Descrição (opcional)

    // [HideInInspector]
    public bool isActive; // Se o objetivo já foi descoberto
    // [HideInInspector]
    public bool isCompleted; // Se o objetivo foi concluído
}
