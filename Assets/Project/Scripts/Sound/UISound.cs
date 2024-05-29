using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISound : MonoBehaviour
{
    public AudioSource uiSource;
    public AudioClip uiHoverSound;
    public AudioClip uiClickSound;

    public void hoverSound()
    {
        uiSource.PlayOneShot(uiHoverSound);
    }

    public void clickSound()
    {
        uiSource.PlayOneShot(uiClickSound);
    }
}
