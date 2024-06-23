using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int x, y;
    public GameObject ballPrefab;
    private GameObject ball;
    public float speed;
    private bool isMoving;
    private Directions direction;
 
  
    public enum Directions
    {
        DownRight,       
        DownLeft,
        UpRight,
        UpLeft,
        None,
    }
    public void Initialize(Vector2[,] field)
    {
      direction = Directions.None;    
      ball = Instantiate(ballPrefab, field[y,x],Quaternion.identity);
   
    }
    private void ChangeDirection(int platformX, int platformY, GameObject[] platform, Vector2[,] field, Block block)
    {
         List<Vector2> blocksCordinates = block.GetPositions();
        List<GameObject[]> blocks = block.Getblocks();  
        for (int i = 0; i < blocks.Count; i++)
        {
            GameObject[] currentArr = blocks[i];
            Vector2 currentPosition = block.GetPositions()[i];
            for (int j = 0; j < currentArr.Length; j++)
            {
                if (currentPosition.x == x && currentPosition.y == y - 1 || currentPosition.x + 1 == x && currentPosition.y == y - 1)
                {
                    if (direction == Directions.UpLeft)
                    {
                        direction = Directions.DownLeft;
                    }
                    else if (direction == Directions.UpRight)
                    {
                        direction = Directions.DownRight;
                    }

                        block.DestroyAt(i);
                    return;
                }
            }
        }


        if (x == field.GetLength(1) - 1 && y == field.GetLength(0) - 2 && platformX+platform.Length-1 == field.GetLength(1) - 1) 
        {
            Debug.Log(true);
            direction = Directions.UpLeft;
            return; 
        }
        else if (x == 0 && y == field.GetLength(0) - 2 && platformX == 0)
        {
            direction = Directions.UpRight;
        }
        else if (x == field.GetLength(1) - 1 && y == 0)
        {
            direction = Directions.DownLeft;
        }
        else if (x == 0 && y == 0)
        {
            direction = Directions.DownRight;
        }
       
       
        else if (y == field.GetLength(0) - 1)
        {
            
            direction = Directions.None;
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
     public bool IsBallActive()
     {
        return direction != Directions.None;
     }
    public IEnumerator MoveBall(int platformX,int platformY,Vector2[,] field, GameObject[] platform, Block block)
    {
        


        if (direction == Directions.None)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                direction = Directions.UpLeft;
            }
            ball.transform.position = field[platformY - 1, platformX + platform.Length / 2];
            x = platformX + 1; y = platformY;
           
        }
        if (isMoving || direction == Directions.None) yield break;
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
            ChangeDirection(platformX, platformY, platform, field,block);
            isMoving = false;
        }
                
    }
   
    public int GetX() { return x; } 
    public int GetY() { return y; }
  
}
