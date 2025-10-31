using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Tooltip("��ź�� �ڵ����� ������� �ð�")] public float lifetime = 5f;

    [Header("���� ����")]
    [Tooltip("���� ������")] public GameObject expFab;

    [Header("������ ����")]
    [Tooltip("���� �ݰ�")] public float rad = 3f; //���� ������?
    [Tooltip("������ �� ������")] public int dmg = 50;
    [Tooltip("���� ���̾�")] public LayerMask eLayer;


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
            //�Ѿ��� �ִ� ��ġ�� ���� ������ ���� //���� �����տ� �ڵ带 �־� �ð� ������ �ı�
            Instantiate(expFab, transform.position, Quaternion.identity);
        }

        // ���� ��ġ�� �߽����� rad�ݰ� ���� ���̾ ���� �ݶ��̴� ����
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, rad, eLayer);

        //
        foreach (Collider2D hit in cols)
        {
            if(hit.CompareTag("Enemy")) //��ü�� ��ũ��Ʈ �ְ�, ������ ó�� �ڵ� �ۼ�
            {
                //���� Ž���ϸ� ������ �ʷϻ� ���� �׸��ϴ�.(��ź������ 10���� �����ص� �۵��� �ȵǴ� ����)
                Debug.DrawRay(transform.position, hit.transform.position - transform.position, Color.green);

                EHP h = hit.GetComponent<EHP>();
                if (h != null) {   h.TakeDmg(dmg);  }
            }
        }

        Destroy(gameObject); //��ź ������Ʈ �ı� 
    }

    //������� ���� ���� �ݰ� �ð�ȭ //����� ����߿��� �Ⱥ���... 
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rad);
    }
}