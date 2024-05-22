using System.Collections.Generic;
using UnityEngine;

// 변동되는 스텟에 대한 관리
public class FlightStatHandler : MonoBehaviour
{
    [SerializeField] private FlightStat baseStat;
    public FlightStat CurrentStat { get; private set; }
    public List<FlightStat> statModifiers = new List<FlightStat>();

    private void Awake()
    {
        InitFlightStat();        
    }
    private void OnEnable()
    {
        InitFlightStat();
    }
    private void InitFlightStat()
    {
        CurrentStat = new FlightStat();
        CurrentStat.MoveSpeed = baseStat.MoveSpeed;
        CurrentStat.AtkDamage = baseStat.AtkDamage;
        CurrentStat.MaxHealth = baseStat.MaxHealth;
        CurrentStat.EFlightStatus= baseStat.EFlightStatus;
        CurrentStat.bulletAngle = baseStat.bulletAngle;
        CurrentStat.bulletSpeed = baseStat.bulletSpeed;
        CurrentStat.bulletNum = baseStat.bulletNum;
        CurrentStat.atkDelay = baseStat.atkDelay;
        CurrentStat.BulletAnimator = baseStat.BulletAnimator;
    }

    public void Death()
    {
        CurrentStat.EFlightStatus = EFlightStatus.Dead;
        AudioManager.instance.PlaySfx(Sfx.FlightExplosion);
    }
}
