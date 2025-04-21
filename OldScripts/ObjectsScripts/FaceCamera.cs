using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Camera targetCamera; // A câmera que o Canvas deve apontar para
    public bool useMiniMapCamera;
    private void Start()
    {
        if (useMiniMapCamera)
        {
            targetCamera = GameObject.FindGameObjectWithTag("MiniMapCamera").GetComponent<Camera>();
        }

        if (targetCamera == null)
        {
            // Busca pela câmera principal caso nenhuma seja atribuída
            targetCamera = Camera.main;
        }
    }

    private void LateUpdate()
    {
        // Faz o Canvas olhar para a câmera
        transform.LookAt(transform.position + targetCamera.transform.rotation * Vector3.forward,
                         targetCamera.transform.rotation * Vector3.up);
    }
}
