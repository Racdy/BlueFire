using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueEnemy : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject dialoguePanel;
    public string dilogue;
    public CinemachineVirtualCamera virtualCamera;
    public CinemachineBrain brain;

    public float dialogueTime;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerPrincipal"))
        {
            StartCoroutine("ShowDialogue");
        }
    }

    public IEnumerator ShowDialogue()
    {
        dialoguePanel.SetActive(true);
        dialogueText.text = dilogue;
        virtualCamera.Priority = 12;
        brain.m_IgnoreTimeScale = true;
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(dialogueTime);
        Time.timeScale = 1f;
        brain.m_IgnoreTimeScale = false;
        virtualCamera.Priority = 0;
        dialogueText.text = "";
        dialoguePanel.SetActive(false);
        this.gameObject.GetComponent<BoxCollider>().enabled = false;

    }
}
