using UnityEngine;

public class PlayerAnimationController : FlightAnimationController
{
    private readonly int isMoving = Animator.StringToHash("isMoving");
    private readonly int Hit = Animator.StringToHash("isInvisibility");

    [SerializeField] private PlayerHealthUIHandler playerHealthUIHandler;
    protected override void Start()
    {
        base.Start();
        topDownController.OnMoveEvent += Move;
        healthSystem.OnDamage += TakeDamage;
    }
    
    private void Move(Vector2 direction)
    {
        AudioManager.instance.PlaySfx(Sfx.PlayerMove);
        anim.SetBool(isMoving, direction.magnitude != 0);
    }
    private void TakeDamage()
    {
        anim.SetTrigger(Hit);
        playerHealthUIHandler.UpdateHeart();
        AudioManager.instance.PlaySfx(Sfx.PlayerHit);
    }
    
    private void InvisibilityOn()
    {
        gameObject.layer = 9;
    }
    private void InvisibilityOff()
    {
        gameObject.layer = 6;
    }

    private void PlayerDead()
    {
        gameObject.SetActive(false);
        GameManager.Instance.GameOverUI.SetActive(true);
    }
}