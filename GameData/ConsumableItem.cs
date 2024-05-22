using System;
using UnityEngine;

public class ConsumableItem : PoolObject
{
    [SerializeField] private LayerMask targetLayer;
    private EConsumableItemType consumableItemType;
    private Animator animator;
    private Rigidbody2D rigid;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    public void ConsumableInit(RuntimeAnimatorController animator, EConsumableItemType consumableItemType , float downSpeed)
    {
        this.animator.runtimeAnimatorController = animator;
        this.consumableItemType = consumableItemType;
        rigid.velocity = Vector2.down * downSpeed;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsLayerMatched(targetLayer.value, other.gameObject.layer))
        {
            
            AudioManager.instance.PlaySfx(Sfx.ItemCon);
            switch (consumableItemType)
            {
                case EConsumableItemType.AttackUp:
                    other.gameObject.GetComponent<PlayerBasicAttackHandler>().AttackUp();
                    break;
                case EConsumableItemType.HealthUp:
                    other.gameObject.GetComponent<HealthSystem>().ChangeHealth(2);
                    break;
                case EConsumableItemType.BulletCountUp:
                    other.gameObject.GetComponent<PlayerBasicAttackHandler>().BulletCountUp();
                    break;
            }
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