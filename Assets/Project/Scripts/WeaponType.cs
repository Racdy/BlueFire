using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class WeaponType : MonoBehaviour
{
    //ANIMATOR---------------------------------------------------
    public Animator skysungAnimator;

    public AnimatorStateInfo skysungRAInfoHold;
    public AnimatorStateInfo skysungDCInfoHold;

    public AnimatorStateInfo skysungRAInfoAim;
    public AnimatorStateInfo skysungDCInfoAim;

    //POINTERS & BULLESTS-----------------------------------------
    public GameObject pointer;

    public float bulletDamage;
    public float bulletForce;

    public GameObject Bullet;
    private GameObject BulletTMP;

    //SHOOT LOGIC---------------------------------------------
    public bool allowButtonHold;
    private bool shooting;
    private bool readyToShoot;
    private bool reloading;

    public float maxMunition;
    public float currentMunition;
    public float magazineSize;
    public float bullestLeft;
    public float bulletsShot;
    public float bulletsPerTap;

    public float timeBetweenShooting;
    public float timeBetweenShot;
    public float reloadTime;

    //WeaponController-----------------------------------------
    public WeaponController aimShoot;

    //PARTICLES-------------------------------------------------
    public ParticleSystem muzzleFlash;

    public string type;
    public float getNewMunicion;

    // Start is called before the first frame update
    void Start()
    {
        currentMunition = maxMunition;
        bullestLeft = magazineSize;
        readyToShoot = true;
    }

    public float PickUpMunicion()
    {
        currentMunition += aimShoot.getMunicion;
        getNewMunicion = currentMunition - maxMunition;
        if (currentMunition > maxMunition)
            currentMunition = maxMunition;

        return getNewMunicion;
    }

    // Update is called once per frame
    void Update()
    {
        skysungRAInfoAim = skysungAnimator.GetCurrentAnimatorStateInfo(3);
        skysungDCInfoAim = skysungAnimator.GetCurrentAnimatorStateInfo(4);

        ShootLogic();
    }

    public void ShootLogic()
    {
        if (allowButtonHold)
            shooting = Input.GetKey(KeyCode.Mouse0);
        else
            shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (readyToShoot && shooting && !reloading && bullestLeft > 0 && currentMunition > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        }

        if (!shooting)
            skysungAnimator.SetBool(aimShoot.weapongShootAIM[aimShoot.weaponIndex], false);

        if (Input.GetKeyDown(KeyCode.R) && bullestLeft < magazineSize && !reloading && type == "RifleA34" && currentMunition > 0)
            Reload();
    }

    void Shoot()
    {
        if (!(skysungRAInfoAim.IsName("AimingRifleA34") || skysungRAInfoAim.IsName("AimingShotRifleA34") || skysungDCInfoAim.IsName("AimingDC") || skysungDCInfoAim.IsName("DCShootAIM")))
        {
            return;
        }

        muzzleFlash.Play();

        readyToShoot = false;

        Ray rayShoot = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        Vector3 targetPoint;

        if (Physics.Raycast(rayShoot, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = rayShoot.GetPoint(75);
        }

        skysungAnimator.SetBool(aimShoot.weapongShootAIM[aimShoot.weaponIndex], true);

        BulletTMP = Instantiate(Bullet, pointer.transform.position, Quaternion.identity);
        BulletTMP.transform.forward = Camera.main.transform.forward;
        BulletTMP.GetComponent<Rigidbody>().AddForce((targetPoint - pointer.transform.position).normalized * bulletForce, ForceMode.Impulse);

        /*BulletTMP = Instantiate(Bullet, pointer.transform.position, Quaternion.identity);
        BulletTMP.transform.forward = Camera.main.transform.forward;
        BulletTMP.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * bulletForce, ForceMode.Impulse);*/


        bullestLeft--;
        bulletsShot--;
        currentMunition--;

        Invoke("ResetShoot", timeBetweenShooting);

        if (bullestLeft > 0 && bulletsShot > 0)
            Invoke("Shoot", timeBetweenShot);
    }

    void ResetShoot()
    {
        readyToShoot = true;
    }

    void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    void ReloadFinished()
    {
        bullestLeft = magazineSize;
        reloading = false;
    }

}
