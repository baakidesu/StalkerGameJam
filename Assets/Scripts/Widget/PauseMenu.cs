using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] public GameObject pauseMenu;
    [SerializeField] public GameObject escMenu;
    [SerializeField] public GameObject settingsMenu;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.LogError("asd");
            if(pauseMenu != null)
            {
                if (!pauseMenu.active)
                {
                    pauseMenu.SetActive(true);
                    Time.timeScale = 0f;
                }
                else
                {
                    BackEvent();
                    Resume();
                }
            }

        }
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void QuitEvent()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
    public void SettingsEvent()
    {
        escMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void BackEvent()
    {
        escMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }
}
