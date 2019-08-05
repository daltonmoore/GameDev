using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : Game
{
    Game game;

    private void Start()
    {
        game = GameObject.Find("Game").GetComponent<Game>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        game.handleOutOfBoundsEggs(collision.gameObject);
    }
}
