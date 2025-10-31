using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EHP : MonoBehaviour
{
    [Tooltip("�ִ� ü��")] public int maxHP = 100;
    [Tooltip("���� ü��")] public int curHP;

    [Tooltip("�ǰݽ� ������ �ð�")] public float hitDur = 1.0f;

    SpriteRenderer sr; //��������Ʈ ������ ������Ʈ ����
    Color oriC; //���� ���� ����� (�������� Į��)                       
    
    // Start is called before the first frame update
    void Start()
    {
        curHP = maxHP; //����ü���� ������ �ִ�ü������ ����

        sr = GetComponent<SpriteRenderer>();
        if (sr != null) { oriC = sr.color; } //���� ���� ����
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDmg(int dmg) //�������� �޴´� (�ٸ������� ȣ��)
    {
        curHP -= dmg;

        if(sr != null) //�ǰݽ� ���� ���� �ڷ�ƾ
        {
            StopAllCoroutines();
            StartCoroutine(HitEffect());
        }
        
        if(curHP <= 0) { Die();  } //����ü���� 0�� �Ǹ� ���� �MǮ
    }

    IEnumerator HitEffect()
    {
        if (sr != null)
        {
            sr.color = Color.red; //������
            yield return new WaitForSeconds(hitDur); //������ �ð���ŭ ���
            sr.color = oriC; //���� ��������
        }
    }
    void Die()
    {

        Destroy(gameObject); 
    }
}
