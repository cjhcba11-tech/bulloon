using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BalloonMove : MonoBehaviour
{
    [Header("��� ����")]
    public float ascentForce = 2f; //�ö󰡴� ��
    public float maxAscent = 5f; //������ ������ �ִ�ӵ�

    [Header("�ϰ� ����")]
    public float weakGravity = 0.1f; //wŰ�� �ȴ����� �޴� �߷��� ��
    public float normalGravity = 0.5f; //�ִ� �߷�

    [Header("���� �Ѱ� ����")] // ���� �Ѱ踦 �����ϴ� ������
    public float topLimitY = 10f; // �ִ� ��� ���� (Y ��ǥ)
    public float bottomLimitY = -5f; // �ּ� �ϰ� ���� (Y ��ǥ)

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("BalloonMove ��ũ��Ʈ�� Rigidbody2D ������Ʈ�� �ʿ�� �մϴ�!");
            enabled = false; //�� ��ũ��Ʈ�� ������
        }
        else
        {
            // ���� �� ���� �߷����� �����Ͽ� õõ�� �ϰ�
            rb.gravityScale = weakGravity;
        }
    }

    void Update()
    {
        // 1. ���� ���� Ȯ�� �� ���� (CheckHeightLimits �Լ��� ȣ��)
        CheckHeightLimits();

        // 2. ��� �Է� ����
        if (Input.GetKey(KeyCode.W))
        {
            // W Ű�� ������ ������ Ascend �Լ��� ȣ���մϴ�.
            Ascend();
        }
        else
        {
            // W Ű�� ������ �� ApplyWeakGravity �Լ��� ȣ���մϴ�.
            ApplyWeakGravity();
        }
    }

    void CheckHeightLimits() // ���� ������ Ȯ���ϰ� ������ �����ϴ� ���� 
    {
        Vector3 currentPosition = transform.position;

        // --- ��� �Ѱ� (Top Limit) ---
        // ���� Y ��ġ(currentPosition.y)�� �ִ� ����(topLimitY)���� ������ ����
        if (currentPosition.y > topLimitY)
        {
            // Y ��ġ�� �ִ� �Ѱ�� ���� (transform.position)
            currentPosition.y = topLimitY;
            transform.position = currentPosition;

            // Y�� �ӵ��� 0���� �����Ͽ� ���� (rb.velocity)
            if (rb != null)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
            }
        }

        // --- �ϰ� �Ѱ� (Bottom Limit) ---
        // ���� Y ��ġ�� �ּ� ����(bottomLimitY)���� ������ ����
        if (currentPosition.y < bottomLimitY)
        {
            // Y ��ġ�� �ּ� �Ѱ�� ���� (transform.position)
            currentPosition.y = bottomLimitY;
            transform.position = currentPosition;

            // Y�� �ӵ��� 0���� �����Ͽ� ���� (rb.velocity)
            if (rb != null)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
            }
        }
    }

    void Ascend() // W Ű�� ������ �� ��½�Ű�� ���� 
    {
        // ��� �߿��� �Ϲ� �߷� �������� ���� (rb.gravityScale)
        rb.gravityScale = normalGravity;

        // ���� ���� �ӵ�(rb.velocity.y)�� �ִ� ��� �ӵ�(maxAscentSpeed)���� ���� ���� ����
        if (rb.velocity.y < maxAscent)
        {
            // AddForce�� ����Ͽ� �������� ��� ���� �����մϴ�. 
            // (Vector2.up * ascentForce : ���� ����� ũ��)
            // (ForceMode2D.Force : ���� �����ϴ� ���)
            rb.AddForce(Vector2.up * ascentForce, ForceMode2D.Force);
        }
    }

    void ApplyWeakGravity() // W Ű�� ������ �� ���� �߷��� �����ϴ� ���� 
    {
        // Rigidbody�� �߷� �������� ���ϰ� �����Ͽ� õõ�� �������� �մϴ�.
        rb.gravityScale = weakGravity;
    }
}