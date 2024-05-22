using UnityEngine;


public class Bullet : PoolObject
{
    private Rigidbody2D rigid;
    [SerializeField]private LayerMask targetLayer;
    private float damage;
    private Quaternion origRotation;
    private Animator anim;
    
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        origRotation = transform.rotation;
    }

    // Enemy (위에서 아래로)
    public void BulletInit(float damage, float bulletSpeed, RuntimeAnimatorController animator, LayerMask targetLayer)
    {
        rigid.velocity = transform.up * bulletSpeed;
        anim.runtimeAnimatorController = animator;
        this.targetLayer = targetLayer;
        this.damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsLayerMatched(targetLayer.value, other.gameObject.layer ))
        {
            HealthSystem healthSystem = other.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.ChangeHealth(-damage);
            }
            DisableBullet();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Camera"))
        {
            DisableBullet();
        }
    }

    private void DisableBullet()
    {
        transform.rotation = origRotation;
        gameObject.SetActive(false);
    }
    
    // 레이어가 일치하는지 확인하는 메소드입니다.
    private bool IsLayerMatched(int layerMask, int objectLayer)
    {
        return layerMask == (layerMask | (1 << objectLayer));
    }
}
