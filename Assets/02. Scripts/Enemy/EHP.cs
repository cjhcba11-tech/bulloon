using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EHP : MonoBehaviour
{
    [Tooltip("최대 체력")] public int maxHP = 100;
    [Tooltip("현재 체력")] public int curHP;

    [Tooltip("피격시 빨간색 시간")] public float hitDur = 1.0f;

    SpriteRenderer sr; //스프라이트 랜더러 컴포넌트 참조
    Color oriC; //원래 색상 저장용 (오리지날 칼라)

    [Header("자원드롭 설정")]
    [Tooltip("드롭할 아이템상자 프리팹")] public GameObject boxPre;
    [Tooltip("상자가 튀어오르는 힘")] public float boxF = 5f;
    [Tooltip("상자 드롭 확률")] public int dropC = 50;

    
    // Start is called before the first frame update
    void Start()
    {
        curHP = maxHP; //현재체력을 정해준 최대체력으로 셋팅

        sr = GetComponent<SpriteRenderer>();
        if (sr != null) { oriC = sr.color; } //원래 색상 저장
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDmg(int dmg) //데미지를 받는다 (다른곳에서 호출)
    {
        curHP -= dmg;

        if(sr != null) //피격시 색상 변경 코루틴
        {
            StopAllCoroutines();
            StartCoroutine(HitEffect());
        }
        
        if(curHP <= 0) { Die();  } //현재체력이 0이 되면 다이 홏풀
    }

    IEnumerator HitEffect()
    {
        if (sr != null)
        {
            sr.color = Color.red; //빨강색
            yield return new WaitForSeconds(hitDur); //설정된 시간만큼 대기
            sr.color = oriC; //원래 색상으로
        }
    }
    void Die()
    {
        int chance = Random.Range(0, 100); //0~100 난수

        if(boxPre  != null && chance < dropC)
        {
            Instantiate(boxPre, transform.position, Quaternion.identity); //파괴되는 위치에 상자 생성

            //상자 생성시 리지드바디가 있으면 약간 위로 튀어나오는 연출가능(적이 내뱉는 느낌)
            Rigidbody2D boxRB = boxPre.GetComponent<Rigidbody2D>(); 

            //위로 박스에프 힘만큼 순간적인 충격을 줌 
            if(boxRB != null) {   boxRB.AddForce(Vector2.up * boxF, ForceMode2D.Impulse);  }

        }

        Destroy(gameObject); //적 파괴
    }
}
