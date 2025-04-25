using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class MenuMain : MonoBehaviour
{
    float temp = 0;
    public float waitTime = 4;
    public bool pressToStart = false;
    bool pressed;
    Scene scene;
    public string currentLevel, levelToChange;
    public int cLevel = 1;
    public GameObject loading, effect, menuUI, pressUI;
    public Slider slider;
    public Text progressText;
    public UnityEvent OnMainMenu;
    //SaveData saveData;
    //LevelManager levelManager;
    public AudioSource startSound;
    void Start()
    {
        //saveData = SaveData.Instance;
        //levelManager = LevelManager.Instance;
        CheckLevel();
    }
    private void Update()
    {
        if (pressToStart && !pressed)
        {
            if (Input.GetButtonDown("Submit") || Input.GetButton("Primary Shoot"))
            {
                pressed = true;
                menuUI.SetActive(true);
                startSound.Play();
                pressUI.SetActive(false);
            }
        }
        
    }
    void CheckLevel()
    {
        try 
        {
            //saveData.GetInt("currentLevel");
        }
        catch
        {
            //currentLevel = "Level1";
        }
        
        /* switch(saveData.GetInt("currentLevel"))
        {
            case 0:
            
            break;
        } */
    }
    public void GoToLevel(string levelID)
    {
        StartCoroutine(LoadAsynchronously(levelID));
    }
    public void GoToLevelWithLoading(string levelID)
    {
        Invoke("ChangeLevel", waitTime);
        loading.SetActive(true);
        levelToChange = levelID;
    }
    void ChangeLevel()
    {
        GoToLevel(levelToChange);
    }
    public void GoToLevel(int level)
    {
        SceneManager.LoadScene("Level" + "" + level.ToString());
    }
    public void GoToMenu(string levelName)
    {
        SceneManager.LoadScene("Level" + levelName);
    }
    public void LoadLevel()
    {
        //StartCoroutine(LoadAsynchronously("Level" + "" + saveData.GetInt("currentLevel").ToString()));
    }
    public void NextLevel()
    {
        //int nextLevel = levelManager.GetLevel() + 1;
        //StartCoroutine(LoadAsynchronously("Level" + "" + nextLevel.ToString()));
    }
    public void NewGame()
    {
        //saveData.SetInt("currentLevel", 1);
    }
    public void SaveLevel()
    {
        //levelManager.SaveLevel();
    }
    public void RestartLevel()
    {
        //int cLevel = levelManager.GetLevel();
        StartCoroutine(LoadAsynchronously("Level" + "" + cLevel.ToString()));
    }
    IEnumerator LoadAsynchronously(string levelName)
    {
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
    public void SpawnEffect()
    {
        Instantiate(effect, Vector3.zero, Quaternion.identity);
    }

    public void RestoreTimeScale()
    {
        Time.timeScale = 1.0f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
