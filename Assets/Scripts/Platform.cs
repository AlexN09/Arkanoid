using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private int x,y;
    private int size;
    private GameObject[] platform;
    public GameObject platformPart;

    public void Initialize(Vector2[,] field,int _x, int _y, int _size)
    {
        x = _x; y = _y; size = _size;   
        for (int i = 0; i < _size; i++)
        {
            Instantiate(platformPart, field[_y,i], quaternion.identity);
        }
    }
   
    
}
