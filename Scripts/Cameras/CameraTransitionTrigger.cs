using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTransitionTrigger : MonoBehaviour
{
    [Header("Câmeras")]
    [SerializeField] private CinemachineVirtualCamera cameraToDisable;
    [SerializeField] private CinemachineVirtualCamera cameraToEnable;

    [Header("Transporte do Jogador")]
    [SerializeField] private Transform exitPoint;

    [Header("Objetos a serem desativados")]
    [SerializeField] private List<GameObject> objectsToDisable;

    [Header("Tag do jogador")]
    [SerializeField] private string playerTag = "Player";

    [Header("Reutilização")]
    [SerializeField] private float retriggerDelay = 5f; // Tempo para poder usar novamente
    private float retriggerTimer = 0f;
    private bool isInCooldown = false;

    private void Update()
    {
        if (isInCooldown)
        {
            retriggerTimer -= Time.deltaTime;
            if (retriggerTimer <= 0f)
            {
                isInCooldown = false;
                // Reativa os objetos, se desejar que retornem
                foreach (GameObject obj in objectsToDisable)
                {
                    if (obj != null)
                        obj.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isInCooldown) return;

        if (other.CompareTag(playerTag))
        {
            // Move o jogador para o ponto de saída
            other.transform.position = exitPoint.position;

            // Troca de prioridade das câmeras
            if (cameraToDisable != null)
                cameraToDisable.Priority = 0;

            if (cameraToEnable != null)
                cameraToEnable.Priority = 10;

            // Desativa os objetos
            foreach (GameObject obj in objectsToDisable)
            {
                if (obj != null)
                    obj.SetActive(false);
            }

            // Ativa o cooldown
            retriggerTimer = retriggerDelay;
            isInCooldown = true;
        }
    }
}
