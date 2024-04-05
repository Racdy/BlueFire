using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Animator skysungAnimator;

    public GameObject rifleA34;

    // Start is called before the first frame update
    void Start()
    {
        rifleA34.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            skysungAnimator.SetBool("RifleA34",true);
            rifleA34.SetActive(true);
        }
    }
}
