using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBullet : MonoBehaviour //��1 �Ҹ�
{
    // Start is called before the first frame update

    //�Ѿ� �ڵ��Ҹ�
    public float lifeTime = 3f;

    
    void Start()
    {
        Destroy (gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
