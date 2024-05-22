using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ExplosionEffect : Effect
{
    [SerializeField] private float scanRadius;
    [SerializeField] private LayerMask targetLayer;
    private float damage;
    
    public void Init(float damage)
    {
        this.damage = damage;
    }
    private void ScanEnemiesInRadius() // 애니메이션 이벤트
    {
        RaycastHit2D []  EnemyHits = Physics2D.CircleCastAll(transform.position, scanRadius, Vector2.zero, 0 , targetLayer);

        foreach (RaycastHit2D enemy in EnemyHits)
        {
            enemy.collider.gameObject.GetComponent<HealthSystem>().ChangeHealth(-damage);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, scanRadius);
    }
}