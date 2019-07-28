using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMaker : MonoBehaviour
{
    //right now, board to make is to tell this script which level we are creating when we start
    //not yet implemented
    public int boardToMake = 0;

    //where to spawn the soil prefab on the board
    //starts at (-6.8, -3.7) because that is the bottom left of the board for camera: size = 4, position = (0, 0 , -10), orthographic, ratio = 16:9 
    Vector3 spawnPoint = new Vector3(-6.8f, -3.7f, 0);

    //getting bottom soil prefab from assets folder because it will never move or change
    GameObject bottomSoilPrefab;

    void Start()
    {
        //need to call resources.load in start or awake
        bottomSoilPrefab = Resources.Load<GameObject>("Prefabs/Prefab_Soil_Bottom");
        print(bottomSoilPrefab);
        SpawnSoil();
    }

    void SpawnSoil()
    {
        //sprite associated with this soil prefab
        Sprite soilSprite = bottomSoilPrefab.GetComponent<SpriteRenderer>().sprite;

        for (int i = 0; i < 10; i++)
        {
            GameObject currentEntity = Instantiate(bottomSoilPrefab, spawnPoint, Quaternion.identity);

            //ofset each soil tile by the width of the sprite. it is divided by 100 to scale the 64 pixel width to unity units.
            spawnPoint += new Vector3(soilSprite.rect.width / 100, 0);
        }
    }
}
