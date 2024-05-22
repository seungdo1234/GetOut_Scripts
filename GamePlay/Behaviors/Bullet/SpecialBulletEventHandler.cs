using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpecialBulletEventHandler : SpecialWeaponController
{

    protected override void OnEnable()
    {
        base.OnEnable();
        topDownController.OnSpecialFireEvent += SpecialBulletFireEvent;
    }

    private void SpecialBulletFireEvent(bool isPress)
    {
        if (isPress && !isDelay)
        {
            if (waitDelayCoroutine != null)
            {
                StopCoroutine(waitDelayCoroutine);
            }

            if (gameObject.activeSelf)
            {
                waitDelayCoroutine = StartCoroutine(WaitSpecialWeaponDelayTime());   
            }
            
            anim.SetTrigger(isSpecialFire);
        }
    }

    private void FireSpecialBullet() // 애니메이션 이벤트
    {
        float damage = specialWeaponData.atkPercent * FlightDataManager.Instance.PlayerFlightStat.CurrentStat.AtkDamage;
        SpecialBullet special = GameManager.Instance.Pool.SpawnFromPool(EPoolObjectType.SpecialBullet)
            .ReturnMyConponent<SpecialBullet>();
        
        special.transform.position = specialWeaponData.weaponPivots[CurBulletCount % specialWeaponData.bulletsPerShot].position;
        special.SpecialBulletInit(damage, specialWeaponData.weaponSpeed , specialWeaponData.bulletAnimator);

        if (--CurBulletCount == 0)
        {
            topDownController.OnSpecialFireEvent -= SpecialBulletFireEvent;
            StopCoroutine(waitDelayCoroutine);
            gameObject.SetActive(false);
        }
    }
}