using UnityEngine;

[System.Serializable]
public class SpecialWeaponData
{
    [Header("# Special Weapon Data")]
    public EWeaponType weaponType;
    public float atkPercent;
    public Transform[] weaponPivots;
    public int bulletCount;
    public int bulletsPerShot;
    public float weaponAtkDelay;
    public float weaponSpeed;
    public RuntimeAnimatorController bulletAnimator;
}