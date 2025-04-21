using UnityEngine;

public class CameraRootManager : MonoBehaviour
{
    [Header("Rotation Settings")]
    public Vector3 initialRotation;
    public Vector3 finalRotation;
    public bool useCurrentInitialRotation = false;

    [Header("Rotation Speed")]
    public float rotationSpeed = 1.0f;

    private Quaternion startRotation;
    private Quaternion endRotation;
    private float rotationProgress = 0f;

    void Start()
    {
        // Define a rota��o inicial
        if (useCurrentInitialRotation)
        {
            initialRotation = transform.eulerAngles;
        }

        // Converte os vetores de rota��o para quaternions
        startRotation = Quaternion.Euler(initialRotation);
        endRotation = Quaternion.Euler(finalRotation);
    }

    void Update()
    {
        // Incrementa o progresso da rota��o baseado no tempo e na velocidade
        if (rotationProgress < 1f)
        {
            rotationProgress += Time.deltaTime * rotationSpeed;
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, rotationProgress);
        }
    }

    // M�todo p�blico para reiniciar a rota��o
    public void RestartRotation()
    {
        rotationProgress = 0f;
        if (useCurrentInitialRotation)
        {
            initialRotation = transform.eulerAngles;
            startRotation = Quaternion.Euler(initialRotation);
        }
    }
}
