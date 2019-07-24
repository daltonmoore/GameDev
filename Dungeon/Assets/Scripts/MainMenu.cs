using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    Button playGame;

    void Start()
    {
        playGame = GameObject.Find("Play Game Button").GetComponent<Button>();
        playGame.onClick.AddListener(ClickPlayGameButton);
    }
    
    void ClickPlayGameButton()
    {
        SceneManager.LoadScene("Start");
    }
}
