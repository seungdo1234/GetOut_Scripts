using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class TopDownShooting : MonoBehaviour
{
    [SerializeField] private Transform weaponPivot;
    [SerializeField] private LayerMask targetLayer;
    public LayerMask TargetLayer => targetLayer;

    protected FlightStatHandler flightStat;
    protected Rigidbody2D rigid;

    protected virtual void Awake()
    {
        flightStat = GetComponent<FlightStatHandler>();
        rigid = GetComponent<Rigidbody2D>();
    }

    protected void Shooting()
    {
        var currentStat = FlightDataManager.Instance.PlayerFlightStat.CurrentStat;
        BulletShooting(currentStat.bulletNum, currentStat.bulletAngle, weaponPivot.position, currentStat.AtkDamage, currentStat.bulletSpeed, currentStat.BulletAnimator);
    }

    protected void Shooting(AttackSO attackSO)
    {
        BulletShooting(attackSO.BulletNum, attackSO.BulletAngle, weaponPivot.position, attackSO.AtkDamage, attackSO.BulletSpeed, attackSO.BulletAnimator, attackSO);
    }

    protected void Shooting(AttackSO attackSO, Transform position)
    {
        BulletShooting(attackSO.BulletNum, attackSO.BulletAngle, position.position, attackSO.AtkDamage, attackSO.BulletSpeed, attackSO.BulletAnimator, attackSO);
    }

    protected void Shooting(AttackSO attackSO, float angle)
    {
        SpawnBullet(angle, weaponPivot.position, attackSO.AtkDamage, attackSO.BulletSpeed, attackSO.BulletAnimator, attackSO);
    }

    protected void Bombing(AttackSO attackSO, float bombSpeed, float explodeDelay)
    {
        Bomb bomb = GameManager.Instance.Pool.SpawnFromPool(EPoolObjectType.Bomb).ReturnMyConponent<Bomb>();
        if (bomb != null)
        {
            bomb.transform.position = weaponPivot.position;
            bomb.DropBomb(bombSpeed);
            StartCoroutine(DelayShooting(explodeDelay, attackSO, bomb.transform));
        }
    }

    private IEnumerator DelayShooting(float explodeDelay, AttackSO attackSO, Transform bomb)
    {
        yield return new WaitForSecondsRealtime(explodeDelay);
        Shooting(attackSO, bomb);
        bomb.gameObject.SetActive(false);
    }

    private void BulletShooting(int numberOfProjectiles, float angleSpace, Vector3 position, float damage, float speed, RuntimeAnimatorController bulletAnimator, AttackSO attackSO = null)
    {
        float minAngle = -(numberOfProjectiles / 2f) * angleSpace + 0.5f * angleSpace;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float angle = minAngle + i * angleSpace;
            SpawnBullet(angle, position, damage, speed, bulletAnimator, attackSO);
        }
    }

    private void SpawnBullet(float angle, Vector3 position, float damage, float speed, RuntimeAnimatorController bulletAnimator, AttackSO attackSO)
    {
        Bullet bullet = GameManager.Instance.Pool.SpawnFromPool(EPoolObjectType.Bullet).ReturnMyConponent<Bullet>();
        if (bullet != null)
        {
            bullet.transform.position = position;
            bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
            bullet.BulletInit(damage, speed, bulletAnimator, targetLayer);
        }
        else
        {
            Debug.LogError("Bullet Null Error");
        }
    }
}
