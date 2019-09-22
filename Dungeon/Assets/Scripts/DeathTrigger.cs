using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    GameObject playerObj;
    AudioSource audioSource;
    Player player;
    private void Start()
    {
        playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<Player>();
        audioSource = player.GetComponent<AudioSource>();
    }

    private void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Scuba" && !player.dead)
        {
            player.dead = true;
            audioSource.clip = player.deathSound;
            audioSource.Play();
            audioSource.loop = false;
            Destroy(player.spriteRenderer);
        }
    }
}
