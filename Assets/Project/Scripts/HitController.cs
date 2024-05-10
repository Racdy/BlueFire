using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitController : MonoBehaviour
{
    public Animator skysungAnimator;
    public WeaponController weaponController;
    public SkysungController skysungController;

    public bool hitEnable;

    private void Start()
    {
        weaponController = GetComponent<WeaponController>();
        skysungController = GetComponent<SkysungController>();
    }

    void Update()
    {
        if(weaponController.weaponEquipedIndex == 0 && skysungController.isGrounded)
            hitEnable = true;
        else
            hitEnable = false;

        if (Input.GetKeyDown(KeyCode.Mouse0) && hitEnable)
        {
            skysungAnimator.SetTrigger("Hit");
        }

    }


}
