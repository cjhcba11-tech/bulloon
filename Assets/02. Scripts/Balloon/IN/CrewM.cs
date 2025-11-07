using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class CrewM : MonoBehaviour
{
    [SerializeField, Tooltip("싱글톤 인스턴스")] static CrewM instance;

    const int MAX_CREW = 3; //역할별 최대 배치가능 선원 수 

    [Header("선원 자원")]
    [Tooltip("총 선원 수")] public int totalCrew = 2;
    [SerializeField, Tooltip("현재 배치할 수 있는 선원 수")] int avaCrew = 2;

    [Header("배치된 선원")]
    [SerializeField, Tooltip("운전")] int steerCrew = 0;
    [SerializeField, Tooltip("수리")] int repairCrew = 0;
    [SerializeField, Tooltip("포수")] int canonCrew = 0;
    [SerializeField, Tooltip("제조")] int craftCrew = 0;

    [Header("UI 연결")]
    [SerializeField, Tooltip("남은 선원수 UI텍스트")] TextMeshProUGUI avaCrewText;

    [SerializeField, Tooltip("운전 선원이미지(최대 3명)")] GameObject[] steerIco = new GameObject[MAX_CREW];
    [SerializeField, Tooltip("수리 선원이미지(최대 3명)")] GameObject[] repairIco = new GameObject[MAX_CREW];
    [SerializeField, Tooltip("포수 선원이미지(최대 3명)")] GameObject[] canonIco = new GameObject[MAX_CREW];
    [SerializeField, Tooltip("제조 선원이미지(최대 3명)")] GameObject[] craftIco = new GameObject[MAX_CREW];

    public enum CrewRole { Steer, Repair, Canon, Craft  }; //선원 역할을 쉽게 식별하기 위해 (열거형)사용

    private void Awake()
    {
        //싱글톤 패턴 기존 보안 (GM 오브젝트가 같은게 복사될 경우에 필요한것이라는데 필요없어보임) 
        if(instance == null) { instance = this;} //등록된 인스턴스가 없다면 이것을 전역적으로 등록
        else if (instance != this) { Destroy(gameObject); } //만약 등록된 인스턴스가 있다면 그것을 파괴

    }
    // Start is called before the first frame update
    void Start()
    {
        avaCrew = totalCrew; //시작시 모든 선원은 배치가능
        UpdateUI(); //선원 이미지 업데이트
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateUI()
    {
        if(avaCrewText != null) { avaCrewText.text = "남은 선원" + avaCrew.ToString(); } //남은 선원 텍스트 업데이트

        UpIco(CrewRole.Steer, steerCrew); //선원 이미지 업데이트 
        UpIco(CrewRole.Repair, repairCrew);
        UpIco(CrewRole.Canon, canonCrew);
        UpIco(CrewRole.Craft, craftCrew);
    }

    void UpIco(CrewRole role, int count) //선원 이미지 업데이트를 진짜로 하는곳
    {

        GameObject[] icons = new GameObject[0];

        switch (role)
        {
            case CrewRole.Steer: { icons = steerIco; break; }
            case CrewRole.Repair: { icons = repairIco; break; }
            case CrewRole.Canon: { icons = canonIco; break; }
            case CrewRole.Craft: { icons = craftIco; break; }

        }

        for (int i = 0; i < icons.Length; i++) //배열을 순회하면 활성화 또는 비활성화 
        {
            if (icons[i] != null) { icons[i].SetActive(i < count);  } //i가 현재 배치된 count보다 작으면 활성화 (0,1,2)

        }
    }


    public void Assign(CrewRole role) //선원 배치
    {
        if(avaCrew <= 0) { return; } //배치할 선원 부족시 종료
        if(GetRole(role) >= MAX_CREW) { return; } //최대 배치인원 체크

        avaCrew--; //배치 가능한 선원수 감소
        IncRole(role);

        UpdateUI(); //선원 이미지 업데이트
    }

    public void Unassign(CrewRole role) //선원 배치 취소 
    {
        if(GetRole(role) <= 0) { return;  }

        avaCrew++;
        DecRole(role); 

        UpdateUI();
    }


    void IncRole(CrewRole role) //특정역할에 선원수 배치 1증가
    {
        switch (role)
        {
            case CrewRole.Steer: { steerCrew++; break; }
            case CrewRole.Repair: { repairCrew++; break; }
            case CrewRole.Canon: { canonCrew++; break; }
            case CrewRole.Craft: { craftCrew++; break; }
        }
    }

    void DecRole(CrewRole role) //특정역할에 선원수 배치 1감소
    {
        switch (role)
        {
            case CrewRole.Steer: { steerCrew--; break; }
            case CrewRole.Repair: { repairCrew--; break; }
            case CrewRole.Canon: { canonCrew--; break; }
            case CrewRole.Craft: { craftCrew--; break; }
        }
    }

    int GetRole(CrewRole role) //특정 역할에 몇명이 배치되어있는지 값 반환 
    {
        switch (role)
        {
            case CrewRole.Steer: { return steerCrew; }
            case CrewRole.Repair: { return repairCrew; }
            case CrewRole.Canon: { return canonCrew; }
            case CrewRole.Craft: { return craftCrew; }
            default: return 0;
        }
    }


}
