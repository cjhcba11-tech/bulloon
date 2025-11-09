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

    [Header("드랍")]
    [SerializeField, Tooltip("드랍 재료의 종류")] List<MaterType> droppMater = new List<MaterType>();
    
    [SerializeField, Tooltip("드랍 재료수 min")] int minDrop = 1;
    [SerializeField, Tooltip("드랍 재료수 max")] int maxDrop = 3;

    public enum MaterType { A,B,C } //드랍 재료 종류 열거형으로 설정  


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

            //if (GM.instance != null) //아이템획득 (중복 획득때문에 삭제)
            //{
            //    GM.instance.UseHand(); //손(박스수집) 사용
            //}

            GetMateria(); //재료 얻음

            Destroy(gameObject, 1f); //애니 재생시간 후 파괴하게 함
        }
    }

    void GetMateria()
    {
        if(droppMater == null || droppMater.Count == 0) { return; } //드랍목록이 없다면 그냥 종료 

        foreach(MaterType type in droppMater) //설정된 모든 타입에 대해 랜덤 수량 드랍
        {
            int dropRand = Random.Range(minDrop, maxDrop + 1); //랜덤 드랍수량 

            if(dropRand > 0)
            {
                string materName = type.ToString(); //재료타입 문자열로 변경

                if(CrewM.instance !=  null) { CrewM.instance.AMater(materName, dropRand); } //CrewM 싱글톤을 통해 재료 적립 및 UI업데이트 요청
                
            }
        }

    }
}
