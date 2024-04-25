using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class WeaponController : MonoBehaviour
{
    public Animator skysungAnimator;
    public AnimatorStateInfo skysungStateInfo;
    public SkysungController skysungController;

    public Image weapongIcon;

    public GameObject[] weapons;
    public Sprite[] weaponIcons;

    public GameObject pointer;

    public float bulletDamage;
    public float bulletForce;

    public GameObject rifleA34Bullet;
    private GameObject rifleA34BulletTMP;

    public CinemachineVirtualCamera aimCamera;
    public int priorityBoost = 10;


    // Start is called before the first frame update
    void Start()
    {
        InactiveWeapons();
        skysungController = GetComponent<SkysungController>();
    }

    // Update is called once per frame
    void Update()
    {
        skysungStateInfo = skysungAnimator.GetCurrentAnimatorStateInfo(1);

        if (Input.GetMouseButtonDown(1) && skysungStateInfo.IsName("IdleRifleA34"))
        {
            skysungAnimator.SetTrigger("AimingRA34");
            if(aimCamera.Priority < 10)
            {
                StartAIM();
                skysungController.currentCamera = SkysungController.CameraSyle.COMBAT;
                
            }
            else
            {
                CancelAIM();
                skysungController.currentCamera = SkysungController.CameraSyle.BASIC;

            }
        }

        if(Input.GetAxis("Mouse ScrollWheel") < 0){

        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            weapongIcon.sprite = weaponIcons[0];
            skysungAnimator.SetTrigger("RifleA34");
            skysungAnimator.SetBool("DroneCanon", false);
            weapons[0].SetActive(true);
            weapons[1].SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            weapongIcon.sprite = weaponIcons[1];
            skysungAnimator.SetBool("RifleA34", false);
            skysungAnimator.SetBool("DroneCanon", true);
            weapons[0].SetActive(false);
            weapons[1].SetActive(true);
        }

        skysungStateInfo = skysungAnimator.GetCurrentAnimatorStateInfo(3);
        if (Input.GetMouseButton(0) && ((skysungStateInfo.IsName("AimingRifleA34") || (skysungStateInfo.IsName("AimingShotRifleA34")))))
        {
            Shoot();
        }
        else
        {
            skysungAnimator.SetBool("AIMRA34Shoot", false);
        }
    }

    void InactiveWeapons()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }
    }

    void Shoot()
    {
        skysungAnimator.SetBool("AIMRA34Shoot", true);
        rifleA34BulletTMP = Instantiate(rifleA34Bullet, pointer.transform.position, Quaternion.identity);
        rifleA34BulletTMP.transform.forward = Camera.main.transform.forward;
        rifleA34BulletTMP.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * bulletForce, ForceMode.Impulse);
    }

    public void StartAIM()
    {
        aimCamera.Priority += priorityBoost;
    }

    public void CancelAIM()
    {
        aimCamera.Priority -= priorityBoost;
    }
    
}
