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
            StartCoroutine("Death");
            bossBarrer.rotationSpeed = 0;
        }

    }
    IEnumerator Death()
    {
        yield return new WaitForSeconds(3f);
        int scene = SceneManager.GetActiveScene().buildIndex + 1;
        PlayerPrefs.SetInt("Level", scene);
        SceneManager.LoadScene(scene);
    }
}
