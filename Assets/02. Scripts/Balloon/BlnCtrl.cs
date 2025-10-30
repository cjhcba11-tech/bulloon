using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlnCtrl : MonoBehaviour
{
    // 1. �Ϲ� ���� ��������Ʈ
    public Sprite dSprite;
    // 2. ���� ���� ��������Ʈ
    public Sprite cSprite;

    private SpriteRenderer sr;

    void Start()
    {
        // SpriteRenderer ������Ʈ ��������
        sr = GetComponent<SpriteRenderer>();
        // ���� �� �⺻ �̹��� ����
        sr.sprite = dSprite;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            // WŰ�� ������ ���� ���� �̹����� ��ü
            sr.sprite = cSprite;
            
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            // WŰ�� ���� �⺻ �̹����� ����
            sr.sprite = dSprite;
            
        }
    }
}