using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFinal : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public CinemachineBrain brain;
    public GameObject HUD;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerPrincipal"))
        {
            HUD.SetActive(false);
            brain.m_IgnoreTimeScale = true;
            Time.timeScale = 0f;
            virtualCamera.Priority = 12;

            StartCoroutine("LoadMainMenu");
        }
    }

    public IEnumerator LoadMainMenu()
    {
        yield return new WaitForSecondsRealtime(10f);
        Time.timeScale = 1f;
        brain.m_IgnoreTimeScale = false; 
        SceneManager.LoadScene(0);
    }
}
