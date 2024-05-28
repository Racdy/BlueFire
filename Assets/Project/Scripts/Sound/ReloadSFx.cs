using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadSFx : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioFXs;

    public void PlayReloadSFx()
    {
        audioSource.PlayOneShot(audioFXs);
    }
}
