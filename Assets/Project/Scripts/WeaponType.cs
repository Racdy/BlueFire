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
    //private GameObject BulletTMP;

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

    private Vector3 spreedDir;
    public float spreed;

    public bool pause;

    // Start is called before the first frame update
    void Start()
    {
        currentMunition = maxMunition;
        bullestLeft = magazineSize;
        readyToShoot = true;
        pause = false;
    }

    //Método que se encarga de calcular cuanta munición debe recoger el arma
    //en caso de recoger la misma arma
    public float PickUpMunicion(float getMunicion)
    {
        //Debug.Log("PickUpMunicion (): MUNICIÓN ACTUAL DEL ARMA ANTES DE RECARGAR " + currentMunition);
        //La munición actual del arma equipada se le suma la munición del arma soltada
        currentMunition += getMunicion;
        //Debug.Log("PickUpMunicion: Munición aumentada " + currentMunition);
        //Se obtiene el sobrante del arma para el arma del suelo y se almacena en 'getNewMunicion'
        getNewMunicion = currentMunition - maxMunition;
        //Debug.Log("PickUpMunicion: Munición devuelta (en PickUp) " + getNewMunicion);
        //Si la nueva cantidad de munición del arma equipada es mayor a la munición actual se decide limitarla a la cantidad máxima de munición
        if (currentMunition > maxMunition)
            currentMunition = maxMunition;

        //Se regresa el sobrante del arma
        return getNewMunicion;
    }

    // Update is called once per frame
    void Update()
    {

        if (pause)
            return;

        skysungRAInfoHold= skysungAnimator.GetCurrentAnimatorStateInfo(1);
        skysungDCInfoHold = skysungAnimator.GetCurrentAnimatorStateInfo(2);

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
        {
            skysungAnimator.SetBool(aimShoot.weapongShootAIM[aimShoot.weaponIndex], false);
            skysungAnimator.SetBool(aimShoot.weaponShootTorso[aimShoot.weaponIndex], false);
        }
        if (Input.GetKeyDown(KeyCode.R) && bullestLeft < magazineSize && !reloading && type == "RifleA34" && currentMunition > 0)
            Reload();
    }

    void Shoot()
    {
        if (!(skysungRAInfoAim.IsName("AimingRifleA34") || skysungRAInfoAim.IsName("AimingShotRifleA34")
            || skysungDCInfoAim.IsName("AimingDC") || skysungDCInfoAim.IsName("DCShootAIM")
            || skysungRAInfoHold.IsName("IdleRifleA34") || skysungDCInfoHold.IsName("IdleDronCanon")
            || skysungRAInfoHold.IsName("RA34HoldTorso") || skysungDCInfoHold.IsName("DCShootTorso")))
        {
            return;
        }

        muzzleFlash.Play();

        this.gameObject.SendMessage("PlaySFx", SendMessageOptions.DontRequireReceiver);

        readyToShoot = false;
        Ray rayShoot;
        if ((skysungRAInfoHold.IsName("IdleRifleA34") || skysungDCInfoHold.IsName("IdleDronCanon")
            || skysungRAInfoHold.IsName("RA34HoldTorso") || skysungDCInfoHold.IsName("DCShootTorso")) && !aimShoot.isAIM)
            rayShoot = new Ray(pointer.transform.position, pointer.transform.TransformDirection(Vector3.down));
        else
            rayShoot = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

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



        //GameObject BulletTMP = Instantiate(Bullet, pointer.transform.position, Quaternion.identity);
        //GameObject BulletTMP = BulletPool.Instance.RequestBullet();
        GameObject BulletTMP = ObjectPool.Instance.GetGameObjectOfType(Bullet.name, true);

        BulletTMP.transform.position = pointer.transform.position;
        BulletTMP.transform.rotation = Quaternion.identity;

        //Forward de la bala dependiendo si es disparo desde el cadera o torso (AIM)
        if ((skysungRAInfoHold.IsName("IdleRifleA34") || skysungDCInfoHold.IsName("IdleDronCanon")
            || skysungRAInfoHold.IsName("RA34HoldTorso") || skysungDCInfoHold.IsName("DCShootTorso")) && !aimShoot.isAIM)
        { 
            float x = Random.Range(-spreed, spreed);
            float y = Random.Range(-spreed, spreed);
            spreedDir = targetPoint - pointer.transform.position + new Vector3(x, y, 0f);
            skysungAnimator.SetBool(aimShoot.weaponShootTorso[aimShoot.weaponIndex], true);
            BulletTMP.transform.forward = pointer.transform.TransformDirection(Vector3.down);
        }
        else
        {
            spreedDir = targetPoint - pointer.transform.position;
            skysungAnimator.SetBool(aimShoot.weapongShootAIM[aimShoot.weaponIndex], true);
            BulletTMP.transform.forward = Camera.main.transform.forward;
        }

        BulletTMP.SetActive(true);
        BulletTMP.GetComponent<Rigidbody>().velocity = Vector3.zero;
        BulletTMP.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        BulletTMP.GetComponent<Rigidbody>().AddForce(spreedDir.normalized * bulletForce, ForceMode.Impulse);

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
        skysungAnimator.SetBool("Reload", true);
        this.gameObject.SendMessage("PlayReloadSFx", SendMessageOptions.DontRequireReceiver);
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    void ReloadFinished()
    {
        skysungAnimator.SetBool("Reload", false);
        bullestLeft = magazineSize;
        if (bullestLeft > currentMunition)
            bullestLeft = currentMunition;
        reloading = false;
    }

}
