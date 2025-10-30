using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EBullet : MonoBehaviour //��1 �Ҹ�
{
    // Start is called before the first frame update
    [Tooltip("�Ѿ� �ڵ��Ҹ�ð�")] public float eLifeTime = 3f;
    
    [Header("���� ����")]
    [Tooltip("���� ��������Ʈ")] public GameObject eExpFab;
  
    Rigidbody2D rb;


    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Destroy (gameObject, eLifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision coll)
    {
        Explode();
    }
    void Explode()
    {
        if (eExpFab != null)
        {
            //�Ѿ��� �ִ� ��ġ�� ���� ������ ����
            Instantiate(eExpFab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
