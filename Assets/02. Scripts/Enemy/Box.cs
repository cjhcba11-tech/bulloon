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

    public void OpenBox()
    {
        if (!isOpen && anim != null)
        {
            isOpen = true;
            anim.SetTrigger("Open"); //애니메이션 파라미터 설정

            if (GM.instance != null) //아이템획득
            {
                GM.instance.UseHand(); //손(박스수집) 사용
                //GM.instance.GetItem(1); //획득갯수 재료 수집은 좀 있다 내부만들고
            }

            Destroy(gameObject, 1f); //애니 재생시간 후 파괴하게 함
        }
    }
}
