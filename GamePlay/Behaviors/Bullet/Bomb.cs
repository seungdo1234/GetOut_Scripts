using System;
using System.Collections;
using UnityEngine;

public class Bomb : PoolObject
{
    private Rigidbody2D rigid;
    private bool isFinish;
    public bool IsFinish => isFinish;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void DropBomb(float bombSpeed)
    {
        rigid.velocity = transform.up * bombSpeed;
    }

    // 제한 시간 이후 터지는 것으로 사용할 것이기에 따로 예외처리를 넣어두지 않음
}