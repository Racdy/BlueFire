using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitMedBehaviour : MonoBehaviour
{
    public bool kitMedEnable;

    private void Start()
    {
        kitMedEnable = true;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent<Damage>(out Damage playerComponent) && kitMedEnable && Input.GetKeyDown(KeyCode.E))
        {
            if (playerComponent.currentLife < playerComponent.maxLife)
            {
              
                playerComponent.ReLife();
                kitMedEnable = false;
               
            }
 
        }
    }
}
