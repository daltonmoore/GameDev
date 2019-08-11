using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy : MonoBehaviour
{
    Grid grid;
    Tilemap tilemap;
    TilemapCollider2D tilemapCollider;
    Rigidbody2D rb;
    Sprite ghostSprite;
    BoxCollider2D front, back, top, bottom, body;
    GameObject player;
    float forceDirection = 1, moveForce = 2, maxVelocity = .5f;
    enum Behaviour {idling, pursuing, fleeing};
    Behaviour currentState = Behaviour.pursuing;
    bool moveHorizontally = true;

    // Start is called before the first frame update
    void Start()
    {
        if(tag == "Scuba")
            ghostSprite = Resources.Load<Sprite>("Sprites/Enemies/Scuba Guy Ghost");
        player = GameObject.Find("Player");
        grid = GameObject.Find("Grid").GetComponent<Grid>();
        rb = GetComponent<Rigidbody2D>();
        front = transform.GetChild(0).GetComponent<BoxCollider2D>();
        back = transform.GetChild(1).GetComponent<BoxCollider2D>();
        top = transform.GetChild(2).GetComponent<BoxCollider2D>();
        bottom = transform.GetChild(3).GetComponent<BoxCollider2D>();
        body = GetComponent<BoxCollider2D>();
        tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
        tilemapCollider = tilemap.GetComponent<TilemapCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case Behaviour.idling:
                movingBackAndForth();
                break;
            case Behaviour.pursuing:
                beginPursuit();
                break;
            case Behaviour.fleeing:
                break;
        }
    }

    //idling behavior
    void movingBackAndForth()
    {
        if (moveHorizontally)
        {
            if (front.IsTouching(tilemapCollider))
            {
                if (back.IsTouching(tilemapCollider))
                {
                    moveHorizontally = false;
                }
                else
                {
                    turnHorizontal();
                }
            }
            else
            {
                moveHorizontal();
            }
        }
        else//moving vertically
        {
            if (top.IsTouching(tilemapCollider) && bottom.IsTouching(tilemapCollider))
            {
                moveHorizontally = true;
            }
            if (top.IsTouching(tilemapCollider))
            {
                //print("flip force down");
                forceDirection = -1;
            }
            else if (bottom.IsTouching(tilemapCollider))
            {
                //print("flip force up");
                forceDirection = 1;
            }
            moveVertical();
        }
    }

    void moveHorizontal()
    {
        //print("moving Horizontally");

        if (rb.velocity.x < maxVelocity)
        {
            rb.AddForce(new Vector2(moveForce * forceDirection, 0));
        }
    }

    void turnHorizontal()
    {
        //print("flip");
        transform.Rotate(new Vector3(0, 180, 0));
        forceDirection *= -1;
    }

    void moveVertical()
    {
        //print("moving Vertically");

        if(rb.velocity.y < maxVelocity)
        {
            rb.AddForce(new Vector2(0, moveForce * forceDirection));
        }
    }
    
    //pursing behavior
    void beginPursuit()
    {
        goToCell(player.transform.position);
    }

    void goToCell(Vector2 playerCell)
    { 
        Debug.DrawLine(transform.position, playerCell);

        RaycastHit2D soilHit = Physics2D.Linecast(transform.position, playerCell, 1 << 8);
        RaycastHit2D playerHit = Physics2D.Linecast(transform.position, playerCell, 1);
        print("Soil "+soilHit.distance);
        print("Player "+playerHit.distance);
        var ghostmode = false;

        if(soilHit  && soilHit.collider.gameObject.layer == 8 && soilHit.collider.gameObject.name != "blank sky" 
            && Vector2.Distance(transform.position, playerCell) > 4 && !ghostmode) // 8 is soil layer
        {
            ghostmode = true;
            body.isTrigger = true;
            GetComponent<SpriteRenderer>().sprite = ghostSprite;
        }
        if (ghostmode)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerCell, .01f);
        }
        else
        {
            determineDirectionToMoveTowardsPlayer(playerHit);
        }
    }

    void determineDirectionToMoveTowardsPlayer(RaycastHit2D hit)
    {
        Vector2 frontVector = transform.position - front.transform.position;
        Vector2 towardsPlayerVector = (Vector2)transform.position - hit.point;
        print("Angle: "+Vector2.Angle(frontVector, towardsPlayerVector));
    }
}