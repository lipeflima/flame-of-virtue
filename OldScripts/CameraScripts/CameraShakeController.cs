using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShakeController : MonoBehaviour
{
    // Referências à Cinemachine
    public CinemachineVirtualCamera virtualCamera;

    // Controle de intensidade e duração
    public float shakeIntensity = 1.5f;
    public float shakeDuration = 0.2f;

    // Variável para controle interno
    private CinemachineBasicMultiChannelPerlin perlinNoise;
    private float shakeTimer;
    private bool isShaking = false;  // Controla o estado da tremida
    private Character character;
    void Start()
    {
        // Acessar o componente de tremida da câmera Cinemachine
        perlinNoise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        // Certifique-se de que a tremida começa desativada
        perlinNoise.m_AmplitudeGain = 0f;

        // Inicializar referências de arma e personagem
        character = Character.Instance;
    }

    void Update()
    {
        // Verifica se o jogador está disparando
        if (character.GetAliveStatus() || character.Health.GetDamageStatus())
        {
            // Iniciar tremida se ainda não estiver ativa
            if (!isShaking)
            {
                TriggerCameraShake();
            }
        }

        // Controlar a duração da tremida
        if (isShaking && shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                StopCameraShake();
            }
        }
    }

    // Método para ativar o efeito de tremida
    public void TriggerCameraShake()
    {
        perlinNoise.m_AmplitudeGain = shakeIntensity;  // Define a intensidade da tremida
        shakeTimer = shakeDuration;  // Define a duração da tremida
        isShaking = true;  // Define o estado como tremendo
    }

    // Método para parar o efeito de tremida
    private void StopCameraShake()
    {
        perlinNoise.m_AmplitudeGain = 0f;  // Volta a intensidade para zero, removendo a tremida
        isShaking = false;  // Define o estado como não tremendo
    }
}
