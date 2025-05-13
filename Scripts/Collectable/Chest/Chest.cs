using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Chest : MonoBehaviour
{
    [Header("Itens a serem gerados")]
    public GameObject[] itemsPrefabs;
    public GameObject interactHud;

    [Header("Configuração de Spawn")]
    public float maxDistance = 2f;
    public float launchForce = 5f;
    public float moveDuration = 0.5f;
    public float arcHeight = 1.5f;

    [Header("Interação")]
    public float interactHoldTime = 2f;
    private float interactTimer = 0f;
    private bool isPlayerNearby = false;
    private bool chestOpened = false;

    [Header("Inputs")]
    public KeyCode interactKey = KeyCode.E;

    [Header("UI de Interação")]
    public GameObject holdUI;
    public Image holdProgressBar;

    [Header("Animação de Balanço")]
    public float shakeDuration = 0.5f;
    public float shakeIntensity = 10f; // graus de balanço
    private Quaternion originalRotation;
    private bool isShaking = false;

    [Header("Áudio")]
    public AudioSource audioSource;
    public AudioClip shakeSound;
    public AudioClip openSound;

    [Header("Partículas")]
    public ParticleSystem shakeParticles;
    public ParticleSystem openParticles;

    void Start()
    {
        originalRotation = transform.rotation;
    }

    void Update()
    {
        CheckInput();
    }

    void CheckInput()
    {
        if (!isPlayerNearby || chestOpened) return;

        if (Input.GetKey(interactKey))
        {
            interactTimer += Time.deltaTime;
            UpdateHoldUI(interactTimer / interactHoldTime);

            if (interactTimer >= interactHoldTime)
            {
                chestOpened = true;
                StartCoroutine(ShakeAndOpenChest());
                HideHoldUI();
            }
        }
        else
        {
            interactTimer = 0f;
            UpdateHoldUI(0f);
        }
    }

    private void UpdateHoldUI(float progress)
    {
        if (holdUI != null)
        {
            holdUI.SetActive(true);
            if (holdProgressBar != null)
            {
                holdProgressBar.fillAmount = Mathf.Clamp01(progress);
            }
        }
    }

    private void HideHoldUI()
    {
        if (holdUI != null)
        {
            holdUI.SetActive(false);
        }
    }

    private IEnumerator ShakeAndOpenChest()
    {
        isShaking = true;

        // Toca som de balançar
        if (audioSource != null && shakeSound != null)
            audioSource.PlayOneShot(shakeSound);

        // Ativa partículas de shake
        if (shakeParticles != null)
            shakeParticles.Play();

        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            elapsed += Time.deltaTime;
            float angle = Mathf.Sin(elapsed * 50f) * shakeIntensity * (1f - (elapsed / shakeDuration));
            transform.rotation = originalRotation * Quaternion.Euler(0f, 0f, angle);
            yield return null;
        }

        // Para de balançar
        transform.rotation = originalRotation;
        isShaking = false;

        // Para partículas de shake
        if (shakeParticles != null)
            shakeParticles.Stop();

        // Toca som de abrir
        if (audioSource != null && openSound != null)
            audioSource.PlayOneShot(openSound);

        // Partículas de abrir
        if (openParticles != null)
            openParticles.Play();

        OpenChest();
    }

    public void OpenChest()
    {
        foreach (GameObject itemPrefab in itemsPrefabs)
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            Vector3 spawnPosition = transform.position;

            GameObject spawnedItem = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
            StartCoroutine(MoveItemWithArc(spawnedItem.transform, randomDirection));
            this.enabled = false;
        }
    }

    private IEnumerator MoveItemWithArc(Transform itemTransform, Vector2 direction)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = itemTransform.position;
        Vector3 targetPosition = startPosition + (Vector3)(direction * maxDistance);

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / moveDuration);

            Vector3 horizontalPosition = Vector3.Lerp(startPosition, targetPosition, t);
            float height = arcHeight * 4f * (t - t * t);

            try
            {
                itemTransform.position = horizontalPosition + new Vector3(0f, height, 0f);
            }
            catch{}            

            yield return null;
        }

        itemTransform.position = targetPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = true;
            interactTimer = 0f;
            interactHud.SetActive(true);
            UpdateHoldUI(0f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
            interactTimer = 0f;
            interactHud.SetActive(false);
            HideHoldUI();
        }
    }
}
