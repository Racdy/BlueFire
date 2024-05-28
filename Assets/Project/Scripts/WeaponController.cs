using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.ParticleSystemJobs;
using Cinemachine;
using System.Security.Claims;

public class WeaponController : MonoBehaviour
{
    //ANIMATOR---------------------------------------------------
    public Animator skysungAnimator;
    public AnimatorStateInfo skysungStateInfo;
    public SkysungController skysungController;

    public AnimatorStateInfo skysungRAInfoHold;
    public AnimatorStateInfo skysungDCInfoHold;

    public AnimatorStateInfo skysungRAInfoAim;
    public AnimatorStateInfo skysungDCInfoAim;

    private string[] weaponHold = { "Punch", "RifleA34", "DroneCanon" };
    private int holdIndex;

    private string[] weapongAIM = { "Punch1", "AimingRA34", "AimingDC" };
    public string[] weapongShootAIM = { "Punch2", "AIMRA34Shoot", "AIMDCShoot" };

    public string[] weaponShootTorso = { "Punch3", "TorsoRA34", "TorsoDC" };

    //UI---------------------------------------------------------
    public Image weapongIcon;
    public Sprite[] weaponIcons;

    public Image crosshairIcon;
    public Sprite[] crosshair;

    //PREFABS----------------------------------------------------
    public GameObject[] weapons;
    public int weaponIndex;
    public int preWeaponIndex;

    //CAMERA----------------------------------------------------
    public CinemachineVirtualCamera aimingCamera;
    public int priorityBoost = 10;

    //EQUIPED--------------------------------------------------
    private ArrayList weaponsEquiped;
    public int weaponEquipedIndex;
    private Sprite[] iconEquiped;
    private int iconIdex;

    //GRAB WEAPONS LOGIC---------------------------------------
    private bool isRA34 = false;
    private bool isDC = false;
    public bool enableIK;
    public GameObject weaponGrabbed;
    public GameObject WeaponDropped;
    public GameObject[] WeaponsDropped;
    public string type;
    public float currtenMunicion;
    public float getMunicion;
    public float municion;
    public bool destroyWeapon;
    public float newMunicionActual;
    public bool isGetNewMunicion;

    public bool isAIM;

    public bool isTuto;

