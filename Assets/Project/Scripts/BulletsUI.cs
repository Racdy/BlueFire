using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BulletsUI : MonoBehaviour
{
    public TextMeshProUGUI municionCount;
    public TextMeshProUGUI maxMunicionCount;
    private WeaponType weaponType;
    public WeaponController weaponController;

    // Update is called once per frame
    void Update()
    {
        weaponType = weaponController.weapons[weaponController.weaponIndex].GetComponent<WeaponType>();
        if (weaponController.weaponEquipedIndex == 0)
        {
            municionCount.text = "";
            maxMunicionCount.text = "";
        }
        else
        {
            if (weaponType.type == "RifleA34")
            {
                municionCount.text = "" + weaponType.bullestLeft + "|";
                maxMunicionCount.text = "" + weaponType.currentMunition;
            }
            else if (weaponType.type == "DroneCanon")
            {
                municionCount.text = "" + weaponType.bullestLeft;
                maxMunicionCount.text = "";
            }
        }
    }
}
