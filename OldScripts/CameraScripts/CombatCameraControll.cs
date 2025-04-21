using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCameraControll : MonoBehaviour
{
    private Character character;
    private CharacterInputHandler inputHandler;
    private bool zoomInput;
    private bool isZoomedIn = false; // Controla o estado atual do zoom
    public GameObject table, container;
    public CinemachineVirtualCamera VirtualCamera;
    public int maxPriority = 10; // Prioridade máxima da câmera
    public int minPriority = 0;  // Prioridade mínima da câmera

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        // Lê o estado do botão de zoom
        zoomInput = Input.GetMouseButtonDown(1);

        if (zoomInput)
        {
            ToggleCameraPriority();
        }
    }

    void ToggleCameraPriority()
    {
        // Alterna a prioridade entre máximo e mínimo
        if (isZoomedIn)
        {
            VirtualCamera.Priority = minPriority;
        }
        else
        {
            VirtualCamera.Priority = maxPriority;
        }

        // Atualiza o estado atual do zoom
        isZoomedIn = !isZoomedIn;
    }
}
