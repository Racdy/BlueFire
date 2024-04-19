using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject HUDPanel;
    public GameObject exitPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void CleanPanel()
    {
        pausePanel.SetActive(false);
        HUDPanel.SetActive(false);
        exitPanel.SetActive(false);
    }

    public void Pause()
    {
        CleanPanel();
        //Cursor.lockState = CursorLockMode.Confined;
        //Cursor.visible = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void ContinueGame()
    {
        CleanPanel();
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        HUDPanel.SetActive(true);
        Time.timeScale = 1.0f;
    }

    public void ExitGame() 
    { 
        CleanPanel();
        exitPanel.SetActive(true);
    }

    public void ExitGameYes()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }

    public void ExitGameNo()
    {
        Pause();
    }
}
