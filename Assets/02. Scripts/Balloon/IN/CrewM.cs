using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using Unity.Collections;

public class CrewM : MonoBehaviour
{
    [Tooltip("싱글톤 인스턴스")] public static CrewM instance;

    const int MAX_CREW = 3; //역할별 최대 배치가능 선원 수 

    [Header("선원 자원")]
    [Tooltip("총 선원 수")] public int totalCrew = 2;
    [SerializeField, Tooltip("현재 배치할 수 있는 선원 수")] int avaCrew = 2;

    [Header("배치된 선원")]
    [Tooltip("운전")] public int steerCrew = 0;
    [Tooltip("수리")] public int repairCrew = 0;
    [Tooltip("포수")] public int canonCrew = 0;
    [Tooltip("제조")] public int craftCrew = 0;

    [Header("UI 연결")]
    [SerializeField, Tooltip("남은 선원수 UI텍스트")] TextMeshProUGUI avaCrewText;

    [SerializeField, Tooltip("운전 선원이미지(최대 3명)")] GameObject[] steerIco = new GameObject[MAX_CREW];
    [SerializeField, Tooltip("수리 선원이미지(최대 3명)")] GameObject[] repairIco = new GameObject[MAX_CREW];
    [SerializeField, Tooltip("포수 선원이미지(최대 3명)")] GameObject[] canonIco = new GameObject[MAX_CREW];
    [SerializeField, Tooltip("제조 선원이미지(최대 3명)")] GameObject[] craftIco = new GameObject[MAX_CREW];

    [Header("제조 자원")]
    [SerializeField, Tooltip("재료A 개수 텍스트UI")] TextMeshProUGUI matAText;
    [SerializeField, Tooltip("재료B 개수 텍스트UI")] TextMeshProUGUI matBText;
    [SerializeField, Tooltip("재료C 개수 텍스트UI")] TextMeshProUGUI matCText;
    [SerializeField, Tooltip("재료A")] int matA = 0;
    [SerializeField, Tooltip("재료B")] int matB = 0;
    [SerializeField, Tooltip("재료C")] int matC = 0;


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

    public void AMater(string materName, int count) //폭탄 재료 증가(상자 열면)
    {
        if (count <= 0) { return; }

        switch(materName.ToUpper()) //문자열로 변경한 열거형을 받아온것
        {
            case "A": { matA += count; break; }
            case "B": { matB += count; break; }
            case "C": { matC += count; break; }
        }

        UpdateUI(); //재료 변경될때 UI업데이트 
    }

    public void ASteer() { Asg(CrewRole.Steer); }   //UI버튼에서 온클릭으로 연결할 함수
    public void USteer() { Unasg(CrewRole.Steer); }
    public void ARepair() { Asg(CrewRole.Repair); }
    public void URepair() { Unasg(CrewRole.Repair); }
    public void ACanon() { Asg(CrewRole.Canon); }
    public void UCanon() { Unasg(CrewRole.Canon); }
    public void ACraft() { Asg(CrewRole.Craft); }
    public void UCraft() { Unasg(CrewRole.Craft); }


    public void Asg(CrewRole role) //선원 배치 증가 
    {
        if(avaCrew <=  0) { return;  } //배치 가능한 선원이 없으면 종료
        if(GetRole(role) >= MAX_CREW) { return; } //역할마다 최대 배치 인원수랑 같거나 높으면 종료

        avaCrew--; //배치가능 선원수 감소
        IncRole(role); //해당 역할의 배치 인원 증가 

        UpdateUI(); //선원 이미지 업데이트
    }

    public void Unasg(CrewRole role) //선원 배치 빼기
    {
        if(GetRole(role) <= 0) { return; } //배치 가능 성원

        avaCrew++; //배치가능 선원수 증가
        DecRole(role);
        UpdateUI(); //선원 이미지 업데이트

    }

    void UpdateUI()
    {
        if(avaCrewText != null) { avaCrewText.text = "남은 선원 :" + avaCrew.ToString(); } //남은 선원 텍스트 업데이트

        UpIco(CrewRole.Steer, steerCrew); //선원 이미지 업데이트 
        UpIco(CrewRole.Repair, repairCrew);
        UpIco(CrewRole.Canon, canonCrew);
        UpIco(CrewRole.Craft, craftCrew);

        if(matAText != null) { matAText.text = matA.ToString(); } //폭탄 재료 갯수 텍스트 업데이트
        if(matAText != null) { matBText.text = matB.ToString(); }
        if(matAText != null) { matCText.text = matC.ToString(); }
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

    public int GetRole(CrewRole role) //특정 역할에 몇명이 배치되어있는지 값 반환 
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
