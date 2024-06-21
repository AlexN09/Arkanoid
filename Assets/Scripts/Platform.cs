using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEditor;
using UnityEditor.AdaptivePerformance.Editor;
using UnityEditor.Build;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private int x, y;
    public int size;
    private GameObject[] platform;
    public GameObject platformPart;
    public float speed;
    private bool isMoving = false;
    private Field field;
    private void Start()
    {
        field = gameObject.GetComponent<Field>();
    }
    public void Initialize(Vector2[,] field, int _x, int _y)
    {
  
        x = _x - 1; y = _y - 1;
        platform = new GameObject[size];
        for (int i = 0; i < size; i++)
        {
           platform[i] = Instantiate(platformPart, field[y, x + i], quaternion.identity);

        }
    }
    private void Update()
    {
        if (field.isInitialized)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                StartCoroutine(MoveRight(field.GetField()));
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                StartCoroutine(MoveLeft(field.GetField()));
            }
        }
       
    }
    public IEnumerator MoveRight(Vector2[,] field)
    {
        if (isMoving) yield break; 

        if (field.GetLength(1) - size > x)
        {
            isMoving = true; 
            yield return new WaitForSeconds(speed);
            //Erase();
            x++;
            //for (int i = 0; i < size; i++)
            //{
            //    platform[i] = Instantiate(platformPart, field[y, x + i], Quaternion.identity);
            //}

            for (int i = 0;i < size;i++)
            {
                platform[i].transform.position = field[y,x + i];
            }
            isMoving = false; 
        }
    }

    public IEnumerator MoveLeft(Vector2[,] field)
    {
        if (isMoving) yield break;

        if (x > 0)
        {
            isMoving = true;
            yield return new WaitForSeconds(speed);
            //Erase();
            x--;
            //for (int i = 0; i < size; i++)
            //{
            //    platform[i] = Instantiate(platformPart, field[y, x + i], Quaternion.identity);
            //}
            for (int i = 0; i < size; i++)
            {
                platform[i].transform.position = field[y, x + i];
            }
            isMoving = false;
        }
    }
    public int GetX() { return x; }
    public int GetY() { return y; }
    public GameObject[] GetPlatform() { return platform; }
    private void Erase()
    {
        for (int i = 0; i < size; i++)
        {
            Destroy(platform[i]);
        }
    }
  
}
