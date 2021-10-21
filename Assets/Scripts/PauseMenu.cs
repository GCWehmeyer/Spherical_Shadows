using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused;
    [SerializeField] private GameObject pauseMenu;
    
    [SerializeField] private GameObject ResumeButton;
    [SerializeField] private GameObject MainMenuButton;
    [SerializeField] private GameObject level1Button;
    [SerializeField] private GameObject level2Button;
    [SerializeField] private GameObject level3Button;
    

    GameObject[] pauseObjects;

    public void Start()
    {
        pauseObjects = GameObject.FindGameObjectsWithTag("PauseStuff");
        hidePaused();
    }
    
    public void MainMenu()
    {
        resumeButton();
        SceneManager.LoadScene(0);
    }
    
    public void loadLvl1()
    {
        resumeButton();
        SceneManager.LoadScene(1);
    }

    public void loadLvl2()
    {
        resumeButton();
        SceneManager.LoadScene(2);
    }

    public void loadLvl3()
    {
        resumeButton();
        SceneManager.LoadScene(3);
    }
    
    public void showPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    public void hidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    public void resumeButton()
    {
        gameIsPaused = !gameIsPaused;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        hidePaused();
        Time.timeScale = 1;
        AudioListener.pause = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }
    }

    void PauseGame()
    {
        if (gameIsPaused)
        {
            showPaused();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
            AudioListener.pause = true;
        }
        else
        {
            hidePaused();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            AudioListener.pause = false;
        }
    }

}
