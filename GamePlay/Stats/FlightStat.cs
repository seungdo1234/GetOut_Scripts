using System;
using UnityEditor.Animations;
using UnityEngine;
[Serializable]
public class FlightStat
{
    [Header("# Flight Stat")]
    public float MoveSpeed;
    public float AtkDamage;
    public float MaxHealth;
    public EFlightStatus EFlightStatus;

    [Header("# Bullet Animator")]
    public RuntimeAnimatorController BulletAnimator;
    
    [Header("# Player Stat")]
    public int bulletNum;
    public float bulletAngle;
    public float bulletSpeed;
    public float atkDelay;
}
