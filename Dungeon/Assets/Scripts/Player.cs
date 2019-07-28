using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float movespeed = 0;
    Rigidbody2D rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        move();
    }

    void move()
    {
        if(Input.GetKey(KeyCode.D))
        {
            rigid.AddForce(Vector2.right * movespeed);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rigid.AddForce(-Vector2.right * movespeed);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            rigid.AddForce(Vector2.up * movespeed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rigid.AddForce(-Vector2.up * movespeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
