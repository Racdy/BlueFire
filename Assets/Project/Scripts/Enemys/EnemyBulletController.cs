using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    public GameObject decalBulletHolePrefab;
    public GameObject impactBulletPrefab;
    public int bulletDamage;

    public void OnEnable()
    {
        Invoke("bulletLife", 2f);
    }


    void bulletLife()
    {
        ObjectPool.Instance.PoolGameObject(gameObject);

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<Damage>(out Damage playerComponent))
        {
            playerComponent.TakeDamage(bulletDamage);
        }

        GameObject impactBullet = ObjectPool.Instance.GetGameObjectOfType(impactBulletPrefab.name, true);
        impactBullet.transform.position = other.contacts[0].point;
        impactBullet.transform.rotation = Quaternion.identity;
        impactBullet.SetActive(true);

        GameObject decalBulletHole = ObjectPool.Instance.GetGameObjectOfType(decalBulletHolePrefab.name, true);
        decalBulletHole.transform.position = other.contacts[0].point + new Vector3(0.002f, 0.002f, -0.002f);
        decalBulletHole.transform.rotation = Quaternion.FromToRotation(Vector3.back, other.contacts[0].normal);
        decalBulletHole.SetActive(true);


        ObjectPool.Instance.PoolGameObject(gameObject);

    }
}
