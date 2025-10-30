using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBullet : MonoBehaviour //Àû1 ºÒ¸´
{
    // Start is called before the first frame update

    //ÃÑ¾Ë ÀÚµ¿¼Ò¸ê
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
