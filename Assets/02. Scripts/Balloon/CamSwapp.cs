using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwapp : MonoBehaviour
{
    [Header("카메라")]
    [SerializeField, Tooltip("기본 화면 카메라")] Camera mainCam;
    [SerializeField, Tooltip("내부 화면 카메라")] Camera insideCam;
    
    
    // Start is called before the first frame update
    void Start()
    {
        if(mainCam == null) {   mainCam = Camera.main;    } //태그가 MainCamera인 카메라 찾기
        if(insideCam != null) {     insideCam.enabled = false;      } //인사이드 카메라는 꺼져있어야 함
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1)) { TgCameras(); } //마우스 우클릭 감지
    }

    void TgCameras() //토글 카메라들
    {
        if (mainCam == null && insideCam == null) //안전장치
        {
            if (GM.instance != null) { GM.instance.TgPause(); } //혹시 모르니 일시정지 상태는 변경
            return;
        }

        if (mainCam.enabled) //메인카메라가 켜져있다면
        {
            mainCam.enabled = false;
            insideCam.gameObject.SetActive(true); //게임 오브젝트 활성화(보장) 
            insideCam.enabled = true; //메인끄고 인사이드 켬
        }

        else
        {
            mainCam.enabled = true;
            insideCam.gameObject.SetActive(false); //게임 오브젝트 비활성화
            insideCam.enabled = false; //반대
        }

        if(GM.instance != null) { GM.instance.TgPause(); } //GM의 일시정지 상태 호출
    }
}
