using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
#if UNITY_EDITOR
using System.Reflection;
#endif
public class Ball : MonoBehaviour
{
    public int x, y;
    public GameObject ballPrefab;
    private GameObject ball;
    public float speed;
    private bool isMoving;
    private Directions direction;
    private Directions startDirection;
    private bool isAttached;
    public int testDirection;
    
    
    public enum Directions
    {
        DownRight,       
        DownLeft,
        UpRight,
        UpLeft,
        None,
       
    }
    private void ClearUnityConsole()
    {
        var assembly = Assembly.GetAssembly(typeof(SceneView));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(null, null);
    }


    private void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.S))
        {
            testDirection = testDirection == 1 ? testDirection = 2 : testDirection = 1;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            direction = Directions.None;
        }
        ClearUnityConsole();    
        Debug.Log("Current Direction: " + direction);
        Debug.Log("Current start Direction: " + (testDirection == 1 ? "UpLeft" : "UpRight"));

        if (testDirection == 1)
        {
           startDirection = Directions.UpLeft;
        }
        else if (testDirection == 2)
        {
            startDirection = Directions.UpRight;
        }
    }
    public void Initialize(Vector2[,] field)
    {
      direction = Directions.None;    
      ball = Instantiate(ballPrefab, field[y,x],Quaternion.identity);
       isAttached = true;
    }
    private void ChangeDirection(int platformX, int platformY, GameObject[] platform, Vector2[,] field, Block block)
    {
        List<Vector2> blocksCordinates = block.GetPositions();
        List<GameObject[]> blocks = block.Getblocks();
        bool foundMatch = false;

        for (int i = 0; i < blocksCordinates.Count; i++)
        {
            Vector2 currentPos = blocksCordinates[i];
            for (int j = 0; j < 4; j++)
            {
                Vector2 checkIfAngle = new Vector2(currentPos.x + 3, currentPos.y);
                if (x == currentPos.x + j && y == currentPos.y + 1)
                {
                    block.DestroyAt(i);
                    direction = direction == Directions.UpRight ? Directions.DownRight : Directions.DownLeft; break;
                }
          
                else if (!blocksCordinates.Contains(checkIfAngle) && currentPos.x + 3 == x && y == currentPos.y + 1)
                {
                    block.DestroyAt(i);
                    direction = direction == Directions.UpLeft ? Directions.DownRight : direction; break;
                }
                

            }
        }

      

        
       
         if (x == 0 && y == field.GetLength(0) - 2 && platformX == 0)
        {
            direction = Directions.UpRight;
            return;
        }
        else if (x == field.GetLength(1) - 1 && y == 0)
        {
            direction = Directions.DownLeft;
            return;
        }
        else if (x == 0 && y == 0)
        {
            direction = Directions.DownRight;
            return;
        }
        else if (y == field.GetLength(0) - 1)
        {
            direction = Directions.None;
            isAttached = true;
            return;
        }
        else if (y == 0)
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

        // Проверка на столкновение с краем платформы
        for (int i = 0; i < platform.Length; i++)
        {
            if (x == platformX + i && y == platformY - 1)
            {
                //if (x == field[field.GetLength(0) - 2, field.GetLength(1) - 1].x && y == field[field.GetLength(0) - 2, field.GetLength(1) - 1].y)
                //{
                //    Debug.Log(true);
                //    direction = Directions.UpLeft;
                //}
                if (direction == Directions.DownRight)
                {
                    direction = Directions.UpRight;
                }
                else if (direction == Directions.DownLeft)
                {
                    direction = Directions.UpLeft;
                }
                return;
            }
        }

        // Дополнительная проверка на столкновение с краем платформы
        if (x == platformX - 1 && y == platformY - 1)
        {
            direction = direction == Directions.DownRight ? Directions.UpLeft : direction = direction;
            //direction = Directions.UpRight;
            return;
        }
        else if (x == platformX + platform.Length - 1 && y == platformY - 1)
        {
            direction = Directions.UpLeft;
            return;
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
                direction = startDirection;
                isAttached = false;
                y--; x--;
                ball.transform.position = field[y, x];
            }
            else 
            {
                ball.transform.position = field[platformY - 1, platformX + platform.Length / 2];
                x = platformX /*+ 1*/; y = platformY;
            }
      
        }
        if (isMoving || direction == Directions.None || isAttached) yield break;
        if (/*x > 0 && x < field.GetLength(1) && y > 0 && y < field.GetLength(0)*/ true)
        {
          
                ChangeDirection(platformX, platformY, platform, field, block);
            
            
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
            //ChangeDirection(platformX, platformY, platform, field,block);
            isMoving = false;
        }
         
    }
   
    public int GetX() { return x; } 
    public int GetY() { return y; }
  
}
