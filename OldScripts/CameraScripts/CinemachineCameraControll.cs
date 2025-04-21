using UnityEngine;
using Cinemachine;

public class CameraHorizontalControl : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    private CinemachinePOV povComponent;
    private Character character;
    private CharacterInputHandler inputHandler;
    private bool LeftMouseInput;
    void Start()
    {
        // Obtém a referência à Cinemachine Virtual Camera e ao componente Aim POV
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        character = Character.Instance;
        inputHandler = character.GetComponent<CharacterInputHandler>();
        if (virtualCamera != null)
        {
            povComponent = virtualCamera.GetCinemachineComponent<CinemachinePOV>();
        }

        if (povComponent == null)
        {
            Debug.LogError("CinemachinePOV não encontrado na Virtual Camera.");
        }
    }

    void Update()
    {
        LeftMouseInput = inputHandler.LeftMouseInput;

        if (povComponent == null || character == null)
            return;

        // Verifica se o botão direito do mouse está pressionado
        if (LeftMouseInput)
        {
            // Define a velocidade do eixo horizontal como 2
            povComponent.m_HorizontalAxis.m_MaxSpeed = 300f;
            povComponent.m_VerticalAxis.m_MaxSpeed = 300f;

            // Atualiza apenas o eixo horizontal do POV com base no movimento do mouse
            //float horizontalInput = Input.GetAxis("Mouse X");
            //povComponent.m_HorizontalAxis.Value += horizontalInput * povComponent.m_HorizontalAxis.m_MaxSpeed * Time.deltaTime;
        }
        else
        {
            // Define a velocidade do eixo horizontal como 0
            povComponent.m_HorizontalAxis.m_MaxSpeed = 0f;
            povComponent.m_VerticalAxis.m_MaxSpeed = 0f;
        }
    }
}
