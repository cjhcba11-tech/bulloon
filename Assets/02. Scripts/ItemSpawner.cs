using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("프리팹")]
    public GameObject itemprefab;
    public GameObject center;

    [Header("스폰 설정")]
    [SerializeField] float radius = 2f;
    [SerializeField] int startSpawncount = 5;
    [SerializeField] int maxCount = 20; // 동시 존재 가능한 최대갯수 제한
    [SerializeField] float spawnInterval; // 생성주기

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        if (center == null)
        {
           // center = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ItemSpawn()
    {

    }
    //int CountItem()
    //{
        //var items = FindObjectByType<Item>();
        //return items.Length; // 아이템의 길이를 리턴 
    //}
}
