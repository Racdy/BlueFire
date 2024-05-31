using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cinematic3 : MonoBehaviour
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

    public GameObject blackScreenPanel;

    public GameObject RA34;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine("Scene");
    }

    public IEnumerator Scene()
    {
        player1Animator.SetTrigger("SkysungChangeAnim");
        yield return new WaitForSeconds(2f);
        cam2.Priority = 11;
        yield return new WaitForSeconds(2f);
        cam3.Priority = 13;
        yield return new WaitForSeconds(4f);
        cam4.Priority = 14;
        yield return new WaitForSeconds(3f);
        player.transform.Translate(0f,0f,2.8f);
        cam5.Priority = 15;
        player1Animator.SetTrigger("SkysungChangeAnim");
        yield return new WaitForSeconds(3f);
        cam6.Priority = 16;
        player1Animator.SetTrigger("SkysungChangeAnim");
        yield return new WaitForSeconds(3f);
        player.transform.Translate(-0.3f, 0f, .2f);
        player1Animator.SetTrigger("SkysungChangeAnim");
        cam7.Priority = 17;
        yield return new WaitForSeconds(2f);
        cam8.Priority = 18;
        player1Animator.SetTrigger("SkysungChangeAnim");
        RA34.SetActive(true);
        yield return new WaitForSeconds(2f);
        blackScreenPanel.SetActive(true);
        int scene = SceneManager.GetActiveScene().buildIndex + 1;
        PlayerPrefs.SetInt("Level", scene);
        SceneManager.LoadScene(scene);
    }
}
