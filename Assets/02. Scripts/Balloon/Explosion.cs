using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [Tooltip ("���� �����ð�") ]public float Time = 3f;

    // Start is called before the first frame update
    void Start()
    {
        //���� �ִϸ��̼� 3�� �� ����
        Destroy(gameObject, Time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
