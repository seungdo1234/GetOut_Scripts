using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ZapperEventHandler : SpecialWeaponController
{
    [SerializeField] private Zapper zapperProjectile;

    private PlayerBasicAttackHandler autoBasicAttackHandler;
    
    [SerializeField] private float maxFireDuration = 50.0f;
    private float bulletCountPerSecond;
    private Coroutine fireZapperCoroutine;
    
    protected override void Awake()
    {
        base.Awake();
        autoBasicAttackHandler = topDownController.GetComponent<PlayerBasicAttackHandler>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        topDownController.OnSpecialFireEvent += ZapperFireEvent;
        bulletCountPerSecond = CurBulletCount / maxFireDuration;
    }

    private void ZapperFireEvent(bool isPress)
    {
        
        if (anim.GetBool(isSpecialFire) || !isDelay)
        {
            anim.SetBool(isSpecialFire, isPress);

            StopFireZapperCoroutine();
            fireZapperCoroutine = StartCoroutine(WaitSpecialWeaponDelayTime());
        }
    }

    private void FireZapper()
    {
        zapperProjectile.Init(specialWeaponData.atkPercent * FlightDataManager.Instance.PlayerFlightStat.CurrentStat.AtkDamage);
        zapperProjectile.gameObject.SetActive(true);
        autoBasicAttackHandler.BasicAttackLock(true);
        StartCoroutine(FireLaserCoroutine());
    }

    private void DisableZapper()
    {
        zapperProjectile.gameObject.SetActive(false);
        autoBasicAttackHandler.BasicAttackLock(false);
    }

    private IEnumerator FireLaserCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.01f);
        AudioManager.instance.PlayZapperSfx(true);
        
        while (zapperProjectile.gameObject.activeSelf)
        { 
            CurBulletCount --;
            if (CurBulletCount < 0)
            {
                DisableZapper();
                topDownController.OnSpecialFireEvent -= ZapperFireEvent;
                gameObject.SetActive(false);
                break;
            }
            yield return wait;
        }
        
        AudioManager.instance.PlayZapperSfx(false);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        DisableZapper();
        AudioManager.instance.PlayZapperSfx(false);
        StopFireZapperCoroutine();
    }

    private void StopFireZapperCoroutine()
    {
        if (fireZapperCoroutine != null)
        {
            StopCoroutine(fireZapperCoroutine);
        }
    }
}