using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;
using TMPro; //Text(TMP) 사용 //Text Mesh Pro

public class GM : MonoBehaviour
{
    public static GM instance; //싱글론 패턴 //유일

    [Header("자원")]
    [Tooltip("남은 폭탄 갯수")] public int bombC = 10; //Count
    [Tooltip("남은 핸드 갯수")] public int handC = 10;

    [Header("UI연결")]
    [SerializeField, Tooltip("폭탄 표시할 UI")] TextMeshProUGUI bombT; //Text
    [SerializeField, Tooltip("핸드 표시할 UI")] TextMeshProUGUI handT;

    [Header("게임 상태")]
    [SerializeField, Tooltip("현재 일시정지 상태인가")] bool isPause;



    private void Awake()
    {
        if (instance == null) //싱글톤 인스턴스 설정
        {
            instance = this; //씬 전환시 파괴되지 않게 하려면 DontDestroyOnLoad(gameObject)로 가능? 
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateUI(); //게임 시작시 UI초기화
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool UseBomb() //폭탄 사용
    {
        if(bombC > 0)
        {
            bombC--;
            UpdateUI();
            return true; //사용함
        }
        return false; //사용못함(수량부족)
    }
    public bool UseHand() //핸드 사용
    {
        if(handC > 0)
        {
            handC--;
            UpdateUI();
            return true; //사용함
        }
        return false; //사용못함(수량부족)
    }

    public void AddBomb(int amount) //폭탄 증가 (제조)
    {
        if (amount <= 0) { return; }
        bombC += amount;
        UpdateUI();
    }
    public void AddHand(int amount) //클릭으로 상자를 열지 못했을때 사용된 핸드를 다시 반환 + 제조로 획득
    {
        if (amount <= 0) { return; }
        handC += amount;
        UpdateUI();
    }

    public void GetItem(int a) //아이템 획득
    {
        handC += a;
        UpdateUI();
    }

    void UpdateUI() //UI업데이트
    {
        if(bombT != null) {  bombT.text = "" + bombC; }  //정수를 문자열로 변환해 표시
        if(handT != null) {  handT.text = "" + handC; }
    }

    public void TgPause() //
    {
        isPause = !isPause; //상태 반전
        
        if(isPause) {  Time.timeScale = 0f; } //일시정지 상태일경우 시간의 흐름을 멈춘다.
        else {  Time.timeScale = 1f; } //게임 재개
    }
}
