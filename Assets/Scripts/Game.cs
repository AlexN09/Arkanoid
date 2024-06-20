using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private GameObject Object;
    private Field field;
    private Platform platform;
    void Start()
    {
      Object = gameObject;
      field = Object.GetComponent<Field>();
      platform = Object.GetComponent<Platform>();
       field.Initialize();
        platform.Initialize(field.GetField(), field.GetWidth() - 4,field.GetHeight() - 1,3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
