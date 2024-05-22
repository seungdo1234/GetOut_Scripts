using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpecialWeaponUIHandler : MonoBehaviour
{
    [SerializeField] private Sprite[] specialWeaponSprites;
    [SerializeField] private TextMeshProUGUI bulletCountText;
    [SerializeField] private Image specialWeaponIcon;
    private SpecialWeaponController specialWeaponController;
    
    public void SpecialWeaponInit(SpecialWeaponController specialWeaponController)
    {
        gameObject.SetActive(true);
        this.specialWeaponController = specialWeaponController;
        specialWeaponIcon.sprite = specialWeaponSprites[(int)specialWeaponController.WeaponType - 1];
    }

    private void Update()
    {
        if (specialWeaponController.CurBulletCount < 0)
        {
            gameObject.SetActive(false);
        }
        
        bulletCountText.text = specialWeaponController.CurBulletCount.ToString();
    }
}
