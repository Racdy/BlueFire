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

        skysung.Dead(currentLife);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)){
            TakeDamage();
        }
        
    }

    void TakeDamage()
    {
        currentLife -= 10;
        currentShield -= 10;
        lifeBar.CurrentLife(currentLife/maxLife);
        shieldBar.CurrentShiedl(currentShield / maxShield);

        skysung.Dead(currentLife);
    }

}
