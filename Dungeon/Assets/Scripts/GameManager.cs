using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int playerLives = 3;
    Player player;
    GameObject canvas;
    public bool[] message = new bool[7];
    public bool[] enemiesDead = new bool[7];
    AudioSource audioSource;
    AudioClip enemyDeathSound;
    int msgCount;

    void Start()
    {
        DontDestroyOnLoad(this);
        canvas = GameObject.Find("Canvas");
        DontDestroyOnLoad(GameObject.Find("EventSystem"));
        DontDestroyOnLoad(canvas);
        canvas.transform.GetChild(0).gameObject.SetActive(false); // turn off lives label
        enemyDeathSound = Resources.Load<AudioClip>("Sound/Kill Enemy");
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = enemyDeathSound;
    }

    void Update()
    {
        for (int i = 0; i < message.Length; i++)
        {
            if (!message[i])
            {
                break;
            }
            else
            {
                msgCount++;
            }
        }
        if(msgCount == 7)
        {
            print("you win");
        }
        msgCount = 0;

        for (int i = 0; i < enemiesDead.Length; i++)
        {
            if (enemiesDead[i])
            {
                audioSource.Play();
                enemiesDead[i] = false;
            }
        }
        if (player == null && SceneManager.GetActiveScene().name == "Start")
        {
            print("Grabbed Player");
            player = GameObject.Find("Player").GetComponent<Player>();
            canvas.transform.GetChild(1).gameObject.SetActive(false); // turn off play button
            canvas.transform.GetChild(0).gameObject.SetActive(true); // turn on lives label
        }
        else if (player != null)
        {
            if (player.dead)
            {
                if (playerLives > 0)
                {
                    if (!player.audioSource.isPlaying)
                    {
                        SceneManager.LoadScene("Start");

                        playerLives--;
                        canvas.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = ""+playerLives;
                        player = null;
                    }

                }
                else
                {
                    if (!player.audioSource.isPlaying)
                    {
                        SceneManager.LoadScene("Gameover");
                    }
                }
            }
        }

        if(SceneManager.GetActiveScene().name == "Gameover")
        {
            canvas.transform.GetChild(0).gameObject.SetActive(false); // turn off lives label
            canvas.transform.GetChild(2).gameObject.SetActive(true); // turn on gameover message and button
        }
    }
}
