using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitMedBehaviour : MonoBehaviour
{
    public bool kitMedEnable;
    public AudioSource kitMedAudioSource;
    public AudioClip kitMedClip;
    public Material kitMedMaterial;
    public Color colorEnable;

    private void Start()
    {
        kitMedEnable = true;
        kitMedMaterial.EnableKeyword("_EMISSION");
        kitMedMaterial.SetColor("_EmissionColor", colorEnable);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent<Damage>(out Damage playerComponent) && kitMedEnable && Input.GetKeyDown(KeyCode.E))
        {
            if (playerComponent.currentLife < playerComponent.maxLife)
            {
                kitMedAudioSource.PlayOneShot(kitMedClip);
                kitMedMaterial.SetColor("_EmissionColor", Color.black);
                playerComponent.ReLife();
                kitMedEnable = false;
               
            }
 
        }
    }
}
