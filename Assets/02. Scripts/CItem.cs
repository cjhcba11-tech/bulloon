using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CItem : MonoBehaviour
{

    [SerializeField] bool isDestory = false;


    void Collect()
    {
        if(isDestory)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
 