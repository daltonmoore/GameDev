using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float movespeed;

    void Start()
    {
        
    }

    void Update()
    {
        move();
    }

    void move()
    {
        if(Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector3(transform.position.x + movespeed, transform.position.y);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector3(transform.position.x - movespeed, transform.position.y);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + movespeed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - movespeed);
        }
    }
}
