using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonShooter : MonoBehaviour
{
    [Header("������")]
    public GameObject bombPrefab;         // �߻��� ��ź ������ (�����տ� Rigidbody �ʿ�)
    public Transform firePoint;           // �߻� ��ġ/����(��: ǳ���� ��� ����ִ� Transform)

    [Header("�߻�")]
    public float minForce = 5f;           // �ּ� �߻� �ӵ�
    public float maxForce = 30f;          // �ִ� �߻� �ӵ�
    public float maxChargeTime = 2f;      // �� �ð� ���� ������ ���� maxForce
    public bool allowAutoFireOnMax = false; // true�� �ִ�ġ ���޽� �ڵ� �߻�

    [Header("���߷� �ð�")]
    public float straightTime = 0.5f;     // �߻� �� �󸶳� ����(���߷�)���� ������(��). BombProjectile������ ���ϰ� ���� ����.

    private float chargeTimer = 0f;
    private bool charging = false;

    void Update()
    {
        // �����̽��� ������ �ִ� ���� ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            charging = true;
            chargeTimer = 0f;
        }

        if (charging && Input.GetKey(KeyCode.Space))
        {
            chargeTimer += Time.deltaTime;
            if (chargeTimer >= maxChargeTime)
            {
                chargeTimer = maxChargeTime;
                if (allowAutoFireOnMax)
                {
                    Fire();
                    charging = false;
                }
            }
            // (����) ���⼭ UI ������ ������Ʈ ����: chargeTimer / maxChargeTime
        }

        // ���� ���� �߻�
        if (charging && Input.GetKeyUp(KeyCode.Space))
        {
            Fire();
            charging = false;
        }

        // (����) �����: ���� ���� �ۼ�Ʈ
        // Debug.Log($"Charge: {chargeTimer/maxChargeTime * 100f:F0}%");
    }

    void Fire()
    {
        if (bombPrefab == null || firePoint == null) return;

        float t = chargeTimer / maxChargeTime;
        float force = Mathf.Lerp(minForce, maxForce, t);

        GameObject go = Instantiate(bombPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = go.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = firePoint.forward * force; // �ʱ� ���� �ӵ� ����
        }
        /*
        // BombProjectile�� straightTime �� ���� (�ִٸ�)
        BombProjectile bp = go.GetComponent<BombProjectile>();
        if (bp != null)
        {
            bp.ActivateStraightFlight(straightTime);
        }
        */
        // ����
        chargeTimer = 0f;
    }
}
