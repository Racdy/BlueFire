using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovSFx : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;
    public AudioClip[] audioFXs;

    public AudioSource walkSource;

    public bool walkEnable;
    public bool isWalking;

    private float timeByStep;

    private void Start()
    {
        isWalking = false;
    }

    public void WalkSpeed()
    {
        timeByStep = 0.35f;
    }

    public void RunSpeed()
    {
        timeByStep = 0.25f;
    }

    public void EnableWalkSFx()
    {
        walkEnable = true;
        if(!isWalking)
            StartCoroutine("PlaYStepsSFx");
    }

    public void DisableWalkSFx()
    {
        walkEnable = false;
    }
    
    public IEnumerator PlaYStepsSFx()
    {
        isWalking = true;
        while (walkEnable) 
        {
            walkSource.Play();
            yield return new WaitForSeconds(timeByStep);
        }

        isWalking = false;

    }

    public void PlayJumpSFx()
    {
        audioSource.PlayOneShot(audioFXs[1]);
    }

    public void PlayFallSFx()
    {
        audioSource.PlayOneShot(audioFXs[2]);
    }

    public void PlayPuchSFx()
    {
        audioSource.PlayOneShot(audioFXs[3]);
    }

    public void PlayDashSFx()
    {
        audioSource.PlayOneShot(audioFXs[4]);
    }
}
