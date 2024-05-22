using System;
using System.Collections;
using UnityEngine;

public class AutoFlightHandler : TopDownShooting
{
    [SerializeField] private float rotationDelay;

    private Transform targetPos;
    private Coroutine flightCoroutine;

    private void Start()
    {
        targetPos = FlightDataManager.Instance.PlayerFlightStat.transform;
        AimFlight();
    }

    private void AimFlight()
    {
        if(flightCoroutine != null)
        {
            StopCoroutine(flightCoroutine);
        }

        flightCoroutine = StartCoroutine(AimFlightAttack());
    }

    private IEnumerator AimFlightAttack()
    {
        float time = 0f;
        while (time < rotationDelay)
        {
            time += Time.deltaTime;
            Vector2 dir = (targetPos.position - transform.position).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            // 각도 계산하는 부분 수정 필요
            transform.rotation = Quaternion.Euler(0, 0, -90 + angle);
            yield return null;
        }
        PlayAutoFlightAttack();
    }

    private void PlayAutoFlightAttack()
    {
        rigid.velocity = transform.up * flightStat.CurrentStat.MoveSpeed;
    }
}
