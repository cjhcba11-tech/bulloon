using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [Tooltip ("폭발 유지시간") ]public float Time = 3f;

    // Start is called before the first frame update
    void Start()
    {
        //폭발 애니메이션 3초 후 삭제
        Destroy(gameObject, Time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
