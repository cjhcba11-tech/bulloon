using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //씬전환에 필요함
using UnityEngine.UI; //UI요소에 필요

public class PHP : MonoBehaviour
{
    [Header("체력 설정")]
    [Tooltip("최대 체력")] public int maxHP = 500;
    [Tooltip("현재 체력")] public int curHP;

    [Header("게임 오버")]
    [Tooltip("게임오버 씬 이름")] public string gameOverS = "GameOver";
    [Tooltip("적탄환 태그 1데미지")] public string eBullTag = "EBullet";
    [Tooltip("적탄환 태그 5데미지")] public string eBullTag5 = "EBullet5";
    [Tooltip("적탄환 태그 골드획득")] public string eBullTag2 = "EBullet2";

    [Header("UI 연결")]
    [Tooltip("체력게이지 실린더")] public Slider hpSlider;

    [Header("수리선원 체력회복")]
    [Tooltip("1명당 회복량")] public float baseRepair = 10f;
    [Tooltip("회복 간격(초) ")] public float repairInter = 1f; //1초
    float regenTime = 0f; //마지막 회복 후 경과된 시간을 누적하며 카운트하는곳

    // Start is called before the first frame update
    void Start()
    {
        curHP = maxHP;

        if(hpSlider != null)
        {
            hpSlider.maxValue = maxHP;
            hpSlider.value = curHP;
        }
    }

    // Update is called once per frame
    void Update()
    {
        RegenHP(); //체력 회복
    }

    void RegenHP() //수리선원 배치시 체력회복
    {
        if(CrewM.instance == null) { return; } //인스턴스가 없으면 작동안함

        int repairCrew = CrewM.instance.repairCrew; //수리에 배치된 인원수를 가져옴 
        if(repairCrew > 0 && curHP < maxHP) //수리선원 배치 && 최대체력이 아니라면
        {
            regenTime += Time.deltaTime; //경과된 시간을 누적시킴

            if(regenTime >= repairInter) //경과시간이 설정된 체력회복시간(1초)를 넘어갔을경우
            {
                //총 회복 = 1명당회복량(10) * 수리선원수(1~3) * 회복간격(1초)
                int heal = Mathf.RoundToInt(baseRepair * repairCrew * repairInter);
                Heal(heal); //회복 실행
                regenTime = 0f;  //누적된 타이머 리셋
            }

        }
        else
        {
            regenTime = 0f; //선원이 0명일 경우 타이머 누적하지 않고 리셋 
        }
    }

    void Heal(int heal)
    {
        if(heal <= 0) { return; } //총 회복 힐량이 0이나 0보다 작으면 취소

        curHP += heal; //현재 체력에 추가
        curHP = Mathf.Min(curHP, maxHP); //최대체력을 초과화지 않도록 제한 (둘중 작은값을 대입)
        UpdateHPBar();
    }

    private void OnTriggerEnter2D(Collider2D other) //적 총알이 열기구에 닿았을때 (충돌감지)
    {
        
        int rand = Random.Range(1, 6); //골드 랜덤값 1~5

        if (other.CompareTag(eBullTag)) //충돌한 오브젝트의 태그가 EnemyBullet인지 확인 
        {
            Destroy(other.gameObject); //충돌한 총알제거
            TakeD(1); //데미지 받음
        }
        else if (other.CompareTag(eBullTag5))
        {
            Destroy(other.gameObject); //충돌한 총알제거
            TakeD(5); //데미지 받음
        }
        else if (other.CompareTag(eBullTag2))
        {
            Destroy(other.gameObject); //충돌한 총알제거
            GM.instance.AddGold(rand);//골드 상승
        }
    }

    public void TakeD(int dmg)
    {
        curHP -= dmg;
        UpdateHPBar(); 

        if(curHP <= 0 )
        {
            EndGame(); 
        }
    }

    void EndGame()
    {
        SceneManager.LoadScene(gameOverS); //게임오버 씬으로 전환
    }

    void UpdateHPBar() //플레이어 체력바 함수 호출 (이게 있어야 화면에 적용되어 보인다)
    {
        if (hpSlider != null)
        {
            hpSlider.value = curHP;
        }
    }
}
