using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameUI : MonoBehaviour
{
    public GameObject gameLoseUI;
    public GameObject gameWinUI;

    public Animator transition;
    public float transitionTime = 1f;

    bool victory;
    PlayerHealth playerHealth;
    public static bool gameOver;
    void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerHealth.OnPlayerDeath += ShowGameLoseUI;
        FindObjectOfType<Finish_Point>().OnReachedEndOfLevel += ShowGameWinUI;
    }

   
    void Update()
    {
        if (gameOver)
        {
           
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (SceneManager.GetActiveScene().buildIndex == 8 )
                {
                    Loader.Load(1, transition, transitionTime, gameLoseUI);
                }
                else
                {
                Loader.Load((SceneManager.GetActiveScene().buildIndex + 1), transition, transitionTime, gameLoseUI);

                }
              
                gameOver = false;
            }
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
              
        }
    }
    void ShowGameLoseUI()
    {
        GameOver(gameLoseUI);
    }
    void ShowGameWinUI()
    {
        GameOver(gameWinUI);
        
    }
    void GameOver(GameObject UI)
    {
        UI.SetActive(true);
        gameOver = true;

        // deregistrate delegates
        playerHealth.OnPlayerDeath -= ShowGameLoseUI;
        FindObjectOfType<Finish_Point>().OnReachedEndOfLevel -= ShowGameWinUI;
    }
}
