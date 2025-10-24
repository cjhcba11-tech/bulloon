using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonShooter : MonoBehaviour
{
    [Header("프리팹")]
    public GameObject bombPrefab;         // 발사할 폭탄 프리팹 (프리팹에 Rigidbody 필요)
    public Transform firePoint;           // 발사 위치/방향(예: 풍선의 노즈에 비어있는 Transform)

    [Header("발사")]
    public float minForce = 5f;           // 최소 발사 속도
    public float maxForce = 30f;          // 최대 발사 속도
    public float maxChargeTime = 2f;      // 이 시간 동안 완전히 차면 maxForce
    public bool allowAutoFireOnMax = false; // true면 최대치 도달시 자동 발사

    [Header("무중력 시간")]
    public float straightTime = 0.5f;     // 발사 후 얼마나 직선(무중력)으로 날릴지(초). BombProjectile에서도 동일값 설정 가능.

    private float chargeTimer = 0f;
    private bool charging = false;

    void Update()
    {
        // 스페이스바 누르고 있는 동안 충전
        if (Input.GetKeyDown(KeyCode.Space))
        {
            charging = true;
            chargeTimer = 0f;
        }

        if (charging && Input.GetKey(KeyCode.Space))
        {
            chargeTimer += Time.deltaTime;
            if (chargeTimer >= maxChargeTime)
            {
                chargeTimer = maxChargeTime;
                if (allowAutoFireOnMax)
                {
                    Fire();
                    charging = false;
                }
            }
            // (선택) 여기서 UI 게이지 업데이트 가능: chargeTimer / maxChargeTime
        }

        // 떼는 순간 발사
        if (charging && Input.GetKeyUp(KeyCode.Space))
        {
            Fire();
            charging = false;
        }

        // (선택) 디버그: 현재 충전 퍼센트
        // Debug.Log($"Charge: {chargeTimer/maxChargeTime * 100f:F0}%");
    }

    void Fire()
    {
        if (bombPrefab == null || firePoint == null) return;

        float t = chargeTimer / maxChargeTime;
        float force = Mathf.Lerp(minForce, maxForce, t);

        GameObject go = Instantiate(bombPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = go.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = firePoint.forward * force; // 초기 직선 속도 설정
        }
        /*
        // BombProjectile에 straightTime 값 전달 (있다면)
        BombProjectile bp = go.GetComponent<BombProjectile>();
        if (bp != null)
        {
            bp.ActivateStraightFlight(straightTime);
        }
        */
        // 리셋
        chargeTimer = 0f;
    }
}
