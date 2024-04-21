using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    public Animator skysungAnimator;
    public Image weapongIcon;

    public Sprite rifleA34Icon;
    public Sprite droneCanonIcon;
    public Sprite sentinelRifleIcon;
    public Sprite handIcon;

    public GameObject rifleA34;
    public GameObject droneCanon;


    // Start is called before the first frame update
    void Start()
    {
        rifleA34.SetActive(false);
        droneCanon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            weapongIcon.sprite = rifleA34Icon;
            skysungAnimator.SetBool("RifleA34",true);
            skysungAnimator.SetBool("DroneCanon", false);
            rifleA34.SetActive(true);
            droneCanon.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            weapongIcon.sprite = droneCanonIcon;
            skysungAnimator.SetBool("RifleA34", false);
            skysungAnimator.SetBool("DroneCanon", true);
            rifleA34.SetActive(false);
            droneCanon.SetActive(true);
        }
    }
}
