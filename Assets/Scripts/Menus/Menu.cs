using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    public GameObject MainMenuUI;
    public GameObject TutorialMenuUI;
    public GameObject pauseMenuUI;

    private bool gameIsPaused = false;
    private bool inGame = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !inGame)
        {
            if (gameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void MenuScreen()
    {
        inGame = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);

    }
    public void PlayGame()
    {
        inGame = true;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("QuitGame");
        Application.Quit();
    }

    public void TutorialMenu()
    {
        MainMenuUI.SetActive(false);
        TutorialMenuUI.SetActive(true);
    }
    public void Return()
    {
        MainMenuUI.SetActive(true);
        TutorialMenuUI.SetActive(false);
    }
}
