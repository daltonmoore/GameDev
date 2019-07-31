using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMaker : MonoBehaviour
{
    //right now, board to make is to tell this script which level we are creating when we start
    //not yet implemented
    public int boardToMake = 0;

    //where to spawn the soil prefab on the board
    //starts at (-6.8, 3.7) because that is the top left of the board for camera: size = 4, position = (0, 0 , -10), orthographic, ratio = 16:9 
    Vector3 spawnPoint = new Vector3(-6.8f, 3.7f, 0);

    //getting tile prefabs from assets folder because it will never move or change
    string pathToBoardSprites = "Prefabs/BoardSprites/";
    GameObject skyPrefab;
    GameObject topSoilPrefab;
    GameObject midSoilPrefab;
    GameObject bottomSoilPrefab;

    //just an empty gameobject to hold the tiles of the board
    GameObject boardParentObject;

    int screenWidth = 1920;
    int screenHeight = 1280;

    void Start()
    {
        //need to call resources.load in start or awake
        skyPrefab = Resources.Load<GameObject>(pathToBoardSprites+"Sky");
        topSoilPrefab = Resources.Load<GameObject>(pathToBoardSprites + "TopSoil");
        midSoilPrefab = Resources.Load<GameObject>(pathToBoardSprites + "MiddleSoil");
        bottomSoilPrefab = Resources.Load<GameObject>(pathToBoardSprites + "BottomSoil");

        boardParentObject = GameObject.Find("Board");

        //need both of these to persist because this script will only be created one time
        DontDestroyOnLoad(boardParentObject);
        DontDestroyOnLoad(gameObject);

        //create board
        SpawnSoil();
    }

    void SpawnSoil()
    {
       switch (boardToMake)
        {
            case 0:
                makeFirstLevel();
                break;
        }
    }

    void makeFirstLevel()
    {
        //sprite associated with this soil prefab
        Sprite soilSprite = bottomSoilPrefab.GetComponent<SpriteRenderer>().sprite;
        int numberOfTilesWide = screenWidth / Mathf.RoundToInt(soilSprite.rect.width) - 7; //23 tiles (64 by 64 pixels for each tile)
        int numberOfTilesHigh = screenHeight / Mathf.RoundToInt(soilSprite.rect.height) - 7; //13 tiles (64 by 64 pixels for each tile)

        for (int row = 0; row < numberOfTilesHigh; row++)
        {
            for (int col = 0; col < numberOfTilesWide; col++)
            {
                GameObject temp = null;

                if(aBlankSpace(row, col))
                {

                }
                //we're doing the overworld
                else if (row < 3)
                {
                    temp = Instantiate(skyPrefab, spawnPoint, Quaternion.identity);
                }
                //we're doing top soil
                else if (row < 6)
                {
                    temp = Instantiate(topSoilPrefab, spawnPoint, Quaternion.identity);
                }
                //we're doing top soil
                else if (row < 9)
                {
                    temp = Instantiate(midSoilPrefab, spawnPoint, Quaternion.identity);
                }
                //we're doing top soil
                else if (row < 13)
                {
                    temp = Instantiate(bottomSoilPrefab, spawnPoint, Quaternion.identity);
                }

                //offset each soil tile by the width of the sprite. it is divided by 100 to scale the 64 pixel width to unity units.
                spawnPoint += new Vector3(soilSprite.rect.width / 100, 0);

                if (temp != null)
                    temp.transform.parent = boardParentObject.transform;
            }
            spawnPoint = new Vector3(-6.8f, spawnPoint.y);
            spawnPoint -= new Vector3(0, soilSprite.rect.height / 100);
        }
    }

    bool aBlankSpace(int row, int col)
    {
        if (boardToMake == 0)
        {
            //center player tunnel
            if ((row > 2 && row < 8) && col == 11)
            {
                return true;
            }
            //we're doing the top right space
            else if (row == 4 && (col > 17 && col < 22))
            {
                return true;
            }
            //we're doing the top left space
            //it's a tall one
            else if ((row > 4 && row < 9) && col == 2)
            {
                return true;
            }
            //we're doing the bottom left space
            else if (row == 10 && (col > 3 && col < 8))
            {
                return true;
            }
            else if((row > 8 && row < 12) && col == 15)
            {
                return true;
            }
        }
        return false;
    }
}
