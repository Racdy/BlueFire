using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cinematic1 : MonoBehaviour
{
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;
    public CinemachineVirtualCamera cam3;

    public Animator playerShip1Animator;
    public Animator droneShip1Animator;
    public Animator droneShip2Animator;

    public GameObject playerShip;
    public GameObject dronShip1;
    public GameObject dronShip2;

    public GameObject dialoguePanel;    
    public TextMeshProUGUI dialogue;

    public GameObject blackScreenPanel;

    private void Start()
    {
        StartCoroutine("Scene");
    }

    public IEnumerator Scene()
    {
        Debug.Log("PRIMERA ESCENA");
        playerShip1Animator.SetTrigger("ChangeCam");
        droneShip1Animator.SetTrigger("ChangeAnim");
        droneShip2Animator.SetTrigger("ChangeAnim");

        yield return new WaitForSeconds(4.2f);

        dronShip1.SetActive(false);
        dronShip2.SetActive(false);

        //SCENE2-------------------------------------------
        playerShip1Animator.SetTrigger("ChangeCam");
        yield return new WaitForSeconds(0.8f);
        cam2.Priority = 10;
        dialoguePanel.SetActive(true);
        dialogue.text = "Skysung: MAYDAY. MAYDAY. ¡Me han dado! Están justo detrás de mí";
        yield return new WaitForSeconds(3f);
        dialogue.text = "Skysung: ¡Tendré que hacer un aterrizaje de emergencia!";
        yield return new WaitForSeconds(3f);
        dialogue.text = "General: ¡Intenta acercarte lo que mas pueda al punto de encuentro!";
        yield return new WaitForSeconds(3f);
        dialogue.text = "General:¡El ARTEFACTO debe ser entregado! !No debe caer en manos de los Phyrikigakus!";

        //SCENE3------------------------------------------
        yield return new WaitForSeconds(3.4f);
        playerShip1Animator.SetTrigger("ChangeCam");
        yield return new WaitForSeconds(0.8f);
        cam3.Priority = 11;
        dialogue.text = "Skysung: ¡Esta cosa ya no aguanta!";
        yield return new WaitForSeconds(6.55f);
        blackScreenPanel.SetActive(true);
        dialogue.text = "";

        //Cambiar de escena
        yield return new WaitForSeconds(2f);
        int scene = SceneManager.GetActiveScene().buildIndex + 1;
        PlayerPrefs.SetInt("Level", scene);
        SceneManager.LoadScene(scene);
    }
}
