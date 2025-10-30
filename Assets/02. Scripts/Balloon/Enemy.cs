using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("�߻�")]
    public GameObject bPre; //�Ѿ� ������
    public float bSpeed = 8f; //�Ѿ� �ӵ�
    public float fireRate = 0.5f; //�߻� ����
    float nextFireTime;

    [Header("����")]
    public int bCount = 8; //�ѹ��� �߻��� �Ѿ� ����
    public float maxAngle = 30f; //�߽ɼ����� �ִ� ���� ���� 

    [Header("�̵�")]
    public float moveSpeed = 1f; //�� �������� �̵��ӵ�
    public float lifeTime = 10f; //���� �ð� �� �ڵ��ı� (ȭ�� ������ ������ �ı�)

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
        if (Time.time > nextFireTime) //�ٵ� ��÷���� ������ �ȵǾ��ִµ� ����� ������ �Ǵ�����?
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
