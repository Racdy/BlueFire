using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeShieldBar : MonoBehaviour
{
    public Image lifeBar;
    public Image shieldBar;

    public void CurrentLife(float currentLife)
    {
        lifeBar.fillAmount = currentLife;
    }

    public void CurrentShiedl(float currentShield) 
    {
        shieldBar.fillAmount = currentShield;
    }
}
