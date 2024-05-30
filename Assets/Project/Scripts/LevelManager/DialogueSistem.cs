using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueSistem : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject dialoguePanel;
    public string dilogue;

    public float dialogueTime;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerPrincipal"))
        {
            dialoguePanel.SetActive(true);
            dialogueText.text = dilogue;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerPrincipal"))
        {
            StartCoroutine("ShowDialogue");
        }
    }

    public IEnumerator ShowDialogue()
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(dialogueTime);
        dialogueText.text = "";
        dialoguePanel.SetActive(false);
        
    }
}
