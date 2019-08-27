using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    Grid grid;
    Tilemap tilemap;
    GameObject player;
    Transform northPoint, eastPoint, southPoint, westPoint;

    void Start()
    {
        ConstructVars();
    }

    void Update()
    {
        removeSoilPlayerTouches();
    }

    void ConstructVars()
    {
        grid = GameObject.Find("Grid").GetComponent<Grid>();
        tilemap = grid.GetComponentInChildren<Tilemap>();
        player = GameObject.Find("Player");
        northPoint = player.transform.GetChild(1);
        eastPoint = player.transform.GetChild(4);
        southPoint = player.transform.GetChild(2);
        westPoint = player.transform.GetChild(3);
    }

    void removeSoilPlayerTouches()
    {
        Vector3Int[] sides = new Vector3Int[4];
        sides[0] = grid.WorldToCell(northPoint.position);
        sides[1] = grid.WorldToCell(eastPoint.position);
        sides[2] = grid.WorldToCell(southPoint.position);
        sides[3] = grid.WorldToCell(westPoint.position);

        for(int i = 0; i < sides.Length; i++)
        {
            if (tilemap.HasTile(sides[i]) && !(tilemap.GetTile(sides[i]).name.Contains("sky")))
            {
                tilemap.SetTile(sides[i], null);
            }
        }
    }
}
