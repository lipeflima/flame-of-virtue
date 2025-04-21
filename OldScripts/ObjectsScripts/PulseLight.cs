using UnityEngine;

public class PulseLight : MonoBehaviour
{
    public float pulseSpeed = 1.0f; // Velocidade da pulsa��o (ciclos por segundo)
    public float minIntensity = 0.0f; // Intensidade m�nima da luz
    public float maxIntensity = 1.0f; // Intensidade m�xima da luz

    private Light pointLight; // Refer�ncia para o componente Light

    void Start()
    {
        // Obt�m o componente Light anexado ao GameObject
        pointLight = GetComponent<Light>();

        if (pointLight == null)
        {
            Debug.LogError("Nenhum componente Light encontrado neste GameObject.");
        }
    }

    void Update()
    {
        if (pointLight != null)
        {
            // Calcula a nova intensidade usando uma onda senoidal
            float intensity = Mathf.Lerp(minIntensity, maxIntensity, (Mathf.Sin(Time.time * pulseSpeed * Mathf.PI * 2) + 1) / 2);
            pointLight.intensity = intensity;
        }
    }
}
