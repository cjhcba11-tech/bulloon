using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI��ҿ� �ʿ�
using UnityEngine.SceneManagement; //����ȯ�� �ʿ���

public class PHP : MonoBehaviour
{
    [Header("ü�� ����")]
    [Tooltip("�ִ� ü��")] public int maxHP = 100;
    [Tooltip("���� ü��")] public int curHP;

    [Header("���� ����")]
    [Tooltip("���ӿ��� �� �̸�")] public string gameOverS = "GameOver";
    [Tooltip("��źȯ �±� 1������")] public string eBullTag = "EBullet";
    [Tooltip("��źȯ �±� 5������")] public string eBullTag5 = "EBullet5";

    [Header("UI ����")]
    [Tooltip("ü�°����� �Ǹ���")] public Slider hpSlider;

    // Start is called before the first frame update
    void Start()
    {
        curHP = maxHP;

        if(hpSlider != null)
        {
            hpSlider.maxValue = maxHP;
            hpSlider.value = curHP;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) //�� �Ѿ��� ���ⱸ�� ������� (�浹����)
    {
        if(other.CompareTag(eBullTag)) //�浹�� ������Ʈ�� �±װ� EnemyBullet���� Ȯ�� 
        {
            Destroy(other.gameObject); //�浹�� �Ѿ�����
            TakeD(1); //������ ����
        }
        else if (other.CompareTag(eBullTag5))
        {
            Destroy(other.gameObject); //�浹�� �Ѿ�����
            TakeD(5); //������ ����
        }
    }

    public void TakeD(int dmg)
    {
        curHP -= dmg;
        UpdateHPBar(); 

        if(curHP <= 0 )
        {
            EndGame(); 
        }

        void EndGame()
        {
            SceneManager.LoadScene(gameOverS); //���ӿ��� ������ ��ȯ
        }
    }

    void UpdateHPBar() //�÷��̾� ü�¹� �Լ� ȣ�� (�̰� �־�� ȭ�鿡 ����Ǿ� ���δ�)
    {
        if (hpSlider != null)
        {
            hpSlider.value = curHP;
        }
    }
}
