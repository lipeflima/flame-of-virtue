using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShakeController : MonoBehaviour
{
    // Refer�ncias � Cinemachine
    public CinemachineVirtualCamera virtualCamera;

    // Controle de intensidade e dura��o
    public float shakeIntensity = 1.5f;
    public float shakeDuration = 0.2f;

    // Vari�vel para controle interno
    private CinemachineBasicMultiChannelPerlin perlinNoise;
    private float shakeTimer;
    private bool isShaking = false;  // Controla o estado da tremida
    private Character character;
    void Start()
    {
        // Acessar o componente de tremida da c�mera Cinemachine
        perlinNoise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        // Certifique-se de que a tremida come�a desativada
        perlinNoise.m_AmplitudeGain = 0f;

        // Inicializar refer�ncias de arma e personagem
        character = Character.Instance;
    }

    void Update()
    {
        // Verifica se o jogador est� disparando
        if (character.GetAliveStatus() || character.Health.GetDamageStatus())
        {
            // Iniciar tremida se ainda n�o estiver ativa
            if (!isShaking)
            {
                TriggerCameraShake();
            }
        }

        // Controlar a dura��o da tremida
        if (isShaking && shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                StopCameraShake();
            }
        }
    }

    // M�todo para ativar o efeito de tremida
    public void TriggerCameraShake()
    {
        perlinNoise.m_AmplitudeGain = shakeIntensity;  // Define a intensidade da tremida
        shakeTimer = shakeDuration;  // Define a dura��o da tremida
        isShaking = true;  // Define o estado como tremendo
    }

    // M�todo para parar o efeito de tremida
    private void StopCameraShake()
    {
        perlinNoise.m_AmplitudeGain = 0f;  // Volta a intensidade para zero, removendo a tremida
        isShaking = false;  // Define o estado como n�o tremendo
    }
}
