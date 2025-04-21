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
    public int maxPriority = 10; // Prioridade m�xima da c�mera
    public int minPriority = 0;  // Prioridade m�nima da c�mera

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        // L� o estado do bot�o de zoom
        zoomInput = Input.GetMouseButtonDown(1);

        if (zoomInput)
        {
            ToggleCameraPriority();
        }
    }

    void ToggleCameraPriority()
    {
        // Alterna a prioridade entre m�ximo e m�nimo
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
