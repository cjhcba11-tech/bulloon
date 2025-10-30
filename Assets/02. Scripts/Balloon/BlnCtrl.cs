using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlnCtrl : MonoBehaviour
{
    
    [Tooltip("벌룬 일반적인 상태")] public Sprite dSprite;
    [Tooltip("벌룬 떠오르는 상태")] public Sprite cSprite;

    private SpriteRenderer sr;

    void Start()
    {
        // SpriteRenderer 컴포넌트 가져오기
        sr = GetComponent<SpriteRenderer>();
        // 시작 시 기본 이미지 설정
        sr.sprite = dSprite;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            // W키를 누르는 동안 충전 이미지로 교체
            sr.sprite = cSprite;
            
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            // W키를 떼면 기본 이미지로 복귀
            sr.sprite = dSprite;
            
        }
    }
}