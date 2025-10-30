using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("발사")]
    public GameObject bPre; //총알 프리팹
    public float bSpeed = 8f; //총알 속도
    public float fireRate = 0.5f; //발사 간격
    float nextFireTime;

    [Header("난사")]
    public int bCount = 8; //한번에 발사할 총알 갯수
    public float maxAngle = 30f; //중심선에서 최대 퍼짐 각도 

    [Header("이동")]
    public float moveSpeed = 1f; //적 왼쪽으로 이동속도
    public float lifeTime = 10f; //일정 시간 후 자동파괴 (화면 밖으로 나갈때 파괴)

    // Start is called before the first frame update
    void Start()
    {
        
        Destroy(gameObject, lifeTime);

        //첫발도 값 초기화 (시작 후 약간의 딜레이)
        nextFireTime = Time.time + fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        //좌측이동 (왼쪽 * 속도 * 일정한 이동)
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

        //발사간격체크
        if (Time.time > nextFireTime) //근데 맨첨값이 설정이 안되어있는데 제대로 가동이 되는이유?
        {
            ScatterShot();
            nextFireTime = Time.time + fireRate;
        }
    }
    
    void ScatterShot()
    {
        // 현재 적이 바라보는 방향 (왼쪽)transform.Left는 없음 -로 반대 시켜 사용하거나 Vector2.left로 사용 
        Vector2 baseDir = -transform.right;

        //총알 갯수만큼 반복
        for (int i = 0; i < bCount; i++)
        {
            //랜덤 각도 계산 (-변수부터 +변수까지)<위로 발사하게 조정>
            float randomAngle = Random.Range(-maxAngle-22f, maxAngle-22f);

            //방향 벡터 회전 (z축을 중심으로 회전) 180f는 왼쪽을 바라보도록 
            Quaternion rotation = Quaternion.Euler(0,0,randomAngle );
            
            //회전하는 최종 발사 방향 
            Vector2 finalDir = rotation * baseDir;  

            //총알 생성
            GameObject bInstance = Instantiate(bPre, transform.position, Quaternion.identity);

            //총알 속도
            Rigidbody2D rb = bInstance.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                rb.velocity = finalDir.normalized * bSpeed;
            }
        }

    }
}
