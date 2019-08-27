using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    [SerializeField]
    float movespeed = 0;
    Rigidbody2D rigid;
    Grid grid;
    Tilemap tileMap;
    SpriteRenderer spriteRenderer;
    BoxCollider2D frontTrigger;
    List<GameObject> enemies = new List<GameObject>();
    Vector3[] cellOffsets =
    { 
            /*east*/ new Vector3(.51f,0),
            /*north*/ new Vector3(0, .51f),
            /*west*/ new Vector3(-.51f, 0),
            /*south*/ new Vector3(0, -.51f)
    };

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        grid = GameObject.Find("Grid").GetComponent<Grid>();
        tileMap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        frontTrigger = transform.GetChild(0).GetComponent<BoxCollider2D>();
        foreach (var item in GameObject.FindGameObjectsWithTag("Scuba"))
        {
            enemies.Add(item);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Pump();
        else Move();
    }

    void Move()
    {
        
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rigid.AddForce(Vector2.right * movespeed);
            transform.localEulerAngles = new Vector3Int(0, 0, 0);
            spriteRenderer.flipY = false;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rigid.AddForce(-Vector2.right * movespeed);
            transform.localEulerAngles = new Vector3Int(0, 0, 180);
            spriteRenderer.flipY = true;
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            rigid.AddForce(Vector2.up * movespeed);
            transform.localEulerAngles = new Vector3Int(0, 0, 90);
            spriteRenderer.flipY = false;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            rigid.AddForce(-Vector2.up * movespeed);
            transform.localEulerAngles = new Vector3Int(0, -180, -90);
            spriteRenderer.flipY = false;
        }
        else
        {
            var temp = grid.WorldToCell(transform.position);
            transform.position = temp + new Vector3(.5f, .5f);
        }
    }

    void Pump()
    {
        CheckFacedTile();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroy(collision.gameObject);
    }

    void CheckFacedTile()
    {
        foreach (var item in enemies)
        {
            var enemyCollider = item.GetComponent<BoxCollider2D>();
            if (frontTrigger.IsTouching(enemyCollider))
            {
                Destroy(enemyCollider.gameObject);
            }
        }
    }

    int GetFacingDir()
    {
        var z = (int)transform.localEulerAngles.z;
        switch (z)
        {
            case 90://North
                return 1;
            case 0://East
                return 0;
            case 180://West
                return 2;
            case 270://South
                return 3;
            default:
                return -1;
        }
    }
}
