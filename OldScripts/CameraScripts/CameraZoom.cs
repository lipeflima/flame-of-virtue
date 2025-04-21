using System.Collections;
using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private CinemachineFramingTransposer framingTransposer;

    public float maxDistance = 25f;
    public float minDistance = 8f;
    public float aimDistance = 15f;
    public static float cameraDistance = 12f; // Inicializando com zoom de 10
    public static float zoomCameraInput = 0f;

    [SerializeField] private float sensitivity = 10f;
    private Character character;

    // Vari�vel para armazenar a dist�ncia original da c�mera antes do zoom m�ximo
    private float originalCameraDistance;

    private void Start()
    {
        // Certifica-se que o personagem est� sendo obtido corretamente
        character = Character.Instance;

        // Configura o componente de transla��o da c�mera (Framing Transposer)
        framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

        // Inicializa a dist�ncia da c�mera se n�o estiver configurada em outra c�mera
        if (framingTransposer != null)
        {
            framingTransposer.m_CameraDistance = cameraDistance;
        }
    }

    private void Update()
    {
        // Atualiza o input de zoom baseado no input do jogador
        zoomCameraInput = character.InputHandler.ZoomInput;

        // Calcula a nova distância da câmera com base no input de zoom e sensibilidade
        if (zoomCameraInput != 0f)
        {
            cameraDistance += zoomCameraInput * sensitivity * Time.deltaTime;
            //cameraDistance = Mathf.Clamp(cameraDistance, minDistance, maxDistance);

            if(cameraDistance > maxDistance) cameraDistance = maxDistance;

            if(cameraDistance < minDistance) cameraDistance = minDistance;
        }

        // Aplica a distância calculada à câmera virtual
        if (framingTransposer != null)
        {
            framingTransposer.m_CameraDistance = cameraDistance;
        }

        // Verifica se o botão direito do mouse está pressionado (SecondaryShootInput)
        if (character.InputHandler.RightMouseInput)
        {
            // Armazena a distância original da câmera ao pressionar o botão
            if (cameraDistance < maxDistance)
            {
                originalCameraDistance = cameraDistance;
            }

            // Avança a câmera para a distância máxima
            cameraDistance = maxDistance;
        }
        else if (!character.InputHandler.RightMouseInput && cameraDistance == maxDistance)
        {
            // Se o botão foi solto, retorna à distância original somente se foi alterada
            if (originalCameraDistance > 0f) // Garante que a distância original foi registrada
            {
                cameraDistance = originalCameraDistance;
            }
        }
    }

}
