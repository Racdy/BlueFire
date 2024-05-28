using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public float recoverTime;
    public bool reShield;

    public bool isTuto;

    public Color viejoColor;
    public Color nuevoColor;
    public Material material;

    // Start is called before the first frame update
    void Start()
    {

        material.EnableKeyword("_EMISSION");

        if (isTuto)
        {
            currentLife = 60f;
            currentShield = 0;

            lifeBar.CurrentLife(currentLife / maxLife);
            shieldBar.CurrentShiedl(currentShield);
        }
        else
        {
            currentLife = maxLife;
            currentShield = maxShield;

            lifeBar.CurrentLife(currentLife);
            shieldBar.CurrentShiedl(currentShield);
        }  

        reShield = false;
    }

    private void Update()
    {
        if(currentShield > 0)
            material.SetColor("_EmissionColor", viejoColor);
        else
            material.SetColor("_EmissionColor", nuevoColor);

    }

    public void TakeDamage(int damageAmount)
    {
        reShield = false;
        if (currentShield > 0)
            currentShield -= damageAmount;
        else
            currentLife -= damageAmount;

        if (currentLife < 0)
        {
            Death();
            return;
        }

        
        lifeBar.CurrentLife(currentLife/maxLife);
        shieldBar.CurrentShiedl(currentShield / maxShield);

        if(!isTuto)
            Invoke("ReShieldEnable", 7f);
    }

    private void ReShieldEnable()
    {
        reShield = true;
        StartCoroutine("IncrementarValor");
    }

    public void ReLife()
    {
        currentLife += 40f;
        if (currentLife > maxLife)
            currentLife = maxLife;

        lifeBar.CurrentLife(currentLife / maxLife);
    }

    private IEnumerator IncrementarValor()
    {
        float tiempoInicial = Time.time;
        float valorInicial = currentShield;

        while (Time.time < tiempoInicial + 3f)
        {
            if (!reShield)
                break;
            // Calcula el nuevo valor interpolado
            currentShield = Mathf.Lerp(valorInicial, maxShield, (Time.time - tiempoInicial) / 3f);
            shieldBar.CurrentShiedl(currentShield / maxShield);
            //Debug.Log(reShield);
            //Debug.Log(currentShield);
            yield return null; // Espera al siguiente frame
        }

        // Asegúrate de que el valor final sea exacto
        //currentShield = maxShield;
    }

    public void Death()
    {
        skysung.isDead = true;
    }

}
