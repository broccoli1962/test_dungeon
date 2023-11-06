using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    [Header("맵의 전체 크기를 정하는 헤더")]
    public float Max_x = 500f;
    public float Max_y = 500f;

    [Header("맵들의 프리팹 저장")]
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