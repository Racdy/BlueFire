using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelZeroManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("PlayerPrincipal"))
        {
            int scene = SceneManager.GetActiveScene().buildIndex + 1;
            PlayerPrefs.SetInt("Level", scene);
            SceneManager.LoadScene(scene);
        }
    }
}
