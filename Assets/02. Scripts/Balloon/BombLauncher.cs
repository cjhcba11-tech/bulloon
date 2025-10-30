using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI; // UI ��Ҹ� ����Ϸ��� �� ���ӽ����̽��� �ʿ��մϴ�.

public class BombLauncher : MonoBehaviour
{
    [Header("�߻� ����")]
    public GameObject bombPrefab; // �߻��� ��ź ������ 
    public Transform launchPoint; // ��ź�� ������ ��ġ (Optional: ������ �� ��ũ��Ʈ�� ���� ������Ʈ ��ġ ���)
    public float minLaunchForce = 5f; // �ּ� �߻� ��
    public float maxLaunchForce = 20f; // �ִ� �߻� ��
    public float chargeRate = 10f; // �ʴ� ���� �����ϴ� �ӵ�

    [Header("UI ����")]
    public Slider powerSlider; // �������� ǥ���� UI �����̴� (Inspector���� �Ҵ�)

    private float currentLaunchForce;
    private bool isCharging = false;

    void Start()
    {
        // ���� �� ���� ���� �ּ� ������ �����ϰ� �����̴��� ������Ʈ�մϴ�.
        currentLaunchForce = minLaunchForce;
        if (powerSlider != null)
        {
            powerSlider.minValue = minLaunchForce;
            powerSlider.maxValue = maxLaunchForce;
            powerSlider.value = currentLaunchForce;
        }

        // launchPoint�� �������� �ʾҴٸ� �� ������Ʈ�� ��ġ�� ����մϴ�.
        if (launchPoint == null)
        {
            launchPoint = this.transform;
        }
    }

    void Update()
    {
        // 1. ������ ���� ����: �����̽��ٸ� ������ ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isCharging = true;
            currentLaunchForce = minLaunchForce;
        }

        // 2. ������ ���� ��: �����̽��ٸ� ������ �ִ� ����
        if (isCharging && Input.GetKey(KeyCode.Space))
        {
            // ���� �ð��� ���� ������Ű�� �ִ�ġ�� ���� �ʵ��� �մϴ�.
            currentLaunchForce += chargeRate * Time.deltaTime;
            currentLaunchForce = Mathf.Min(currentLaunchForce, maxLaunchForce);

            // UI �����̴� ������Ʈ
            if (powerSlider != null)
            {
                powerSlider.value = currentLaunchForce;
            }
        }

        // 3. �߻� �� ������ �ʱ�ȭ: �����̽��ٸ� ���� ����
        if (isCharging && Input.GetKeyUp(KeyCode.Space))
        {
            LaunchBomb();
            isCharging = false;
            currentLaunchForce = minLaunchForce; // �߻� �� �� �ʱ�ȭ

            // UI �����̴� �ʱ�ȭ
            if (powerSlider != null)
            {
                powerSlider.value = currentLaunchForce;
            }
        }
    }

    void LaunchBomb()
    {
        // 1. ��ź ����
        GameObject bombInstance = Instantiate(bombPrefab, launchPoint.position, launchPoint.rotation);

        // 2. Rigidbody2D ������Ʈ ��������
        Rigidbody2D rb = bombInstance.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // 3. �߻� �� ����
            // ���⼭�� ���ⱸ�� ������(transform.right)���� ���� �߻��Ѵٰ� �����մϴ�.
            // ���� �ٸ� ������ �߻��ϰ� �ʹٸ� Vector2.right ��� �ٸ� ���� ���͸� ����ϼ���.
            Vector2 launchDirection = transform.right; // ��: ���ⱸ�� �ٶ󺸴� ����

            rb.AddForce(launchDirection * currentLaunchForce, ForceMode2D.Impulse);
            // Impulse(��ݷ�) ��带 ����Ͽ� �ﰢ���� ���� �����մϴ�.
        }
        else
        {
            Debug.LogError("��ź �����տ� Rigidbody2D ������Ʈ�� �����ϴ�!");
        }
    }
}