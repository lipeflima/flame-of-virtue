using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Camera targetCamera; // A c�mera que o Canvas deve apontar para
    public bool useMiniMapCamera;
    private void Start()
    {
        if (useMiniMapCamera)
        {
            targetCamera = GameObject.FindGameObjectWithTag("MiniMapCamera").GetComponent<Camera>();
        }

        if (targetCamera == null)
        {
            // Busca pela c�mera principal caso nenhuma seja atribu�da
            targetCamera = Camera.main;
        }
    }

    private void LateUpdate()
    {
        // Faz o Canvas olhar para a c�mera
        transform.LookAt(transform.position + targetCamera.transform.rotation * Vector3.forward,
                         targetCamera.transform.rotation * Vector3.up);
    }
}
