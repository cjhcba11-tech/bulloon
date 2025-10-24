using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float lifetime = 5f; // ��ź�� �ڵ����� ����� �ð�

    void Start()
    {
        // ���� �ð� �� ��ź�� �ı��մϴ�.
        Destroy(gameObject, lifetime);
    }

    // �ٸ� ������Ʈ�� �浹���� �� (�ɼ�)
    void OnCollisionEnter2D(Collision2D collision)
    {
        // ��: ����("Ground" �±׸� ���� ������Ʈ)�� ������ ���� ���� ó���� �� �� �ֽ��ϴ�.
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Debug.Log("��ź�� ���鿡 �浹�߽��ϴ�!");
            // ���⿡ ���� ȿ��, ���� ���� �ڵ带 �߰��մϴ�.
            Destroy(gameObject);
        }
    }
}