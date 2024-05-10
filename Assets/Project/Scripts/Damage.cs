using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Damage : MonoBehaviour
{
    public float maxLife;
    public float currentLife;

    public float maxShield;
    public float currentShield;

    public LifeShieldBar lifeBar;
    public LifeShieldBar shieldBar;

    public SkysungController skysung;

    // Start is called before the first frame update
    void Start()
    {
        currentLife = maxLife;
        currentShield = maxShield;

        lifeBar.CurrentLife(currentLife);
        shieldBar.CurrentShiedl(currentShield);

    }

    public void TakeDamage(int damageAmount)
    {
        if(currentShield > 0)
            currentShield -= damageAmount;
        else
            currentLife -= damageAmount;
        
        lifeBar.CurrentLife(currentLife/maxLife);
        shieldBar.CurrentShiedl(currentShield / maxShield);

    }
}
