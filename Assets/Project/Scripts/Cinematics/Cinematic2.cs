using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cinematic2 : MonoBehaviour
{
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;
    public CinemachineVirtualCamera cam3;

    public Animator player1Animator;
    public GameObject player;

    public GameObject blackScreenPanel;


    private void Start()
    {
        StartCoroutine("Scene");
    }

    public IEnumerator Scene()
    {
        yield return new WaitForSeconds(1f);
        cam2.Priority = 11;
        yield return new WaitForSeconds(0.6f);
        player1Animator.SetTrigger("SkysungCineChange");

        yield return new WaitForSeconds(4.5f);
        player.transform.Rotate(0, -80f, 0);
        player.transform.Translate(0, -0.5f, 0.6f);
        player1Animator.SetTrigger("SkysungCineChange");
        cam3.Priority = 12;

        yield return new WaitForSeconds(2f);
        blackScreenPanel.SetActive(true);

        yield return new WaitForSeconds(2f);
        int scene = SceneManager.GetActiveScene().buildIndex + 1;
        PlayerPrefs.SetInt("Level", scene);
        SceneManager.LoadScene(scene);
    }
}
