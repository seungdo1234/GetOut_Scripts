using System;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    private TopDownController controller;
    private FlightStatHandler flightStatHandler;
    private Rigidbody2D rigid;
    
    private Vector2 movementDir = Vector2.zero;
    private void Awake()
    {
        flightStatHandler = GetComponent<FlightStatHandler>();
        controller = GetComponent<TopDownController>();
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        controller.OnMoveEvent += Move;
    }

    private void Move(Vector2 dir)
    {
        movementDir = dir;
    }
    
    public void ResetVelocity() // 플레이어 움직임이 제한될 때 호출
    {
        movementDir = Vector2.zero;
        rigid.velocity = Vector2.zero;
    }
    
    private void ApplyMovement(Vector2 dir) // 실제로 이동을 하는 함수
    {
        // 스탯 적용
        dir *= flightStatHandler.CurrentStat.MoveSpeed;

        rigid.velocity = dir;
    }
    private void FixedUpdate()
    {
        ApplyMovement(movementDir);
    }
}