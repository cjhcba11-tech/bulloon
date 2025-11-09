using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //UI클릭 감지를 위해 필요

public class BoxDetector : MonoBehaviour
{
    [Header("클릭감지")]
    [SerializeField, Tooltip("감지반경")] float rad = 2f;
    [SerializeField, Tooltip("상자 레이어마스크")] LayerMask bLayer;
    [SerializeField, Tooltip("상자 Tag")] string bTag = "Box";
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GM.instance != null && GM.instance.isPause) { return; } //내부UI 상태여서 일시정지인지 확인하고 그렇다면 클릭으로 핸드 소모되는걸 멈춤

        if(Input.GetMouseButtonDown(0)) //마우스 왼쪽클릭시
        {
            //UI 위에서 클릭이 발생했다면 상자열기를 실행하지 않고 종료
            if(EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) { return; }

            OpenBoxA();
        }
    }
    
    void OpenBoxA() //오픈박스 에어리어
    {
        if (GM.instance != null && GM.instance.UseHand()) //GM 존재 확인, UseHand가 트루를 반환했을때 실행 (자원확인)
        {
            //마우스 클릭 지점의 2D월드 좌표를 추출
            Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //클릭위치 주변의 모든 콜라이더를 감지한다.
            Collider2D[] hits = Physics2D.OverlapCircleAll(clickPos, rad, bLayer);

            if(hits.Length > 0 ) //감지된 상자가 있을때 실행
            {
                foreach (Collider2D hit in hits) //감지된 모든 콜라이더 문자열을 순회
                {
                    if (hit.CompareTag(bTag)) //상자 태그 확인
                    {
                        Box boxS = hit.GetComponent<Box>(); //박스 스크립트 가져옴

                        if (boxS != null)
                        {
                            boxS.OpenBox();
                        }
                    }
                }
            }

            else // 감지된 상자가 없다면 핸드를 다시 추가
            {
                GM.instance.AddHand(1);
            }
        }


  

    }
    //void OnDrawGizmosSelected() //감지 반경을 기즈모 
    //{
    //    Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    Gizmos.color = Color.cyan;
    //    Gizmos.DrawWireSphere(mousePos, rad);
    //}
}
