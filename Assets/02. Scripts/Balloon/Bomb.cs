using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Tooltip("폭탄이 자동으로 사라지는 시간")] public float lifetime = 5f;

    [Header("폭발 설정")]
    [Tooltip("폭발 프리팹")] public GameObject expFab;

    [Header("데미지 설정")]
    [Tooltip("폭발 반경")] public float rad = 3f; //원의 반지름?
    [Tooltip("적에게 줄 데미지")] public int dmg = 50;
    [Tooltip("적의 레이어")] public LayerMask eLayer;


    Rigidbody2D rb;

    void Start()
    { 
        rb = GetComponent<Rigidbody2D>();
        // 일정 시간 후 폭탄을 파괴합니다.
        Destroy(gameObject, lifetime);
    }

    // 다른 오브젝트와 충돌했을 때 
    
    // coll은 (충돌지점,힘,접촉하는것의 정보)의 타입 other(상대방 콜라이더 자체에 대한 정보)트리거를 쓸려면 other 타입으로 써야한다.
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Explode();
        }
    }

    void Explode()
    {
        if (expFab != null)
        {
            //총알이 있던 위치에 폭발 프리팹 생성 //폭발 프리팹에 코드를 넣어 시간 지나면 파괴
            Instantiate(expFab, transform.position, Quaternion.identity);
        }

        // 현재 위치를 중심으로 rad반경 내의 레이어에 속한 콜라이더 감지
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, rad, eLayer);

        //
        foreach (Collider2D hit in cols)
        {
            if(hit.CompareTag("Enemy")) //적체력 스크립트 넣고, 데미지 처리 코드 작성
            {
                //적을 탐지하면 씬에서 초록색 선을 그립니다.(폭탄범위를 10으로 설정해도 작동이 안되는 현상)
                Debug.DrawRay(transform.position, hit.transform.position - transform.position, Color.green);

                EHP h = hit.GetComponent<EHP>();
                if (h != null) {   h.TakeDmg(dmg);  }
            }
        }

        Destroy(gameObject); //폭탄 오브젝트 파괴 
    }

    //디버깅을 위한 폭발 반경 시각화 //기즈모가 재생중에는 안보임... 
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rad);
    }
}