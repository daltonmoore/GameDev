using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPickup : MonoBehaviour
{
    GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            switch (name)
            {
                case "b" + "(Clone)":
                    gameManager.message[2] = true;
                    break;
                case "m" + "(Clone)":
                    gameManager.message[3] = true;
                    break;
                case "y" + "(Clone)":
                    gameManager.message[4] = true;
                    break;
                case "g" + "(Clone)":
                    gameManager.message[5] = true;
                    break;
                case "f" + "(Clone)":
                    gameManager.message[6] = true;
                    break;
                case "u" + "(Clone)":
                    gameManager.message[1] = true;
                    break;
                case "w" + "(Clone)":
                    gameManager.message[0] = true;
                    break;
            }
            Destroy(gameObject);
        }
    }
}
