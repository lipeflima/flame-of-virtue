using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool gamePaused { get; private set; }
    bool pauseInput;
    public static PauseMenu Instance;
    public GameObject pauseHud;
    private Character character;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        character = Character.Instance;
    }
    private void Update()
    {
        HandleInput();
    }
    void HandleInput()
    {
        pauseInput = character.InputHandler.PauseInput;

        if (pauseInput)
        {
            TogglePause();
        }
    }
    public void TogglePause()
    {
        if (gamePaused)
        {
            gamePaused = false;
            pauseHud.SetActive(false);
            Time.timeScale = 1.0f;
        }
        else
        {
            gamePaused = true;
            pauseHud.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }
}
