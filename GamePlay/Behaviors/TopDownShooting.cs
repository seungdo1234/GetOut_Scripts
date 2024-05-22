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
        // 몇 도 만큼 각도를 띄울건지
        float projectilesAngleSpace =  FlightDataManager.Instance.PlayerFlightStat.CurrentStat.bulletAngle;
        // 한 번에 몇발 나갈 건지
        int numberOfProjectilePerShot =  FlightDataManager.Instance.PlayerFlightStat.CurrentStat.bulletNum;

        // 최소 각 구하기
        float minAngle = -(numberOfProjectilePerShot / 2f) * projectilesAngleSpace +
                         0.5f * projectilesAngleSpace;

        for (int i = 0; i < numberOfProjectilePerShot; i++)
        {
            float angle = minAngle + i * projectilesAngleSpace;
            SpawnBullet(angle);
        }
    }
    protected void Shooting(AttackSO attackSO)
    {
        // 몇 도 만큼 각도를 띄울건지
        float projectilesAngleSpace = attackSO.BulletAngle;
        // 한 번에 몇발 나갈 건지
        int numberOfProjectilePerShot = attackSO.BulletNum;

        // 최소 각 구하기
        float minAngle = -(numberOfProjectilePerShot / 2f) * projectilesAngleSpace +
                         0.5f * projectilesAngleSpace;

        for (int i = 0; i < numberOfProjectilePerShot; i++)
        {
            float angle = minAngle + i * projectilesAngleSpace;
            SpawnBullet(attackSO, angle);
        }
    }

    protected void Shooting(AttackSO attackSO, Transform position)
    {
        // 몇 도 만큼 각도를 띄울건지
        float projectilesAngleSpace = attackSO.BulletAngle;
        // 한 번에 몇발 나갈 건지
        int numberOfProjectilePerShot = attackSO.BulletNum;

        // 최소 각 구하기
        float minAngle = -(numberOfProjectilePerShot / 2f) * projectilesAngleSpace +
                         0.5f * projectilesAngleSpace;

        for (int i = 0; i < numberOfProjectilePerShot; i++)
        {
            float angle = minAngle + i * projectilesAngleSpace;
            SpawnBullet(attackSO, angle, position);
        }
    }

    protected void Shooting(AttackSO attackSO, float angle)
    {
        // 정해진 각도로 바로 발사
        SpawnBullet(attackSO, angle);
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

    IEnumerator DelayShooting(float explodeDelay, AttackSO attackSO, Transform bomb)
    {
        WaitForSecondsRealtime wait = new WaitForSecondsRealtime(explodeDelay);
        yield return wait;
        Shooting(attackSO, bomb.transform);
        bomb.gameObject.SetActive(false);
    }

    private void SpawnBullet(AttackSO attackSO, float angle)
    {
        Bullet bullet = GameManager.Instance.Pool.SpawnFromPool(EPoolObjectType.Bullet).ReturnMyConponent<Bullet>();
        if (bullet != null)
        {
            bullet.transform.position = weaponPivot.position; 
            bullet.transform.Rotate(0, 0, angle);
            bullet.BulletInit(attackSO.AtkDamage, attackSO.BulletSpeed, attackSO.BulletAnimator, targetLayer);   
        }
        else
        {
            Debug.Log("Bullet Null Error");
        }
    }

    private void SpawnBullet(AttackSO attackSO, float angle, Transform position)
    {
        Bullet bullet = GameManager.Instance.Pool.SpawnFromPool(EPoolObjectType.Bullet).ReturnMyConponent<Bullet>();
        if (bullet != null)
        {
            bullet.transform.position = position.position;
            bullet.transform.Rotate(0, 0, angle);
            bullet.BulletInit(attackSO.AtkDamage, attackSO.BulletSpeed, attackSO.BulletAnimator, targetLayer);
        }
        else
        {
            Debug.Log("Bullet Null Error");
        }
    }
    
    private void SpawnBullet(float angle)
    {
        Bullet bullet = GameManager.Instance.Pool.SpawnFromPool(EPoolObjectType.Bullet).ReturnMyConponent<Bullet>();
        if (bullet != null)
        {
            bullet.transform.position = weaponPivot.position;
            bullet.transform.Rotate(0, 0, angle);
            bullet.BulletInit( FlightDataManager.Instance.PlayerFlightStat.CurrentStat.AtkDamage,  FlightDataManager.Instance.PlayerFlightStat.CurrentStat.bulletSpeed, FlightDataManager.Instance.PlayerFlightStat.CurrentStat.BulletAnimator, targetLayer);
        }
        else
        {
            Debug.Log("Bullet Null Error");
        }
    }
}
