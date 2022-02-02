using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    [SerializeField]
    GameObject MenuUI;
    [SerializeField]
    GameObject RulesUI;
    Player player;
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else Pause();
        }   
    }

    private void Pause()
    {
        MenuUI.SetActive(true);
        gameIsPaused = true;
        Time.timeScale = 0f;
        player.Disable();
    }

    public void LoadRules()
    {
        MenuUI.SetActive(false);
        RulesUI.SetActive(true);
    }
    public void BackInMenu()
    {
        RulesUI.SetActive(false);
        MenuUI.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Resume();
    }
    public void Resume()
    {
        MenuUI.SetActive(false);
        gameIsPaused = false;
        Time.timeScale = 1f;
        player.UnDisable();
    }
    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main_Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
