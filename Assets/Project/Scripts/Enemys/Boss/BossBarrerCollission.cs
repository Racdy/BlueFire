using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBarrerCollission : MonoBehaviour
{
    public int hitDamage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Damage>(out Damage playerComponent))
        {
            playerComponent.TakeDamage(hitDamage);
        }
    }
}
