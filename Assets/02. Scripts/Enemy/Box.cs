using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class Box : MonoBehaviour
{
    [Header("보물상자")]
    [SerializeField, Tooltip("상자 이동속도")] float movsS = 0.5f;
    [SerializeField, Tooltip("상자 파괴시간")] float lifeT = 15f;

    [Header("상호작용")]
    [SerializeField, Tooltip("상자 애니")] Animator anim;
    bool isOpen = false; //열렸는지 체크

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeT); //상자 파괴

    }

    // Update is called once per frame
    void Update()
    {
        //좌측 이동 (왼쪽, 속도, 일정한 이동)
        transform.Translate(Vector2.left * movsS * Time.deltaTime, Space.World);
    }

    //void OnMouseDown() //마우스로 클릭했을때 호출되는 내장함수 (상자열기) - 범위열기라서 필요없음
    //{
    //    if (!isOpen && anim != null)
    //    {
    //        isOpen = true;

    //        // 애니메이션 트리거/파라미터 설정 (자동재생방지)
    //        anim.SetTrigger("Open");

    //        //GameManager.Instance.GetITem(); //아이템 획득을 게임매니저 로직에서 가져와 호출

    //        Destroy(gameObject, 1f); //애니 재생시간 후 파괴하게 함 
    //    }
    //}

    public void OpenBox()
    {
        if (!isOpen && anim != null)
        {
            isOpen = true;
            anim.SetTrigger("Open"); //애니메이션 파라미터 설정
                                     //
            Destroy(gameObject, 1f); //애니 재생시간 후 파괴하게 함
        }
        else
        {
            Debug.Log("상자가 이미 열린상태 or 애니메이션이 없습니다.");
        }
    }
}
