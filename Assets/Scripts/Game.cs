using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Game : MonoBehaviour
{
    private delegate void MyDelegate();
    private GameObject Object;
    private Field field;
    private Platform platform;
    private Ball ball;
    void Start()
    {
      Object = gameObject;
      field = Object.GetComponent<Field>();
      platform = Object.GetComponent<Platform>();
      ball = Object.GetComponent<Ball>();
      field.Initialize();
      ball.Initialize(field.GetField());
      platform.Initialize(field.GetField(), field.GetWidth() / 2,field.GetHeight());
     
    }
    
    void Update()
    {

        //if (ball.CheckCollision(platform.GetX(), platform.GetY(), platform.GetPlatform(), field.GetField()) || Input.GetKeyDown(KeyCode.W)) 
        //{
        //    ball.ChangeDirection(platform.GetPlatform());
        //}
        StartCoroutine(ball.MoveBall(platform.GetX(),platform.GetY(),field.GetField(), platform.GetPlatform())) ;
    }
    
}
