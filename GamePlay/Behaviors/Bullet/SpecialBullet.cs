using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBullet : PoolObject
{
    [SerializeField] private LayerMask targetLayer;
    private Rigidbody2D rigid;
    private Animator anim;
    private float damage;
    
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public void SpecialBulletInit(float damage, float cannonSpeed, RuntimeAnimatorController bulletAnimator)
    {
        anim.runtimeAnimatorController = bulletAnimator;
        rigid.velocity = cannonSpeed * transform.up;
        this.damage = damage;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsLayerMatched (targetLayer.value,other.gameObject.layer))
        {
            ExplosionEffect explosionEffect = GameManager.Instance.Pool.SpawnFromPool(EPoolObjectType.Effect).ReturnMyConponent<ExplosionEffect>();
            
            explosionEffect.transform.position = transform.position;
            explosionEffect.Init(damage);
            gameObject.SetActive(false);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Camera"))  
        {
            gameObject.SetActive(false);
            
        }
    }
    
    private bool IsLayerMatched(int layerMask, int objectLayer)
    {
        return layerMask == (layerMask | (1 << objectLayer));
    }
}
