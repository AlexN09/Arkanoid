using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Field : MonoBehaviour
{
    private Vector2[,] fieldCords;
    public GameObject test;
    public int Width, Height;
    public float cellInterval;
    public Vector2 pos;
    private void Start()
    {

    }
    public void Initialize()
    {
        fieldCords = new Vector2[Height, Width];
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                fieldCords[i, j] = new Vector2(pos.x + (cellInterval * j), pos.y - (cellInterval * i));
                Instantiate(test, fieldCords[i, j], Quaternion.identity);
            }
        }
    }
   public Vector2[,] GetField() { return fieldCords; }
   
   public int GetWidth() {  return Width; }
 
   public int GetHeight() { return Height; }
  
}
