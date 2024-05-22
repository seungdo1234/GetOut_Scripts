using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMultipleAttackHandler : TopDownShooting
{
    [SerializeField] private AttackSO attackSO;
    [SerializeField] private List<Transform> firePositions;
    private Coroutine attackCoroutine;

    private void Start()
    {
        PlayAutoMultipleAttack();
    }

    private void PlayAutoMultipleAttack()
    {
        // 이미 코루틴이 실행중이라면 중지하고 다시 시작 (겹치는 것 방지)
        if(attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }

        attackCoroutine = StartCoroutine(AutoMultipleAttackCoroutine());
    }

    private IEnumerator AutoMultipleAttackCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(attackSO.AtkDelay);
        while (flightStat.CurrentStat.EFlightStatus == EFlightStatus.Alive)
        {
            // firePoints들을 순회하면서 발사
            foreach(Transform firePosition in firePositions)
            {
                Shooting(attackSO, firePosition);
                yield return wait;
            }
        }
    }
}
