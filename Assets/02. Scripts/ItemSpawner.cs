using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("������")]
    public GameObject itemprefab;
    public GameObject center;

    [Header("���� ����")]
    [SerializeField] float radius = 2f;
    [SerializeField] int startSpawncount = 5;
    [SerializeField] int maxCount = 20; // ���� ���� ������ �ִ밹�� ����
    [SerializeField] float spawnInterval; // �����ֱ�

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
        //return items.Length; // �������� ���̸� ���� 
    //}
}
