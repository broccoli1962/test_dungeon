using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    [Header("맵의 전체 크기를 정하는 헤더")] //4*4
    public int Max_x = 4;
    public int Max_y = 4;
    public float length = 10f;

    [Header("맵들의 프리팹 저장")]
    public GameObject[] maps = new GameObject[5];


    private void Start()
    {
        MapGenerator(Max_x, Max_y);
    }

    void MapGenerator(int a, int b)
    {
        for(int i = 0; i< a; i++)
        {
            for(int j = 0; j < b; j++)
            {
                int rand_num = Random.Range(0, maps.Length);
                Instantiate(maps[rand_num], new Vector3(i*length, 16.5f, j*length), Quaternion.identity);
            }
        }
    }
}