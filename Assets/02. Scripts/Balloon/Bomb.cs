using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Tooltip("��ź�� �ڵ����� ������� �ð�")] public float lifetime = 5f;

    [Header("���� ����")]
    [Tooltip("���� ��������Ʈ")] public GameObject expFab;

    Rigidbody2D rb;

    void Start()
    { 
        rb = GetComponent<Rigidbody2D>();
        // ���� �ð� �� ��ź�� �ı��մϴ�.
        Destroy(gameObject, lifetime);
    }

    // �ٸ� ������Ʈ�� �浹���� �� 
    
    // coll�� (�浹����,��,�����ϴ°��� ����)�� Ÿ�� other(���� �ݶ��̴� ��ü�� ���� ����)Ʈ���Ÿ� ������ other Ÿ������ ����Ѵ�.
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Explode();
        }
    }

    void Explode()
    {
        if (expFab != null)
        {
            //�Ѿ��� �ִ� ��ġ�� ���� ������ ����
            Instantiate(expFab, transform.position, Quaternion.identity);

            
        }

        Destroy(gameObject);
    }
}