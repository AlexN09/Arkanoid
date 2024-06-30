using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject Object;
    private Field field;
    private Platform platform;
    private Ball ball;
    private Block block;
    private bool canDestroy;

    void Start()
    {
        canDestroy = false;
        Object = gameObject;
        field = Object.GetComponent<Field>();
        platform = Object.GetComponent<Platform>();
        ball = Object.GetComponent<Ball>();
        block = Object.GetComponent<Block>();   
        field.Initialize();
        ball.Initialize(field.GetField());
        platform.Initialize(field.GetField(), field.GetWidth() / 2, field.GetHeight());
        block.Init();
        //block.Init(20,2,field.GetField());

        //for (int j = 0; j < 6; j++)
        //{
        //    for (int i = 0; i < 25; i += 3)
        //    {
        //        block.CreateBlock(i, j, field.GetField());
        //    }
        //}

       


    }

    void Update()
    {
        //StartCoroutine(ball.MoveBall(platform.GetX(), platform.GetY(), field.GetField(), platform.GetPlatform(),block));

        StartCoroutine(ball.MoveBall(platform.GetX(), platform.GetY(), field.GetField(), platform.GetPlatform(), block));
        if (Input.GetKeyDown(KeyCode.W))
        {
            block.CreateBlock(10, 5, field.GetField());
            block.CreateBlock(13, 5, field.GetField());
        }
      
    }

  
}
