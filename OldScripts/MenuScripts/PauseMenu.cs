using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool gamePaused { get; private set; }
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
        if (Input.GetButtonDown("Cancel"))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        gamePaused = !gamePaused;

        pauseHud.SetActive(gamePaused);
        Time.timeScale = gamePaused ? 0.0f : 1.0f;
    }
}