    // Start is called before the first frame update
    void Start()
    {
        isGetNewMunicion = false;
        isAIM = false;
        destroyWeapon = false;
        weaponsEquiped = new ArrayList();
        iconEquiped = new Sprite[2];

        skysungController = GetComponent<SkysungController>();

        InactiveWeapons();

        weaponsEquiped.Add(weapons[0]);
        weaponsEquiped.Add(weapons[1]);
        iconEquiped[0] = weaponIcons[0];
        iconEquiped[1] = weaponIcons[1];

        weaponIndex = 1;
        holdIndex = 1;

        weaponEquipedIndex = 0;
        iconIdex = 0;

        crosshairIcon.enabled = false;


        skysungRAInfoHold = skysungAnimator.GetCurrentAnimatorStateInfo(1);
        skysungDCInfoHold = skysungAnimator.GetCurrentAnimatorStateInfo(2);

        skysungRAInfoAim = skysungAnimator.GetCurrentAnimatorStateInfo(3);
        skysungDCInfoAim = skysungAnimator.GetCurrentAnimatorStateInfo(4);
        if (!isTuto)
        {
            InvokeRepeating("MouseScroll", 0f, 0.01f);
            InvokeRepeating("ScrollWeapon", 0f, 0.01f);
            InvokeRepeating("GrabWeapon", 0f, 0.015f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (skysungController.isDead)
            return;

        skysungRAInfoHold = skysungAnimator.GetCurrentAnimatorStateInfo(1);
        skysungDCInfoHold = skysungAnimator.GetCurrentAnimatorStateInfo(2);

        skysungRAInfoAim = skysungAnimator.GetCurrentAnimatorStateInfo(3);
        skysungDCInfoAim = skysungAnimator.GetCurrentAnimatorStateInfo(4);


        if (Input.GetMouseButtonDown(1) && (skysungRAInfoHold.IsName("IdleRifleA34") || skysungDCInfoHold.IsName("IdleDronCanon")))
        {

            if (aimingCamera.Priority < 10)
                StartAIM();
            else
                CancelAIM();
        }

        if ((skysungRAInfoAim.IsName("AimingRifleA34") || skysungDCInfoAim.IsName("AimingDC")) && weaponEquipedIndex == 0)
            CancelAIM();

        //ShootLogic();

    }

    public void MouseScroll()
    {
        if (Input.GetAxis("Mouse ScrollWheel") == 0)
            return;

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            weaponEquipedIndex = (weaponEquipedIndex + 1) % weaponsEquiped.Count;
            iconIdex = (iconIdex + 1) % weaponsEquiped.Count;

        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            weaponEquipedIndex = (weaponEquipedIndex - 1 + weaponsEquiped.Count) % weaponsEquiped.Count;
            iconIdex = (iconIdex - 1 + weaponsEquiped.Count) % weaponsEquiped.Count;
        }
    }

    public void ScrollWeapon()
    {
        if (weaponEquipedIndex != 0)
        {
            skysungAnimator.SetBool(weaponHold[holdIndex], true);
            weapons[weaponIndex].SetActive(true);
            weapongIcon.sprite = iconEquiped[iconIdex];

        }
        else
        {
            weapons[weaponIndex].SetActive(false);
            skysungAnimator.SetBool(weaponHold[holdIndex], false);
            weapongIcon.sprite = iconEquiped[iconIdex];

        }
    }

    public void GrabWeapon()
    {

        if ((skysungRAInfoAim.IsName("AimingRifleA34") || skysungRAInfoAim.IsName("AimingShotRifleA34") || skysungDCInfoAim.IsName("AimingDC") || skysungDCInfoAim.IsName("DCShootAIM")))
            return;

        if (Input.GetKeyDown(KeyCode.E) && (isRA34 || isDC))
        {
            InactiveWeapons();

            grabeItem();
            weaponEquipedIndex = 1;
            iconIdex = 1;
            skysungAnimator.SetBool(weaponHold[holdIndex], true);

            //Debug.Log("ITS GRABB! ");

        }
    }

    void InactiveWeapons()
    {
        for (int i = 0; i < weapons.Length; i++)
            weapons[i].SetActive(false);
    }


    public void StartAIM()
    {
        isAIM = true;
        enableIK = true;

        crosshairIcon.enabled = true;

        aimingCamera.Priority = 11;
        skysungAnimator.SetBool(weapongAIM[weaponIndex], true);
        skysungController.currentCamera = SkysungController.CameraSyle.COMBAT;
    }

    public void CancelAIM()
    {
        isAIM = false;
        enableIK = false;

        crosshairIcon.enabled = false;

        aimingCamera.Priority = 9;
        skysungAnimator.SetBool(weapongAIM[weaponIndex], false);
        skysungController.currentCamera = SkysungController.CameraSyle.BASIC;
    }

    public void grabeItem()
    {
        //Antes de cambiar arma. Se obtiene la información del arma que está equipada actualmente

        WeaponType weaponCurrten = weapons[weaponIndex].GetComponent<WeaponType>(); //Arma que tengo equiapada actualmente antes de cambiar
        type = weaponCurrten.type;                                                  //Tipo del arma (RifleA34 o DroneCanon
        currtenMunicion = weaponCurrten.currentMunition;                            //Munición del arma actual. Se asigna a la varible 'currtenMunicion'
        //Debug.Log("Munición del arma dejada " + currtenMunicion);
        getMunicion = municion;                                                     //Munición del arma en el suelo. Obtenida en OnTriggerEnter
        //Debug.Log("WeapongController: Municion del arma de suelo" + municion);


        //Dependiendo del arma que se recogerá se asigna la configuración deseada
        if (isRA34)
        {
            preWeaponIndex = 2;
            weaponIndex = 1;
            weaponsEquiped[1] = weapons[weaponIndex];
            iconEquiped[1] = weaponIcons[weaponIndex];
            weapongIcon.sprite = weaponIcons[weaponIndex];
            crosshairIcon.sprite = crosshair[weaponIndex];

            weapons[weaponIndex].SetActive(true);
            holdIndex = 1;
            skysungAnimator.SetBool(weaponHold[holdIndex + 1], false);

            DropWeapon(); 
        }

        if (isDC)
        {
            preWeaponIndex = 1;
            weaponIndex = 2;
            weaponsEquiped[1] = weapons[weaponIndex];
            iconEquiped[1] = weaponIcons[weaponIndex];
            weapongIcon.sprite = weaponIcons[weaponIndex];
            crosshairIcon.sprite = crosshair[weaponIndex];

            weapons[weaponIndex].SetActive(true);
            holdIndex = 2;
            skysungAnimator.SetBool(weaponHold[holdIndex - 1], false);

            DropWeapon();

        }
    }

    public void DropWeapon()
    {
        //Es posible 'recoger' la misma arma, en estos casos simplemente se le añadirá la munición que el arma en suelo tenga
        //en caso de que la munición del arma en suelo sea 0, la misma desaparece
        //Es posible recoger un arma distinta RifleA34 -> DroneCanon ó DroneCanon -> RifleA34. En este caso, se usa la información
        //almacenada del arma que tiene equipada para dropearla con la información integrada y asignar la información del arma que esta en suelo

        //Se obtiene mas información (WeponType) del arma en suelo
        WeaponType weaponGrabed = weapons[weaponIndex].GetComponent<WeaponType>();  //Arma recogida. Puede ser diferente o la misma;


        if ((isDC && (type == "RifleA34")) || (isRA34 && (type == "DroneCanon"))) //Diferente arma a la que tengo equipada. Debo desactivar e instanciar
        {                                                                       //dejando la munición de la otra arma y asignando la nueva
            
            weaponGrabed.currentMunition = getMunicion; //Se asigna la munición del arma recogida 'getMunicion' como municion del arma equipada 

            WeaponDropped = Instantiate(WeaponsDropped[preWeaponIndex], transform.position,Quaternion.identity);  //Se instancia la arma soltada
            WeaponDropped.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward.normalized * 2f, ForceMode.Impulse); // Se crea un impulso para arrojar el arma
            PickUpController pickUpController = WeaponDropped.GetComponent<PickUpController>();  //Se obtiene la componente PickUpController del arma soltada para asignar la 
                                                                                                 //la información del arma soltada
            pickUpController.currentMunicion = currtenMunicion;//A la arma soltada se le asgina la munición 'currentMunicio' que tenía equipada previamente
            destroyWeapon = true; //Se indica que el arma debe ser destruida

        }
        else //Misma arma. Debo calcular las nuevas balas
        {   //Si el arma al que se le obtuvo la munición es 0, el arma debe ser destruida

            //Debug.Log("Munición restante " + weaponGrabed.PickUpMunicion());
            newMunicionActual = weaponGrabed.PickUpMunicion(getMunicion);

            if (newMunicionActual <= 0)
            {
                destroyWeapon = true;
                //Debug.Log("DESTROY agarrar munición");
            }
            else 
            {
                //Se obtiene el restante de munición y se almacena el isGetrNeMunicion. Se envia a OnTriggerStay()
                isGetNewMunicion= true;
            }
            //Debug.Log("WeapongController: Munición devuelta: " + newMunicionActual);


        }
    }

