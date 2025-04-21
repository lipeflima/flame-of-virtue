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
        // Define a rotação inicial
        if (useCurrentInitialRotation)
        {
            initialRotation = transform.eulerAngles;
        }

        // Converte os vetores de rotação para quaternions
        startRotation = Quaternion.Euler(initialRotation);
        endRotation = Quaternion.Euler(finalRotation);
    }

    void Update()
    {
        // Incrementa o progresso da rotação baseado no tempo e na velocidade
        if (rotationProgress < 1f)
        {
            rotationProgress += Time.deltaTime * rotationSpeed;
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, rotationProgress);
        }
    }

    // Método público para reiniciar a rotação
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
