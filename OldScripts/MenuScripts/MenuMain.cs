using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.Rendering.PostProcessing;
public class MenuMain : MonoBehaviour
{
    float temp = 0;
    Scene scene;
    public string currentLevel = "Acampamento";
    public GameObject loading;
    public Slider slider;
    public Text progressText;
    public UnityEvent OnMainMenu;
    GameManager manager;
    public static MenuMain Instance;
    public PostProcessVolume postProcessVolume;
    public PostProcessProfile profile;
    public float transitionDuration = 1f;
    public string nextSceneName;

    private LensDistortion lensDistortion;
    private Coroutine transitionCoroutine;
    private void Awake()
    {
        if(Instance != null)
        {
            //Destroy(gameObject);
        }
        else 
        {
            Instance = this;
        }
        
    }
    void Start()
    {
        manager = GameManager.Instance;   
        if (profile.TryGetSettings(out lensDistortion))
        {
            lensDistortion.intensity.value = 0f;
        }
        else
        {
            Debug.LogError("LensDistortion not found in the PostProcessVolume profile.");
        }     
    }
    public void GoToLevel(string levelID)
    {
        StartCoroutine(LoadAsynchronously(levelID));
    }
    public void GoToEncounter(string levelName)
    {  
        StartSceneTransition(levelName);
    }
    public void GoToLevelBeforeEncounter()
    {
        string levelToGo = PlayerPrefs.GetString("LevelBeforeEncounter");
        StartCoroutine(LoadAsynchronously(levelToGo));        
    }
    public void LoadLevel()
    {
        currentLevel = PlayerPrefs.GetString("CurrentLevelName", "Acampamento");

        if (currentLevel == "CombateSystemScene" || currentLevel == "CombateEnd" || currentLevel == "Menu" || currentLevel == null || currentLevel == "")
            currentLevel = "Acampamento";

        StartCoroutine(LoadAsynchronously(currentLevel));
    }
    public void RestartLevel()
    {
        StartCoroutine(LoadAsynchronously(currentLevel));
    }
    IEnumerator LoadAsynchronously(string levelName)
    {
        Debug.Log(levelName);
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelName);

        loading.SetActive(true);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress/0.9f);

            slider.value = progress * 100f;

            temp = progress * 100f;

            progressText.text = (int)temp + "%";

            yield return null;
        }
    }
    public void Unpause()
    {
        manager.Unpause();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void StartSceneTransition(string levelName)
    {
        if (transitionCoroutine == null)
        {
            transitionCoroutine = StartCoroutine(TransitionEffect(levelName));
        }
    }

    private IEnumerator TransitionEffect(string levelName)
    {
        float elapsedTime = 0f;

        // Gradually increase the intensity of LensDistortion
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionDuration;
            lensDistortion.intensity.value = Mathf.Lerp(-30f, -100f, t);
            yield return null;
        }

        lensDistortion.intensity.value = -100f;

        // Load the next scene
        //SceneManager.LoadScene(nextSceneName);
        StartCoroutine(LoadAsynchronously(levelName));

        // Reset elapsed time for fading out
        elapsedTime = 0f;

        // Gradually decrease the intensity back to 0
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionDuration;
            lensDistortion.intensity.value = Mathf.Lerp(-100f, 0f, t);
            yield return null;
        }

        lensDistortion.intensity.value = 0f;
        transitionCoroutine = null;
    }
}
