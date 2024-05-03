using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsController : MonoBehaviour
{
    public GameObject decalBulletHole;
    public Vector3 target;
    public bool hit;

    public void OnEnable()
    {
        Destroy(gameObject,3f);
    }

    /*private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, 1f * Time.deltaTime);
    }*/

    private void OnCollisionEnter(Collision other)
    {
        Instantiate(decalBulletHole,
                     other.contacts[0].point + new Vector3(0.002f, 0.002f, -0.002f),
                     Quaternion.FromToRotation(Vector3.back, other.contacts[0].normal));

        Destroy(gameObject);
    }

}
