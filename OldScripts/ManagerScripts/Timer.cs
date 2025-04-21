using UnityEngine;
using UnityEngine.Rendering;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public GameObject sunContainer, rpDay, rpNight;
    public Transform sun, moon, center;
    public Material daySkyBox;
    public Material nightSkyBox;
    public int dayTime = 6;
    public int nightTime = 18;
    public int endTime = 30;
    public int mult = 1;
    private float timeElapsed;
    private const float DegreesPerHour = 360f / 24f;
    public bool isDay, hasTimer, reset;

    void Start()
    {
        if (reset)
        {
            ResetSavedData();
        }
        // Carregar valores persistidos
        timeElapsed = PlayerPrefs.GetFloat("TimeElapsed", 0);
        isDay = PlayerPrefs.GetInt("IsDay", 1) == 1;
        float sunRotation = PlayerPrefs.GetFloat("SunRotation", 0);

        // Configurar Skybox inicial
        RenderSettings.skybox = isDay ? daySkyBox : nightSkyBox;
        DynamicGI.UpdateEnvironment();

        // Configurar rota��o do Sol
        if (sunContainer != null)
        {
            sunContainer.transform.rotation = Quaternion.Euler(sunRotation, 0, 0);
        }

        // Configurar estado dos Reflection Probes
        if (rpDay != null) rpDay.SetActive(isDay);
        if (rpNight != null) rpNight.SetActive(!isDay);

        // Verificar atribui��es
        if (sunContainer == null)
        {
            Debug.LogError("O sunContainer n�o foi atribu�do. Por favor, arraste o Directional Light para o script.");
        }
        if (daySkyBox == null || nightSkyBox == null)
        {
            Debug.LogError("Os materiais do Skybox n�o foram atribu�dos. Por favor, configure daySkyBox e nightSkyBox.");
        }
    }

    void Update()
    {
        timeElapsed += Time.deltaTime * mult;

        // Converter tempo em horas e minutos
        int hours = Mathf.FloorToInt(timeElapsed / 60);
        int minutes = Mathf.FloorToInt((timeElapsed % 60) % 60);

        // Atualizar rota��o do Sol
        RotateSun(timeElapsed);

        // Alternar Skybox
        UpdateSkybox(hours);

        //Debug.Log(hours);

        // Atualizar o texto do temporizador, se necess�rio
        // timerText.text = string.Format("{0:00}:{1:00}", hours, minutes);
    }
    void LateUpdate()
    {
        FaceCenter();
    }
    private void RotateSun(float elapsedTime)
    {
        float hoursPassed = elapsedTime / 60f;
        float sunRotation = hoursPassed * DegreesPerHour;

        if (sunContainer != null)
        {
            sunContainer.transform.rotation = Quaternion.Euler(sunRotation, 0, 0);
        }

        // Salvar rota��o do Sol
        PlayerPrefs.SetFloat("SunRotation", sunRotation);
    }

    private void UpdateSkybox(int currentHour)
    {
        if (isDay && currentHour >= nightTime)
        {
            RenderSettings.skybox = nightSkyBox;
            DynamicGI.UpdateEnvironment();
            isDay = false;

            if (rpNight != null) rpNight.SetActive(true);
            if (rpDay != null) rpDay.SetActive(false);
        }
        else if (!isDay && currentHour >= endTime)
        {
            RenderSettings.skybox = daySkyBox;
            DynamicGI.UpdateEnvironment();
            isDay = true;

            if (rpNight != null) rpNight.SetActive(false);
            if (rpDay != null) rpDay.SetActive(true);
            timeElapsed = 0;
        }

        // Salvar estado do Skybox e Reflection Probes
        PlayerPrefs.SetInt("IsDay", isDay ? 1 : 0);
        PlayerPrefs.SetFloat("TimeElapsed", timeElapsed);
    }
    public void ResetSavedData()
    {
        PlayerPrefs.SetFloat("TimeElapsed", 6);
        PlayerPrefs.SetInt("IsDay", 1);
        PlayerPrefs.SetFloat("SunRotation", 0);
    }
    void FaceCenter()
    {
        sun.transform.LookAt(center.transform.position);
        moon.transform.LookAt(center.transform.position);
    }
}
