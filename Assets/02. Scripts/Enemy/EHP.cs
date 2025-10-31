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

        Destroy(gameObject); 
    }
}
