using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int playerLives = 3;
    Player player;
    GameObject canvas, winui, liveslabel, mainmenuui, gameOver, end;
    public bool[] message = new bool[7];
    public bool[] enemiesDead = new bool[7];
    AudioSource audioSource;
    AudioClip enemyDeathSound, titleMusic;
    int msgCount;
    Button yes, no, exit, exitMain, playAgain;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        enemyDeathSound = Resources.Load<AudioClip>("Sound/Kill Enemy");
        titleMusic = Resources.Load<AudioClip>("Sound/title");
        audioSource.clip = titleMusic;
    }

    void Start()
    {
        DontDestroyOnLoad(this);
        canvas = GameObject.Find("Canvas");

        DontDestroyOnLoad(GameObject.Find("EventSystem"));
        DontDestroyOnLoad(canvas);

        yes = GameObject.Find("Yes Button").GetComponent<Button>();
        yes.onClick.AddListener(ClickYesButton);

        no = GameObject.Find("No Button").GetComponent<Button>();
        no.onClick.AddListener(ClickNoButton);

        exit = GameObject.Find("ExitButton").GetComponent<Button>();
        exit.onClick.AddListener(ClickExitButton);

        exitMain = GameObject.Find("ExitButtonMain").GetComponent<Button>();
        exitMain.onClick.AddListener(ClickExitButton);

        playAgain = GameObject.Find("PlayAgainButton").GetComponent<Button>();
        playAgain.onClick.AddListener(ClickPlayAgainButton);

        liveslabel = canvas.transform.GetChild(0).gameObject;
        liveslabel.SetActive(false); // turn off lives label

        winui = canvas.transform.GetChild(3).gameObject;
        winui.SetActive(false);

        mainmenuui = canvas.transform.GetChild(1).gameObject;

        end = canvas.transform.GetChild(4).gameObject;
        end.SetActive(false);

        gameOver = canvas.transform.GetChild(2).gameObject;
        gameOver.SetActive(false);
    }

    void Update()
    {
        var currentScene = SceneManager.GetActiveScene().name;
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
            SceneManager.LoadScene("Win");
            message = new bool[7];
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
        if (player == null && currentScene == "Start")
        {
            audioSource.clip = enemyDeathSound;
            audioSource.loop = false;
            player = GameObject.Find("Player").GetComponent<Player>();
            mainmenuui.SetActive(false); // turn off play button
            liveslabel.SetActive(true); // turn on lives label
        }
        if(currentScene != "Start" && audioSource.clip == enemyDeathSound)
        {
            audioSource.clip = titleMusic;
            audioSource.loop = true;
            audioSource.Play();
        }

        if (player != null)
        {
            if (player.dead)
            {
                if (playerLives > 0)
                {
                    if (!player.audioSource.isPlaying)
                    {
                        SceneManager.LoadScene("Start");

                        playerLives--;
                        liveslabel.transform.GetChild(0).GetComponent<Text>().text = ""+playerLives;
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
            liveslabel.gameObject.SetActive(false); // turn off lives label
            gameOver.SetActive(true);
        }

        if(SceneManager.GetActiveScene().name == "Win")
        {
            winui.SetActive(true); // turn on win ui
        }
    }

    void ClickPlayAgainButton()
    {
        Application.Quit();
    }

    void ClickExitButton()
    {
        Application.Quit();
    }

    void ClickYesButton()
    {
        winui.SetActive(false); ; // turn off win ui
        liveslabel.SetActive(false); // turn off lives label
        StartCoroutine(Wait("SheSaidYes"));
    }

    void ClickNoButton()
    {
        winui.SetActive(false); ; // turn off win ui
        liveslabel.SetActive(false); // turn off lives label
        StartCoroutine(Wait("SheSaidNo"));
        end.SetActive(true);

        audioSource.Pause();
    }

    IEnumerator Wait(string scene)
    {
        yield return new WaitForSeconds(.5f);
        winui.SetActive(false); ; // turn off win ui
        liveslabel.SetActive(false); // turn off lives label
        SceneManager.LoadScene(scene);
        end.SetActive(true);
    }
}
