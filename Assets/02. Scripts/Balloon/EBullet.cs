using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBullet : MonoBehaviour //��1 �Ҹ�
{
    // Start is called before the first frame update
    [Tooltip("�Ѿ� �ڵ��Ҹ�ð�")] public float lifeTime = 3f;
    
    [Header("���� ����")]
    [Tooltip("���� �ִϸ��̼� ��� �ð� 30��")] public float expDur = 0.5f;

    Animator anim;
    bool isExp = false; //�����ߺ�����


    void Start()
    {
        anim = GetComponent<Animator>();
        Destroy (gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision coll)
    {
        if (isExp)
        {
            //Explode();
        }
    }
}
