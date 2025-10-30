using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Tooltip("폭탄이 자동으로 사라지는 시간")] public float lifetime = 5f;

    [Header("폭발 설정")]
    [Tooltip("폭발 스프라이트")] public GameObject expFab;

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
            //총알이 있던 위치에 폭발 프리팹 생성
            Instantiate(expFab, transform.position, Quaternion.identity);

            
        }

        Destroy(gameObject);
    }
}