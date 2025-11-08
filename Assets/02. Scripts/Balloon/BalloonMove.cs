using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BalloonMove : MonoBehaviour
{
    [Header("상승 설정")]
    [Tooltip("상승 힘")] public float ascentForce = 2f;
    [Tooltip("키 눌린상태에서 최대속도")] public float maxAscent = 5f;

    [Header("하강 설정")]
    [Tooltip("w키를 안누를때 받는 중력")] public float weakGravity = 0.1f;
    [Tooltip("안누를때 받는 최대 중력")] public float normalGravity = 0.5f;

    [Header("높이 한계 설정")] // 높이 한계를 설정하는 변수들
    [Tooltip("벌룬 최대 상승 높이")] public float topLimitY = 10f;
    [Tooltip("벌룬 최소 하강 높이")] public float bottomLimitY = -5f;

    

    Rigidbody2D rb;
    int curSteerCrew = 0; //현재 배치된 운전 선원 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            enabled = false; //이 스크립트를 꺼버림
        }
        else
        {
            // 시작 시 약한 중력으로 설정하여 천천히 하강
            rb.gravityScale = weakGravity;
        }
    }

    void Update()
    {
        // 1. 높이 제한 확인 및 적용 (CheckHeightLimits 함수를 호출)
        CheckHeightLimits();

        // 2. 상승 입력 감지
        if (Input.GetKey(KeyCode.W))
        {
            if (CanAscend()) { Ascend(); }// W 키를 누르고 있으면 Ascend 함수를 호출합니다.(운전 선원이 배치되었는지 확인되면 상승 )
            else { ApplyWeakGravity(); }  // W 키를 떼었을 때 ApplyWeakGravity 함수를 호출합니다.
        }
    }

    bool CanAscend()
    {
        //CrewM crewManager = CrewM.instance; 

        if(CrewM.instance == null) { return false; }

        //크루메니져에서 운전에 1명이상 배치되었는지 확인 
        return CrewM.instance.GetRole(CrewM.CrewRole.Steer) > 0;
    }

    void OnCollisionStay2D(Collision2D coll)
    {

        if (Input.GetKey(KeyCode.W)) //상승중이면 지면충돌보다 우선
        {
            return; //ascend()가 작동하도록 이 로직을 나감
        }

        if (coll.gameObject.CompareTag("Ground")) //충돌할 오브젝트가 그라운드 태그를 가지고 있다면
        {
            if (rb.velocity.y < 0) //하강속도를 멈추고 지면에 고정 
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
            }
        }

    }
    void CheckHeightLimits() // 높이 제한을 확인하고 강제로 조정하는 역할 
    {
        Vector3 currentPosition = transform.position;

        // --- 상승 한계 (Top Limit) ---
        // 현재 Y 위치(currentPosition.y)가 최대 높이(topLimitY)보다 높으면 실행
        if (currentPosition.y > topLimitY)
        {
            // Y 위치를 최대 한계로 고정 (transform.position)
            currentPosition.y = topLimitY;
            transform.position = currentPosition;

            // Y축 속도를 0으로 설정하여 멈춤 (rb.velocity)
            if (rb != null)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
            }
        }

        // --- 하강 한계 (Bottom Limit) ---
        // 현재 Y 위치가 최소 높이(bottomLimitY)보다 낮으면 실행
        if (currentPosition.y < bottomLimitY)
        {
            // Y 위치를 최소 한계로 고정 (transform.position)
            currentPosition.y = bottomLimitY;
            transform.position = currentPosition;

            // Y축 속도를 0으로 설정하여 멈춤 (rb.velocity)
            if (rb != null)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
            }
        }
    }

    void Ascend() // W 키를 눌렀을 때 상승시키는 역할 
    {
        // 상승 중에는 일반 중력 스케일을 적용 (rb.gravityScale)
        rb.gravityScale = normalGravity;

        // 현재 수직 속도(rb.velocity.y)가 최대 상승 속도(maxAscentSpeed)보다 낮을 때만 실행
        if (rb.velocity.y < maxAscent)
        {
            // AddForce를 사용하여 지속적인 상승 힘을 적용합니다. 
            // (Vector2.up * ascentForce : 힘의 방향과 크기)
            // (ForceMode2D.Force : 힘을 적용하는 방식)
            rb.AddForce(Vector2.up * ascentForce, ForceMode2D.Force);
        }
    }

    void ApplyWeakGravity() // W 키를 떼었을 때 약한 중력을 적용하는 역할 
    {
        // Rigidbody의 중력 스케일을 약하게 설정하여 천천히 떨어지게 합니다.
        rb.gravityScale = weakGravity;
    }
}