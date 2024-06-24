using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Block : MonoBehaviour
{
    public List<GameObject[]> blocks;
    public List<Vector2> cords;
    public GameObject cellPrefab;
    public void Init()
    {
        GameObject[] temp = new GameObject[2];
        blocks = new List<GameObject[]>();
         cords = new List<Vector2>();
        //for (int i = 0; i < 2; i++)
        //{
        //    temp[i] = Instantiate(cellPrefab, field[y, x + i], Quaternion.identity);
        //}
        //cords.Add(new Vector2(x, y));
        //blocks.Add(temp);
    }
    public void CreateBlock(int x, int y,Vector2[,] field)
    {
        GameObject[] temp = new GameObject[2];


        for (int i = 0; i < 2; i++)
        {
            temp[i] = Instantiate(cellPrefab, field[y, x + i], Quaternion.identity);
        }
        cords.Add(new Vector2(x, y));
        blocks.Add(temp);
    }
    public List<GameObject[]> Getblocks() { return blocks; }
    public List<Vector2> GetPositions() { return cords; }

    public void DestroyAt(int index)
    {
        if (cords.Count > 0)
        {
            cords.RemoveAt(index);
        }
    
        for (int i = 0; i < 2; i++)
        {
            Destroy(blocks[index][i]);
        }
         blocks.RemoveAt(index);    
    }
}
