using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDroneCollision : MonoBehaviour
{
    public GameObject impactBulletPrefab;
    public int hitDamage;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Damage>(out Damage playerComponent))
        {
            playerComponent.TakeDamage(hitDamage);
        }

        GameObject impactBullet = ObjectPool.Instance.GetGameObjectOfType(impactBulletPrefab.name, true);
        impactBullet.transform.position = collision.contacts[0].point;
        impactBullet.transform.rotation = Quaternion.identity;
        impactBullet.SetActive(true);

        Debug.Log("Colision");
    }
}
