using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    CircleCollider2D circleCollider;
    public bool grounded = true;
    public float moveForce, jumpForce;
    Rigidbody2D rigid;

    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        move();
    }

    void move()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigid.AddForce(Vector2.left * moveForce * Time.deltaTime, ForceMode2D.Force);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rigid.AddForce(Vector2.right * moveForce * Time.deltaTime);
        }
    }
}
