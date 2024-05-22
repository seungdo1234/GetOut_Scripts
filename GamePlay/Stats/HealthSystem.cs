using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private FlightStatHandler statHandler;

    public event Action OnDamage;
    public event Action OnHeal;
    public event Action OnDeath;

    [SerializeField]private float curHealth;
    public float CurHealth => curHealth;
    public float MaxHealth => statHandler.CurrentStat.MaxHealth;
    private void Awake()
    {
        statHandler = GetComponent<FlightStatHandler>();
    }

    private void Start()
    {
        curHealth = statHandler.CurrentStat.MaxHealth;
    }

    // amount가 +인경우 Heal, -인 경우 damage
    public void ChangeHealth(float amount)
    {
        // 살아있는 상태가 아니라면 모든 이벤트가 발동되지 않도록 설정
        if (statHandler.CurrentStat.EFlightStatus != EFlightStatus.Alive) return;
        
        curHealth += amount;
        curHealth = Mathf.Clamp(curHealth, 0, MaxHealth);

        if(amount >= 0)
        {
            OnHeal?.Invoke();
        }
        else
        {
            OnDamage?.Invoke();
        }
        
        if (curHealth <= 0f)
        {
            statHandler.Death();
            CallDeathEvent();
            return;
        }
        
    }

    private void CallDeathEvent()
    {
        OnDeath?.Invoke();
    }
}

