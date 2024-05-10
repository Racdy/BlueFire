using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;
    public Animator animator;
    public bool isDead;

    public int damageAcomulated;
    public bool isCover;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }
    
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        damageAcomulated += damageAmount;

        if (currentHealth <= 0)
            StartCoroutine("Death");

        if(damageAcomulated > 40)
            StartCoroutine("Cover");
    }

    IEnumerator Death()
    {
        isDead = true;
        animator.SetBool("Death", true);
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    IEnumerator Cover()
    {
        isCover = true;
        yield return new WaitForSeconds(3f);
        isCover = false;
        damageAcomulated = 0;
    }
}
