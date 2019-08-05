using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    Game game;

    void Start()
    {
        game = GameObject.Find("Game").GetComponent<Game>();
    }

    void Update()
    {
        moveBasket();
    }

    void moveBasket()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= new Vector3(2f * Time.deltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(2f * Time.deltaTime, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        game.handleEggInBasket(collision.gameObject);
    }
}
