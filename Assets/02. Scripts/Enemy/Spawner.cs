using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    [SerializeField, Tooltip("��ȯ�� �� ������")] GameObject enemyPre;
    [SerializeField, Tooltip("�ּ� ���� ������(������)")] float minT = 1f;
    [SerializeField, Tooltip("�ִ� ���� ������(������)")] float maxT = 3f; 

    //[SerializeField] float spawnRate = 3f; //��ȯ����  //�������� �߰��ϸ� ������ ������
    //[SerializeField] float startDelay = 1f; //��ȯ ���� �����ð� 
    
    

    
    // Start is called before the first frame update
    void Start()
    {
        //�� ��ȯ �ڷ�ƾ ���� 
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    IEnumerator SpawnRoutine() //�ٵ� �����ǰ� �ٴڰ� �Բ� �̵��� �ؾߵ�, �ڷ�ƾ
    {
        

        while (true)
        {
            float waitT = Random.Range(minT, maxT); //���� �ð� ����

            yield return new WaitForSeconds(waitT); //�ð� ������ŭ ��ٸ� (�ڷ�ƾ�ȿ����� �۵�)

            //������ ��ġ�� ������ = �����Լ�(�� ������, ���� ��ġ, ȸ���� = 0)
            Instantiate(enemyPre, transform.position, Quaternion.identity);

            //yield return new WaitForSeconds(spawnRate); //������ȯ���� ���ð� 
        }
    }
}
