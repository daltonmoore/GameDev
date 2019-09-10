using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    [SerializeField]
    float movespeed = 0, lastPumpTime = 0, lives = 3;
    public bool inflating = false, moving = false, dead= false;
    Enemy currentTarget;
    Rigidbody2D rigid;
    Grid grid;
    Tilemap tileMap;
    public SpriteRenderer spriteRenderer;
    BoxCollider2D frontTrigger;
    List<GameObject> enemies = new List<GameObject>();
    Animator anim;
    public AudioSource audioSource;
    public AudioClip deathSound;
    Vector3[] cellOffsets =
    { 
            /*east*/ new Vector3(.51f,0),
            /*north*/ new Vector3(0, .51f),
            /*west*/ new Vector3(-.51f, 0),
            /*south*/ new Vector3(0, -.51f)
    };

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        deathSound = Resources.Load<AudioClip>("Sound/Death");
        anim = GetComponent<Animator>();
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
        if (dead)
            return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Pumping", true);
            Pump();
        }
        else
        {
            Move();
        }
    }

    void Move()
    {
        float horz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        if (vert != 0 || horz != 0)
        {
            anim.SetBool("Pumping", false);
            anim.SetBool("Moving", true);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
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
            anim.SetBool("Moving", false);
        }
    }

    void Pump()
    {
        CheckFacedTile();
    }

    void CheckFacedTile()
    {
        foreach (var item in enemies)
        {
            if (item == null)
                continue;
            var enemyCollider = item.GetComponent<BoxCollider2D>();
            var enemy = item.GetComponent<Enemy>();
            if (frontTrigger.IsTouching(enemyCollider))
            {
                currentTarget = enemy;
                lastPumpTime = Time.time;
                inflating = true;
                currentTarget.anim.SetBool("Inflating", inflating);
                enemy.HitWithPump();
                StartCoroutine(PumpReset());
                break;
            }
        }
    }

    IEnumerator PumpReset()
    {
        yield return new WaitForSeconds(.2f);
        if (Time.time - lastPumpTime > .1f)
        {
            inflating = false;
            anim.SetBool("Pumping", false);
        }
        else
        {
            inflating = true;
        }
        if(currentTarget != null)
            currentTarget.anim.SetBool("Inflating",inflating);
    }
}
