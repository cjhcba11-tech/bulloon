using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBullet : MonoBehaviour //적1 불릿
{
    // Start is called before the first frame update
    [Tooltip("총알 자동소멸시간")] public float lifeTime = 3f;
    
    [Header("폭발 설정")]
    [Tooltip("폭발 애니메이션 재생 시간 30초")] public float expDur = 0.5f;

    Animator anim;
    bool isExp = false; //폭발중복방지


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
