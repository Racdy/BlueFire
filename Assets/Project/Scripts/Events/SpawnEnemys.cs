using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SpawnEnemys : MonoBehaviour
{
    public Transform[] spawnPoint;
    public GameObject[] enemyType;
    private int droneCount, guardianCount, sentinelCount;
    public int droneCountMax, guardianCountMax, sentinelCountMax;
    private int enemyCount;

    private BoxCollider boxCol;

    private void OnEnable()
    {
        boxCol = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerPrincipal"))
            StartCoroutine("Spawn");

    }
    public IEnumerator Spawn()
    {
        droneCount = 0;
        sentinelCount = 0;
        guardianCount = 0;
        enemyCount = 0;

        //Debug.Log("Cantidad de enemigos" + totalEnemys);
        //Debug.Log("Cantidad de spawns" + spawnPoint.Length);

        if (droneCount != droneCountMax)
        {
            for (int i = 0; i < spawnPoint.Length; i++)
            {
                GameObject droneTMP = ObjectPool.Instance.GetGameObjectOfType(enemyType[0].name, true);
                enemyCount++;
                droneTMP.transform.position = spawnPoint[i].transform.position;
                droneTMP.SetActive(true);
                droneCount++;
                if (droneCount == droneCountMax)
                    break;
                yield return new WaitForSeconds(0.5f);
            }
        }

       // Debug.Log("enemyCount" + enemyCount);

        if (sentinelCount != sentinelCountMax)
        {
            for (int i = enemyCount; i < spawnPoint.Length; i++)
            {
                GameObject sentinelTMP = ObjectPool.Instance.GetGameObjectOfType(enemyType[1].name, true);
                //Debug.Log("i" + i);
                sentinelTMP.transform.position = spawnPoint[i].transform.position;
                sentinelTMP.SetActive(true);
                enemyCount++;
                sentinelCount++;
                if (sentinelCount == sentinelCountMax)
                    break;
                yield return new WaitForSeconds(0.5f);
            }
        }

        if (guardianCount != guardianCountMax)
        {
            for (int i = enemyCount; i < spawnPoint.Length; i++)
            {
                GameObject guardianTMP = ObjectPool.Instance.GetGameObjectOfType(enemyType[2].name, true);
                enemyCount++;
                guardianTMP.transform.position = spawnPoint[i].transform.position;
                guardianTMP.SetActive(true);
                guardianCount++;
                if (guardianCount == guardianCountMax)
                    break;
                yield return new WaitForSeconds(0.5f);
            }

        }
        boxCol.enabled = false;

    }

}
