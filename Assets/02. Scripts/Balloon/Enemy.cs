using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("�߻�")]
    [Tooltip("�� �Ѿ� ������")] public GameObject bPre;
    [Tooltip("�� �Ѿ� �ӵ�")] public float bSpeed = 8f; 
    [Tooltip("�� �Ѿ� �߻簣��")] public float fireRate = 0.5f;
    float nextFireTime;

    [Header("����")]
    [Tooltip("���� �߻��� �Ѿ˰���")] public int bCount = 8;
    [Tooltip("�� �Ѿ��� �߽ɼ����� �ִ� ��������")] public float maxAngle = 30f; 
   
    [Header("�̵�")]
    [Tooltip("�� �������� �̵��ӵ�")] public float moveSpeed = 1f;
    [Tooltip("�� �ڵ��ı��ð�")] public float lifeTime = 10f; 
    

    // Start is called before the first frame update
    void Start()
    {
        
        Destroy(gameObject, lifeTime);

        //ù�ߵ� �� �ʱ�ȭ (���� �� �ణ�� ������)
        nextFireTime = Time.time + fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        //�����̵� (���� * �ӵ� * ������ �̵�)
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

        //�߻簣��üũ
        if (Time.time > nextFireTime) //�ٵ� ��÷���� ������ �ȵǾ��ִµ� ����� ������ �Ǵ�����? = �ڵ� 0
        {
            ScatterShot();
            nextFireTime = Time.time + fireRate;
        }
    }
    
    void ScatterShot()
    {
        // ���� ���� �ٶ󺸴� ���� (����)transform.Left�� ���� -�� �ݴ� ���� ����ϰų� Vector2.left�� ��� 
        Vector2 baseDir = -transform.right;

        //�Ѿ� ������ŭ �ݺ�
        for (int i = 0; i < bCount; i++)
        {
            //���� ���� ��� (-�������� +��������)<���� �߻��ϰ� ����>
            float randomAngle = Random.Range(-maxAngle-22f, maxAngle-22f);

            //���� ���� ȸ�� (z���� �߽����� ȸ��) 180f�� ������ �ٶ󺸵��� 
            Quaternion rotation = Quaternion.Euler(0,0,randomAngle );
            
            //ȸ���ϴ� ���� �߻� ���� 
            Vector2 finalDir = rotation * baseDir;  

            //�Ѿ� ����
            GameObject bInstance = Instantiate(bPre, transform.position, Quaternion.identity);

            //�Ѿ� �ӵ�
            Rigidbody2D rb = bInstance.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                rb.velocity = finalDir.normalized * bSpeed;
            }
        }

    }
}
