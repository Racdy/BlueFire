using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int poolSize;

    [SerializeField] private List<GameObject> bulletsList;

    public static BulletPool instance;
    public static BulletPool Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
        AddBulletsToPool(poolSize);
    }

    private void AddBulletsToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletsList.Add(bullet);
            //bullet.transform.parent = transform;
        }
    }

    public GameObject RequestBullet()
    {
        for(int i = 0; i < bulletsList.Count; i++)
        {
            if (!bulletsList[i].activeSelf)
            {
                //bulletsList[i].SetActive(true);
                return bulletsList[i];
            }
        }
        return null;
    }

}
