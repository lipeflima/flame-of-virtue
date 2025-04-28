using System.Collections;
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
    [SerializeField] private float retriggerDelay = 5f; 
    private float retriggerTimer = 0f;
    private bool isInCooldown = false;

    [Header("Fade Settings")]
    [SerializeField] private CanvasGroup fadeCanvasGroup; // Um Canvas com imagem preta e CanvasGroup
    [SerializeField] private float fadeDuration = 0.5f;   // Tempo do fade in e out
    [SerializeField] private float teleportDelay = 0.2f;  // Tempo que o jogador fica preso antes e depois do teleporte

    private PlayerController controller;

    void Start()
    {
        controller = PlayerController.Instance;
    }

    private void Update()
    {
        if (isInCooldown)
        {
            retriggerTimer -= Time.deltaTime;
            if (retriggerTimer <= 0f)
            {
                isInCooldown = false;
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
            StartCoroutine(HandleTeleportSequence(other.transform));
            retriggerTimer = retriggerDelay;
            isInCooldown = true;
        }
    }

    private IEnumerator HandleTeleportSequence(Transform player)
    {
        controller.isTeleporting = true;

        // Fade in (escurece a tela)
        yield return StartCoroutine(Fade(1f));

        // Espera um pouquinho
        yield return new WaitForSeconds(teleportDelay);

        // Move o jogador
        player.position = exitPoint.position;

        // Troca as câmeras
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

        // Espera um pouquinho depois do teleporte
        yield return new WaitForSeconds(teleportDelay);

        // Fade out (clareia a tela)
        yield return StartCoroutine(Fade(0f));

        controller.isTeleporting = false;
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeCanvasGroup.alpha;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.unscaledDeltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, timer / fadeDuration);
            yield return null;
        }

        fadeCanvasGroup.alpha = targetAlpha;
    }
}
