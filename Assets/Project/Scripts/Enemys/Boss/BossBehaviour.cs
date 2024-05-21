using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossBehaviour : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public int scene;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        
        if (currentHealth <= 0)
            StartCoroutine("Death");

    }
    IEnumerator Death()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(scene);
    }
}
