using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEfectController : MonoBehaviour
{
    public float timeDesapier;
    public void OnEnable()
    {
        Invoke("timeToDesapier", timeDesapier);
        
    }

    void timeToDesapier()
    {
        ObjectPool.Instance.PoolGameObject(gameObject);
    }
}
