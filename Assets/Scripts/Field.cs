using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using UnityEngine;

public class Field : MonoBehaviour
{
    
    private Vector2[,] fieldCords;
    public GameObject test, container;
    public int Width, Height;
    public float cellInterval;
    public Vector2 pos;
    public bool isInitialized;
    private void Start()
    {

    }
    public void Initialize()
    {
        isInitialized = false;
        fieldCords = new Vector2[Height, Width];
        int iterator = 0;
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                fieldCords[i, j] = new Vector2(pos.x + (cellInterval * j), pos.y - (cellInterval * i));
                GameObject test1 = Instantiate(test, fieldCords[i, j], Quaternion.identity, container.transform); // Указание родительского объекта
                test1.name = iterator.ToString(); // Назначение имени объекту
                iterator++;
            }
        }
        isInitialized = true;
    }
    public Vector2[,] GetField() { return fieldCords; }
   
   public int GetWidth() {  return Width; }
 
   public int GetHeight() { return Height; }
  
}