    private void OnTriggerEnter(Collider other) //Sistema de trigger
    {
        destroyWeapon = false;

        //Se obtiene la componente PickUpController (información de munición)
        //y se asgina a 'municion' la informacion de la cantidad de municion que contiene el arma (currentMunicion)
        //(PUEDE QUE allMunition de PickUpController NO SE OCUPE)

        //En caso de que se recoja el arma; Ir al método grabeItem()

        if (other.CompareTag("DroppedRA34"))
        {
            isRA34 = true;
            isDC = false;//Se hace este cambio en isDC para no estar tomando ambas armas
            PickUpController ra34 = other.GetComponent<PickUpController>();
            //Debug.Log("Munición actual " + ra34.currentMunicion);
            municion = ra34.currentMunicion;
            //Debug.Log("OnTriggerEnter: MUNICION DE ARMA EN SUELO " + municion);
        }

        if (other.CompareTag("DroppedDC"))
        {
            isDC = true;
            PickUpController DC34 = other.GetComponent<PickUpController>();
            //Debug.Log("Munición actual " + DC34.currentMunicion);
            municion = DC34.currentMunicion;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        destroyWeapon = false;
        if (other.CompareTag("DroppedRA34"))
            isRA34 = false;

        if (other.CompareTag("DroppedDC"))
            isDC = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("DroppedRA34"))
        {
            if (isGetNewMunicion)
            {
                PickUpController ra34 = other.GetComponent<PickUpController>();
                ra34.currentMunicion = newMunicionActual;
                isGetNewMunicion = false;
            }
            //Debug.Log("STAY en RA");
            
            if (destroyWeapon)
            {
                isRA34 = false;
                Destroy(other.gameObject);
            }
        }

        if (other.CompareTag("DroppedDC"))
        {
            if (isGetNewMunicion)
            {
                PickUpController DC34 = other.GetComponent<PickUpController>();
                DC34.currentMunicion = newMunicionActual;
                isGetNewMunicion = false;
            }

            if (destroyWeapon)
            {
                isDC = false;
                Destroy(other.gameObject);
            }
        }
    }
}
