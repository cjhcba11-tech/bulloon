using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI요소에 필요
using UnityEngine.SceneManagement; //씬전환에 필요함

public class PHP : MonoBehaviour
{
    [Header("체력 설정")]
    [Tooltip("최대 체력")] public int maxHP = 100;
    [Tooltip("현재 체력")] public int curHP;

    [Header("게임 오버")]
    [Tooltip("게임오버 씬 이름")] public string gameOverS = "GameOver";
    [Tooltip("적탄환 태그 1데미지")] public string eBullTag = "EBullet";
    [Tooltip("적탄환 태그 5데미지")] public string eBullTag5 = "EBullet5";

    [Header("UI 연결")]
    [Tooltip("체력게이지 실린더")] public Slider hpSlider;

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
        
    }

    private void OnTriggerEnter2D(Collider2D other) //적 총알이 열기구에 닿았을때 (충돌감지)
    {
        if(other.CompareTag(eBullTag)) //충돌한 오브젝트의 태그가 EnemyBullet인지 확인 
        {
            Destroy(other.gameObject); //충돌한 총알제거
            TakeD(1); //데미지 받음
        }
        else if (other.CompareTag(eBullTag5))
        {
            Destroy(other.gameObject); //충돌한 총알제거
            TakeD(5); //데미지 받음
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

        void EndGame()
        {
            SceneManager.LoadScene(gameOverS); //게임오버 씬으로 전환
        }
    }

    void UpdateHPBar() //플레이어 체력바 함수 호출 (이게 있어야 화면에 적용되어 보인다)
    {
        if (hpSlider != null)
        {
            hpSlider.value = curHP;
        }
    }
}
