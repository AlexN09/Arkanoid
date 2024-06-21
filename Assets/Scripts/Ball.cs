using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int x, y;
    public GameObject ballPrefab;
    private GameObject ball;
    public float speed;
    private bool isMoving,isAttached;
    private Directions direction;
    string currentDirection;
    private Field field;
    private int lastCollisionCordX, lastCollisionCordY;
    private void Start()
    {
        field = gameObject.GetComponent<Field>();
    }
    private enum Directions
    {
        DownRight,       
        DownLeft,
        UpRight,
        UpLeft,
       
    }
    public void Initialize(Vector2[,] field)
    {
      direction = Directions.DownRight;    
      ball = Instantiate(ballPrefab, field[y,x],Quaternion.identity);
   
    }
    private  void ChangeDirection(int platformX,int platformY,GameObject[] platform, Vector2[,] field)
    {
        if (x == 26 && y == 10) 
        {
            Debug.Log(true);
            direction = Directions.UpLeft;
            return; 
        }
        else if (x == 0 && y == 0)
        {
            direction = Directions.DownRight;
            return;
        }
      
      else  if (y == 0) 
        {
            if (direction == Directions.UpRight) 
            {
                direction = Directions.DownRight;
            }
            else if (direction == Directions.UpLeft)
            {
                direction = Directions.DownLeft;
            }
           
            return;
        }
        else if (x == field.GetLength(1) - 1)
        {
            if (direction == Directions.UpRight)
            {
                direction = Directions.UpLeft;
            }
            else if (direction == Directions.DownRight)
            {
                direction = Directions.DownLeft;
            }
            return;
        }
        else if (x == 0)
        {
            if (direction == Directions.UpLeft)
            {
                direction = Directions.UpRight;
            }
            else if (direction == Directions.DownLeft)
            {
                direction = Directions.DownRight;
            }
            return;
        }
       
            for (int i = 0; i < platform.Length; i++)
            {
                if (x == platformX + i && y == platformY - 1)
                {
                 if (x == field[field.GetLength(0) - 2, field.GetLength(1) - 1].x && y == field[field.GetLength(0) - 2, field.GetLength(1) - 1].y)
                 {
                    Debug.Log(true);
                    direction = Directions.UpLeft;
                 }
                  else if (direction == Directions.DownRight)
                   {
                     direction = Directions.UpRight;
                   }
                  else if (direction == Directions.DownLeft)
                  {
                    direction= Directions.UpLeft;
                  }
                   return;                    
                }
            }           
          
      
    }
    public IEnumerator MoveBall(int platformX,int platformY,Vector2[,] field, GameObject[] platform)
    {

        if (y == 0)
        {

        }
        if (isMoving) yield break;
        if (/*x > 0 && x < field.GetLength(1) && y > 0 && y < field.GetLength(0)*/ true)
        {
            isMoving = true;
            yield return new WaitForSeconds(speed);
            if (direction == Directions.DownRight)
            {
                y++;x++;
            }
            else if (direction == Directions.DownLeft)
            {
                y++; x--;
            }
            else if (direction == Directions.UpRight)
            {
                y--;x++;
            }
            else if (direction == Directions.UpLeft)
            {
                y--;x--;
            }
             ball.transform.position = field[y,x];
            ChangeDirection(platformX, platformY, platform,field);
            isMoving = false;
        }
       
    }
   
    public int GetX() { return x; } 
    public int GetY() { return y; }
  
}
