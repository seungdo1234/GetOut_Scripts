using UnityEngine;

public class FlightAnimationController : MonoBehaviour
{
    protected TopDownController topDownController;
    protected HealthSystem healthSystem;
    protected Animator anim;

    private readonly int Dead = Animator.StringToHash("Dead");
    protected readonly int Hit = Animator.StringToHash("isInvisibility");
    protected virtual void Awake()
    {
        topDownController = GetComponent<TopDownController>();
        anim = GetComponent<Animator>();
        healthSystem = GetComponent<HealthSystem>();
    }
    
    protected virtual void Start()
    {
        if(healthSystem != null)
        {
            healthSystem.OnDeath += Death;
        }
    }
    
    private void Death()
    {
        anim.SetTrigger(Dead);
    }
    
}
