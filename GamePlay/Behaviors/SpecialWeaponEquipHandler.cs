using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialWeaponEquipHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] specialWeaponObjects;

    private GameObject activeWeapon ;

    private void Awake()
    {
        activeWeapon = specialWeaponObjects[0];
    }

    public void EquipSpecialWeapon(EWeaponType weaponType)
    {

         if ( activeWeapon.activeSelf)
        {
            activeWeapon.SetActive(false);
        }

        activeWeapon = specialWeaponObjects[(int)weaponType - 1];
        activeWeapon.SetActive(true);
    }
}
