using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelZeroManager : MonoBehaviour
{
    public int scene;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerPrincipal"))
            SceneManager.LoadScene(scene);
    }
}
