using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpecialWeaponController : MonoBehaviour
{
   [SerializeField] protected SpecialWeaponData specialWeaponData;
   [SerializeField] private SpecialWeaponUIHandler specialWeaponUIHandler;
   protected Animator anim;
   protected TopDownController topDownController;
   protected readonly int isSpecialFire = Animator.StringToHash("isSpecialFire");
   protected bool isDelay;
   private WaitForSeconds waitDelayTime;
   protected Coroutine waitDelayCoroutine;
   
   [field:SerializeField]public int CurBulletCount { get; protected set; }
   public EWeaponType WeaponType => specialWeaponData.weaponType; 
   protected virtual void Awake()
   {
      topDownController = transform.root.GetComponent<TopDownController>();
      anim = GetComponent<Animator>();
      waitDelayTime = new WaitForSeconds(specialWeaponData.weaponAtkDelay);
   }

   protected virtual void OnEnable()
   {
      isDelay = false;
      CurBulletCount = specialWeaponData.bulletCount;
      specialWeaponUIHandler.SpecialWeaponInit(this);
   }

   protected IEnumerator WaitSpecialWeaponDelayTime()
   {
      isDelay = true;
      yield return waitDelayTime;
      isDelay = false;
   }

   protected virtual void OnDisable()
   {
      if (waitDelayCoroutine != null)
      {
         StopCoroutine(waitDelayCoroutine);
      }
   }
}
