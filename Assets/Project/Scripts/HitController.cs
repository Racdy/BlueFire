using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitController : MonoBehaviour
{
    public Animator skysungAnimator;
    public WeaponController weaponController;
    public SkysungController skysungController;

    public bool hitEnable;

    public AudioSource puchSound;

    public bool pause;

    private void Start()
    {
        weaponController = GetComponent<WeaponController>();
        skysungController = GetComponent<SkysungController>();
        pause = false;
    }

    void Update()
    {
        if(weaponController.weaponEquipedIndex == 0 && skysungController.isGrounded)
            hitEnable = true;
        else
            hitEnable = false;

        if (pause)
            return;

        if (Input.GetKeyDown(KeyCode.Mouse0) && hitEnable)
        {
            skysungAnimator.SetTrigger("Hit");
            puchSound.Play();
        }

    }



}
