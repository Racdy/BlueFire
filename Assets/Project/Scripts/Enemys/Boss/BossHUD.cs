using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHUD : MonoBehaviour
{
    public Image lifeBarBoss;

    public void CurrentLifeHUD(float currentLife)
    {
        lifeBarBoss.fillAmount = currentLife;
    }
}
