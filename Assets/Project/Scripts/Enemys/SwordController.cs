using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    public int bulletDamage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Damage>(out Damage playerComponent))
        {
            playerComponent.TakeDamage(bulletDamage);
        }
    }
}
