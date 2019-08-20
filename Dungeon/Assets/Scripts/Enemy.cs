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
        lookAtPlayer(player.transform.position);
    }

    void lookAtPlayer(Vector2 playerCell)
    { 
        Debug.DrawLine(transform.position, playerCell);

        RaycastHit2D soilHit = Physics2D.Linecast(transform.position, playerCell, 1 << 8);
        RaycastHit2D playerHit = Physics2D.Linecast(transform.position, playerCell, 1);
        //print("Soil "+soilHit.distance);
        //print("Player "+playerHit.distance);
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
            checkSurroundingTiles();
            //moveNormalTowardsPlayer(dir);
        }
    }
    
    void checkSurroundingTiles()
    {
        //Vector2 frontVector = transform.position - front.transform.position;
        //Vector2 towardsPlayerVector = (Vector2)transform.position - playerHit.point;
        //var angleFromFrontVectorToPlayer = Vector2.SignedAngle(frontVector, towardsPlayerVector);
        //print(angleFromFrontVectorToPlayer);
        var eastVector = new Vector2(90, 0);
        Debug.DrawLine(transform.position, eastVector, Color.red);
        RaycastHit2D eastHit = Physics2D.Linecast(transform.position, eastVector);
        var eastTile = tilemap.GetTile(grid.WorldToCell(eastHit.point));
        var eastAdjTileName = eastTile != null ? eastTile.name : "empty";
        var distToEastAdjCell = Vector3.Distance( grid.WorldToCell(transform.position), grid.WorldToCell(eastHit.point));
        if( eastAdjTileName.Equals("first layer soil") && distToEastAdjCell == 1)
        {
            print("Can't go east!");
        }

        var southVector = new Vector2(0, -90);
        Debug.DrawLine(transform.position, southVector, Color.blue);
        RaycastHit2D southHit = Physics2D.Linecast(transform.position, southVector);
        var southTile = tilemap.GetTile(grid.WorldToCell(southHit.point));
        var southAdjTileName = southTile != null ? southTile.name : "empty";
        var distToSouthAdjCell = Vector3.Distance(grid.WorldToCell(transform.position), grid.WorldToCell(southHit.point));
        if(southAdjTileName.Equals("first layer soil") && distToSouthAdjCell == 1)
        {
            print("Can't go south!");
        }

        var westVector = new Vector2(-90, 0);
        Debug.DrawLine(transform.position, westVector, Color.green);
        RaycastHit2D westHit = Physics2D.Linecast(transform.position, westVector);
        var westTile = tilemap.GetTile(grid.WorldToCell(westHit.point));
        var westAdjTileName = westTile != null ? westTile.name : "empty";
        var distToWestAdjCell = Vector3.Distance(grid.WorldToCell(transform.position), grid.WorldToCell(westHit.point));
        if (westAdjTileName.Equals("first layer soil") && distToWestAdjCell == 1)
        {
            print("Can't go west!");
        }

        var northVector = new Vector2(0, 90);
        Debug.DrawLine(transform.position, northVector, Color.grey);
        RaycastHit2D northHit = Physics2D.Linecast(transform.position, northVector);
        var northTile = tilemap.GetTile(grid.WorldToCell(northHit.point));
        var northAdjTileName = northTile != null ? northTile.name : "empty";
        var distToNorthAdjCell = Vector3.Distance(grid.WorldToCell(transform.position), grid.WorldToCell(northHit.point));
        if (northAdjTileName.Equals("first layer soil") && distToNorthAdjCell == 1)
        {
            print("Can't go north!");
        }
    }


    bool moving = false;
    void moveNormalTowardsPlayer(float direction)
    {
        if (direction == 0)//move east
        {
            Vector3Int adjRightCell = grid.WorldToCell(transform.position + new Vector3(.64f, 0));
            transform.position = Vector2.Lerp(transform.position, grid.CellToWorld(adjRightCell), .01f);
        }
        else if (direction == 1)//move north
        {

        }
        else if (direction == 2)//move west
        {

        }
        else if (direction == 3)//move south
        {
            Vector3Int adjSouthCell = grid.WorldToCell(transform.position + new Vector3(0, -.64f));
            transform.position = Vector2.Lerp(transform.position, grid.CellToWorld(adjSouthCell), .01f);
        }
    }
}