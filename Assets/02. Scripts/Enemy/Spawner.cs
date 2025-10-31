using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    [SerializeField, Tooltip("소환할 적 프리팹")] GameObject enemyPre;
    [SerializeField, Tooltip("최소 스폰 딜레이(랜덤값)")] float minT = 1f;
    [SerializeField, Tooltip("최대 스폰 딜레이(랜덤값)")] float maxT = 3f; 

    //[SerializeField] float spawnRate = 3f; //소환간격  //랜덤값을 추가하면 좋을거 같은데
    //[SerializeField] float startDelay = 1f; //소환 시작 지연시간 
    
    

    
    // Start is called before the first frame update
    void Start()
    {
        //적 소환 코루틴 시작 
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    IEnumerator SpawnRoutine() //근데 생성되고 바닥과 함께 이동을 해야됨, 코루틴
    {
        

        while (true)
        {
            float waitT = Random.Range(minT, maxT); //랜덤 시간 생성

            yield return new WaitForSeconds(waitT); //시간 지연만큼 기다림 (코루틴안에서만 작동)

            //스포너 위치에 적생성 = 생성함수(적 프리팹, 생성 위치, 회전값 = 0)
            Instantiate(enemyPre, transform.position, Quaternion.identity);

            //yield return new WaitForSeconds(spawnRate); //다음소환까지 대기시간 
        }
    }
}
