using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EBullet : MonoBehaviour //Àû1 ºÒ¸´
{
    // Start is called before the first frame update
    [Tooltip("ÃÑ¾Ë ÀÚµ¿¼Ò¸ê½Ã°£")] public float eLifeTime = 3f;
    
    [Header("Æø¹ß ¼³Á¤")]
    [Tooltip("Æø¹ß ½ºÇÁ¶óÀÌÆ®")] public GameObject eExpFab;
  
    Rigidbody2D rb;


    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Destroy (gameObject, eLifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision coll)
    {
        Explode();
    }
    void Explode()
    {
        if (eExpFab != null)
        {
            //ÃÑ¾ËÀÌ ÀÖ´ø À§Ä¡¿¡ Æø¹ß ÇÁ¸®ÆÕ »ý¼º
            Instantiate(eExpFab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
