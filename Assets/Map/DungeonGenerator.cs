using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    [Header("���� ��ü ũ�⸦ ���ϴ� ���")]
    public float Max_x = 500f;
    public float Max_y = 500f;

    [Header("�ʵ��� ������ ����")]
    public GameObject[] maps = new GameObject[5];


    private void Start()
    {
        MapGenerator(Max_x, Max_y);
    }

    private void Update()
    {
        
    }

    void MapGenerator(float a, float b) 
    {

    }
}