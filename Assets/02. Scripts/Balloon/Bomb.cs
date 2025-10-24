using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float lifetime = 5f; // 폭탄이 자동으로 사라질 시간

    void Start()
    {
        // 일정 시간 후 폭탄을 파괴합니다.
        Destroy(gameObject, lifetime);
    }

    // 다른 오브젝트와 충돌했을 때 (옵션)
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 예: 지면("Ground" 태그를 가진 오브젝트)에 닿으면 폭발 등의 처리를 할 수 있습니다.
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Debug.Log("폭탄이 지면에 충돌했습니다!");
            // 여기에 폭발 효과, 사운드 등의 코드를 추가합니다.
            Destroy(gameObject);
        }
    }
}