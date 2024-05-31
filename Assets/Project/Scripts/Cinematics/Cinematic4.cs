using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cinematic4 : MonoBehaviour
{
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;
    public CinemachineVirtualCamera cam3;
    public CinemachineVirtualCamera cam4;
    public CinemachineVirtualCamera cam5;
    public CinemachineVirtualCamera cam6;
    public CinemachineVirtualCamera cam7;
    public CinemachineVirtualCamera cam8;

    public Animator player1Animator;
    public GameObject player;

    public Animator buildAnimator;
    public GameObject build;

    public Animator bossAnimator;

    public GameObject blackScreenPanel;
    private void Start()
    {
        StartCoroutine("Scene");
    }

    public IEnumerator Scene()
    {
        player1Animator.SetTrigger("SkysunAnimChange");
        yield return new WaitForSeconds(1.7f);
        player1Animator.SetTrigger("SkysunAnimChange");
        cam2.Priority = 11;
        yield return new WaitForSeconds(3f);
        cam3.Priority = 12;
        yield return new WaitForSeconds(0.5f);
        cam4.Priority = 13;
        yield return new WaitForSeconds(2f);
        cam5.Priority = 14;
        yield return new WaitForSeconds(3f);
        player1Animator.SetTrigger("SkysunTranslateChange");
        player1Animator.SetTrigger("SkysunAnimChange");
        cam6.Priority = 15;
        yield return new WaitForSeconds(2f);
        buildAnimator.SetTrigger("BuildChane");
        bossAnimator.SetTrigger("BossChange");
        cam7.Priority = 16;

        yield return new WaitForSeconds(2f);
        player1Animator.SetTrigger("SkysunTranslateChange");
        player1Animator.SetTrigger("SkysunAnimChange");
        bossAnimator.SetTrigger("BossChange");
        //buildAnimator.SetTrigger("BuildChane");
        cam8.Priority = 16;


        yield return new WaitForSeconds(1.3f);
        blackScreenPanel.SetActive(true);
        int scene = SceneManager.GetActiveScene().buildIndex + 1;
        PlayerPrefs.SetInt("Level", scene);
        SceneManager.LoadScene(scene);
    }
}
