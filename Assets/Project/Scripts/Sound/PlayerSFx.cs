using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFx : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioFXs;

    public void PlaySFx()
    {
        audioSource.PlayOneShot(audioFXs);
    }
}
