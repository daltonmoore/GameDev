using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    Grid grid;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        grid = GameObject.Find("Grid").GetComponent<Grid>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        print(grid.WorldToCell(player.transform.position));
    }
}
