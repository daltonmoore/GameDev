using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class Enemy : MonoBehaviour
{
    Grid grid;
    Tilemap tilemap;
    TilemapCollider2D tilemapCollider;
    Rigidbody2D rb;
    Sprite ghostSprite, normalSprite;
    BoxCollider2D front, back, top, bottom, body;
    GameObject player;
    float forceDirection = 1, moveForce = 2, maxVelocity = .5f;
    enum Behaviour {idling, becomeGhost, ghostingToCell, becomeNormal, pursuing, fleeing};
    Behaviour currentState = Behaviour.pursuing;
    bool moveHorizontally = true, moving = false;
    Vector3 playerPos;
    int moveCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (tag == "Scuba")
        {
            ghostSprite = Resources.Load<Sprite>("Sprites/Enemies/Scuba Guy Ghost");
            normalSprite = Resources.Load<Sprite>("Sprites/Enemies/Scuba Guy");
        }
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
            case Behaviour.becomeGhost:
                becomeGhost();
                break;
            case Behaviour.ghostingToCell:
                ghostingToLastPlayerPos();
                break;
            case Behaviour.becomeNormal:
                becomeNormal();
                break;
            case Behaviour.pursuing:
                pursue();
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

    //ghost behavior
    void becomeGhost()
    {
        body.isTrigger = true;
        GetComponent<SpriteRenderer>().sprite = ghostSprite;
        playerPos = player.transform.position;
        currentState = Behaviour.ghostingToCell;
    }

    void ghostingToLastPlayerPos()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPos, .0075f);
        if (transform.position == playerPos)
        {
            playerPos = Vector3.zero;
            currentState = Behaviour.becomeNormal;
        }
    }

    void becomeNormal()
    {
        body.isTrigger = false;
        GetComponent<SpriteRenderer>().sprite = normalSprite;
        currentState = Behaviour.pursuing;
    }

    public bool canMoveEast, canMoveNorth, canMoveWest, canMoveSouth;
    void pursue()
    {
        checkSurroundingTiles();
        move();
    }

    void findDirTowardsPlayer()
    {
        Vector2 frontVector = transform.position - front.transform.position;
        RaycastHit2D playerHit = Physics2D.Linecast(transform.position, player.transform.position, 1);
        Vector2 towardsPlayerVector = (Vector2)transform.position - playerHit.point;
        var angleFromFrontVectorToPlayer = Vector2.SignedAngle(frontVector, towardsPlayerVector);
        //print(angleFromFrontVectorToPlayer);
    }

    Vector3Int[] adjCells = new Vector3Int[4];
    void checkSurroundingTiles()
    {
        if (!moving)
        {
            var temp = grid.WorldToCell(transform.position);
            transform.position = temp + new Vector3(.5f, .5f);
        }

        Vector3[] offsets = 
        { 
                /*east*/ new Vector3(.51f,0),
                /*north*/ new Vector3(0, .51f),
                /*west*/ new Vector3(-.51f, 0),
                /*south*/ new Vector3(0, -.51f)
        };
        bool[] canMoves = new bool[4];

        int i = 0;
        foreach (var adjCellOffset in offsets)
        {
            var adjCell = grid.LocalToCell(grid.WorldToLocal(transform.position + adjCellOffset));
            adjCells[i] = adjCell;
            var adjTile = tilemap.GetTile(adjCell);
            if (adjTile == null)
                canMoves[i] = true;
            else if (isSoil(adjTile.name))
            {
                canMoves[i] = false;
            }
            i++;
        }
        canMoveEast = canMoves[0];
        canMoveNorth = canMoves[1];
        canMoveWest = canMoves[2];
        canMoveSouth = canMoves[3];
    }

    public int dir;
    Vector3 targetLoc;
    Vector3 cellToWorldOffset = new Vector3(.5f, .5f);
    void move()
    {
        //print("moving = "+moving);
        if(!moving)
        {
            dir = GetDir();
            if (dir == -1)
            {
                //Debug.LogError("dir is -1");
                //EditorApplication.isPaused = true;
                return;
            }
            switch (dir)
            {
                case 0://targetLoc equals east adjacent cell
                    targetLoc = adjCells[0];
                    break;
                case 1://targetLoc equals north adjacent cell
                    targetLoc = adjCells[1];
                    break;
                case 2://targetLoc equals west adjacent cell
                    targetLoc = adjCells[2];
                    break;
                case 3://targetLoc equals south adjacent cell
                    targetLoc = adjCells[3];
                    break;
            }
            targetLoc += cellToWorldOffset;
            moving = true;
        }
        else
        {
            //print("Target Loc = " + targetLoc);
            //print("Distance to target loc = " + Vector3.Distance(transform.position, targetLoc));
            transform.position = Vector3.MoveTowards(transform.position, targetLoc, .01f);

            //done moving
            if (Vector3.Distance(transform.position, targetLoc) < .01f)
            {
                moving = false;
                if(moveCounter > 10)
                {
                    moveCounter = 0;
                    currentState = Behaviour.becomeGhost;
                }
            }
        }
    }

    //utility
    Vector3Int getPlayerCell()
    {
        var temp = grid.WorldToLocal(player.transform.position);
        return (grid.LocalToCell(temp));
    }


    int GetDir()
    {
        moveCounter++;
        
        var dir = Random.Range(0, 4);
        if (dir == 0 && canMoveEast) return 0;
        else if (dir == 1 && canMoveSouth) return 3;
        else if (dir == 2 && canMoveWest) return 2;
        else if (dir == 3 && canMoveNorth) return 1;
        
        else return -1;
    }

    bool isSoil(string tileName)
    {
        switch (tileName)
        {
            case "first layer soil":
                return true;
            case "second layer soil":
                return true;
            case "third layer soil":
                return true;
            case "fourth layer soil":
                return true;
        }
        return false;
    }
}