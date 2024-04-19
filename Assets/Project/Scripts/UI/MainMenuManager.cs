using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject ContinuePanel;
    public GameObject NewGamePanel;
    public GameObject OptionsPanel;
    public GameObject CreditsPanel;
    public GameObject ExitGamePanel;

    public void CleanPanel()
    {
        NewGamePanel.SetActive(false);
        ContinuePanel.SetActive(false);
    }

    public void ContinueGame()
    {
        ContinuePanel.SetActive(true);
    }

    public void ContinueGameYes()
    {
        SceneManager.LoadScene(1);
    }

    public void ContinueGameNo()
    {
        ContinuePanel.SetActive(false);
    }

    public void NewGame()
    {
        NewGamePanel.SetActive(true);
    }

    public void NewGameYes()
    {
        SceneManager.LoadScene(1);
    }

    public void NewGameNo()
    {
        NewGamePanel.SetActive(false);
    }

    public void Options()
    {

    }

    public void Credits()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
