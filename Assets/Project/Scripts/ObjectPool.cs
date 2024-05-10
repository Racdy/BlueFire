using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    [System.Serializable]
    public struct PrefabPool
    {
        public GameObject prefab;
        public int amountInBuffer;
    }

    public PrefabPool[] prefabs;

    public List<GameObject>[] generalPool;
    private GameObject containerObject;

    private void Awake()
    {
        Instance = this;
        containerObject = new GameObject("ObjectPool");
        generalPool = new List<GameObject>[prefabs.Length];
        int index = 0;
        foreach (PrefabPool objectPrefab in prefabs)
        {
            generalPool[index] = new List<GameObject>();
            for (int i = 0; i < objectPrefab.amountInBuffer; i++)
            {
                GameObject temp = Instantiate(objectPrefab.prefab);
                temp.name = objectPrefab.prefab.name;
                PoolGameObject(temp);
            }
            index++;
        }
    }

    public void PoolGameObject(GameObject obj)
    {
        for (int i = 0; i < prefabs.Length; i++)
        {
            if (prefabs[i].prefab.name == obj.name)
            {
                obj.SetActive(false);
                obj.transform.parent = containerObject.transform;
                obj.transform.position = containerObject.transform.position;
                generalPool[i].Add(obj);
            }
        }
    }

    public GameObject GetGameObjectOfType(string objectType, bool onlyPooled)
    {
        for (int i = 0; i < prefabs.Length; i++)
        {
            GameObject prefab = prefabs[i].prefab;
            if (prefab.name == objectType)
            {
                if (generalPool[i].Count > 0)
                {
                    GameObject pooledObject = generalPool[i][0];
                    pooledObject.transform.parent = null;
                    generalPool[i].RemoveAt(0);
                    //pooledObject.SetActive(true);
                    return pooledObject;
                }
                else if (!onlyPooled)
                {
                    return Instantiate(prefabs[i].prefab);
                }
                break;
            }
        }
        return null;
    }
}
