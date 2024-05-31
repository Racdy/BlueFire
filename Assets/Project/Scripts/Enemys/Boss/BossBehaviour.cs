using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossBehaviour : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;
    public BossHUD lifeBossHUD;
    public BossHeadRot bossBarrer;
    public GameObject[] explosionParticles;

    //public int scene;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        lifeBossHUD.CurrentLifeHUD(maxHealth);
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        lifeBossHUD.CurrentLifeHUD(currentHealth/ maxHealth);

        if (currentHealth <= 100)
            bossBarrer.rotationSpeed = 50;

        if (currentHealth <= 0)
        {
            StartCoroutine("Explosions");
            StartCoroutine("Death");
            bossBarrer.rotationSpeed = 0;
        }

    }
    IEnumerator Death()
    {
        yield return new WaitForSeconds(5f);
        int scene = SceneManager.GetActiveScene().buildIndex + 1;
        PlayerPrefs.SetInt("Level", scene);
        SceneManager.LoadScene(scene);
    }

    IEnumerator Explosions()
    {
        for (int i = 0; i < explosionParticles.Length; i++)
        {
            explosionParticles[i].SetActive(true);
            explosionParticles[i].GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(0.5f);
        }
        
    }
}
