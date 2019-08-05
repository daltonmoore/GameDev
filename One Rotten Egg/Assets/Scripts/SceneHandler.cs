using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    Game game;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        GameObject.Find("Begin Button").GetComponent<Button>().onClick.AddListener(GoToMainGame);
        SceneManager.sceneLoaded += SceneLoaded;
    }

    private void Update()
    {
        if(game != null)
        {
            if(game.state == Game.GameStates.dirty)
                SceneManager.LoadScene("RottenScene");
            if (game.state == Game.GameStates.win)
                SceneManager.LoadScene("WinScene");
        }
    }

    void GoToMainGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    void SceneLoaded(Scene s, LoadSceneMode loadSceneMode)
    {
        if(s.name == "MainScene")
        {
            game = GameObject.Find("Game").GetComponent<Game>();
        }
        else if(s.name == "RottenScene")
        {
            GameObject.Find("Replay Button").GetComponent<Button>().onClick.AddListener(GoToMainGame);
        }
        else if(s.name == "WinScene")
        {
            GameObject.Find("Replay Button").GetComponent<Button>().onClick.AddListener(GoToMainGame);
        }
    }
}
