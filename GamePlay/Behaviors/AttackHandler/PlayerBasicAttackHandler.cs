using System.Collections;
using UnityEngine;

public class PlayerBasicAttackHandler : TopDownShooting
{
    
    private Coroutine attackCoroutine;
    private bool isBasicAttackLock;
    private void Start()
    {
        PlayAutoBasicAttack();
        
    }
    
    public void PlayAutoBasicAttack()
    {
        // 이미 코루틴이 실행중이라면 중지하고 다시 시작 (겹치는 것 방지)
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }

        attackCoroutine = StartCoroutine(AutoBasicAttackCoroutine());
    }
    private IEnumerator AutoBasicAttackCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(FlightDataManager.Instance.PlayerFlightStat.CurrentStat.atkDelay);

        while (flightStat.CurrentStat.EFlightStatus == EFlightStatus.Alive)
        {
            if (!isBasicAttackLock)
            {
                Shooting();   
            }
            yield return wait;
        }
    }

    public void BasicAttackLock(bool isTrue)
    {
        isBasicAttackLock = isTrue;
    }

    public void AttackUp()
    {
        FlightDataManager.Instance.PlayerFlightStat.CurrentStat.AtkDamage++;
        Debug.Log($"AttackDamage { FlightDataManager.Instance.PlayerFlightStat.CurrentStat.AtkDamage}");
    }

    public void BulletCountUp()
    {
        FlightDataManager.Instance.PlayerFlightStat.CurrentStat.bulletNum++;
        Debug.Log($"BulletNum {FlightDataManager.Instance.PlayerFlightStat.CurrentStat.bulletNum}");
    }
}