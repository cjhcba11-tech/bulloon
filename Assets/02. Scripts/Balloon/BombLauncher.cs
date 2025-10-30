using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI; // UI 요소를 사용하려면 이 네임스페이스가 필요합니다.

public class BombLauncher : MonoBehaviour
{
    [Header("발사 설정")]
    [Tooltip("발사할 폭탄 프리팹")] public GameObject bombPrefab; 
    [Tooltip("폭탄 생성 위치")] public Transform launchPoint; // 없으면 이 스크립트가 붙은 오브젝트 위치 사용
    [Tooltip("최소 발사 힘")] public float minLaunchForce = 5f; 
    [Tooltip("최대 발사 힘")] public float maxLaunchForce = 20f;
    [Tooltip("초당 힘 증가속도")] public float chargeRate = 10f;

    [Header("UI 설정")]
    [Tooltip("발사게이지 UI슬라이더")] public Slider powerSlider; 

    private float currentLaunchForce;
    private bool isCharging = false;

    void Start()
    {
        // 시작 시 현재 힘을 최소 힘으로 설정하고 슬라이더를 업데이트합니다.
        currentLaunchForce = minLaunchForce;
        if (powerSlider != null)
        {
            powerSlider.minValue = minLaunchForce;
            powerSlider.maxValue = maxLaunchForce;
            powerSlider.value = currentLaunchForce;
        }

        // launchPoint가 설정되지 않았다면 이 오브젝트의 위치를 사용합니다.
        if (launchPoint == null)
        {
            launchPoint = this.transform;
        }
    }

    void Update()
    {
        // 1. 게이지 충전 시작: 스페이스바를 누르는 순간
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isCharging = true;
            currentLaunchForce = minLaunchForce;
        }

        // 2. 게이지 충전 중: 스페이스바를 누르고 있는 동안
        if (isCharging && Input.GetKey(KeyCode.Space))
        {
            // 힘을 시간에 따라 증가시키고 최대치를 넘지 않도록 합니다.
            currentLaunchForce += chargeRate * Time.deltaTime;
            currentLaunchForce = Mathf.Min(currentLaunchForce, maxLaunchForce);

            // UI 슬라이더 업데이트
            if (powerSlider != null)
            {
                powerSlider.value = currentLaunchForce;
            }
        }

        // 3. 발사 및 게이지 초기화: 스페이스바를 떼는 순간
        if (isCharging && Input.GetKeyUp(KeyCode.Space))
        {
            LaunchBomb();
            isCharging = false;
            currentLaunchForce = minLaunchForce; // 발사 후 힘 초기화

            // UI 슬라이더 초기화
            if (powerSlider != null)
            {
                powerSlider.value = currentLaunchForce;
            }
        }
    }

    void LaunchBomb()
    {
        // 1. 폭탄 생성
        GameObject bombInstance = Instantiate(bombPrefab, launchPoint.position, launchPoint.rotation);

        // 2. Rigidbody2D 컴포넌트 가져오기
        Rigidbody2D rb = bombInstance.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // 3. 발사 힘 적용
            // 여기서는 열기구의 오른쪽(transform.right)으로 수평 발사한다고 가정합니다.
            // 만약 다른 각도로 발사하고 싶다면 Vector2.right 대신 다른 방향 벡터를 사용하세요.
            Vector2 launchDirection = transform.right; // 예: 열기구가 바라보는 방향

            rb.AddForce(launchDirection * currentLaunchForce, ForceMode2D.Impulse);
            // Impulse(충격량) 모드를 사용하여 즉각적인 힘을 적용합니다.
        }
        else
        {
            Debug.LogError("폭탄 프리팹에 Rigidbody2D 컴포넌트가 없습니다!");
        }
    }
}