using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{ 
    GameObject canvas, spawn, goodEgg, rottenEgg;
    AudioSource basketAudioSource;
    AudioClip eggBeep;
    Text goodEggPointsText;
    public int points = 0;

    //clean means you have not touched a rotten egg, dirty means you have touched a rotten egg
    public enum GameStates { dirty, spawn, clean, rotten, phaseTwo, win};
    public GameStates state = GameStates.clean;

    private void Start()
    {
        eggBeep = Resources.Load<AudioClip>("Sound/EggGet");
        canvas = GameObject.Find("Canvas");
        goodEggPointsText = GameObject.Find("Good Egg Points").GetComponent<Text>();
        basketAudioSource = GameObject.Find("Basket").GetComponent<AudioSource>();
        basketAudioSource.clip = eggBeep;
        spawn = GameObject.Find("Spawn");
        goodEgg = Resources.Load<GameObject>("Prefabs/Egg");
        rottenEgg = Resources.Load<GameObject>("Prefabs/Rotten Egg");
    }

    void Update()
    {
        if(state == GameStates.win)
        {
            foreach(GameObject egg in GameObject.FindGameObjectsWithTag("Egg"))
            {
                Destroy(egg);
            }
            foreach (GameObject rottenEgg in GameObject.FindGameObjectsWithTag("Rotten Egg"))
            {
                Destroy(rottenEgg);
            }
        }
        if (state == GameStates.spawn)
        {
            for (int i = 0; i < 9; i++)
            {
                makeEgg("Good");
                if(i % 2 == 0)
                    makeEgg("Rotten");
            }
            state = GameStates.phaseTwo;
        }
    }

    public void handleOutOfBoundsEggs(GameObject egg)
    {
        spawnEgg(egg);
    }

    public void handleEggInBasket(GameObject egg)
    {
        basketAudioSource.Play();
        spawnEgg(egg);
        if (egg.tag == "Egg")
        {
            points++;
            goodEggPointsText.text = points.ToString();
        }
        else if(egg.tag == "Rotten Egg")
        {
            state = GameStates.dirty;
        }

        if(points >= 10 && state == GameStates.clean)
        {
            state = GameStates.spawn;
        }

        if(points >= 50)
        {
            state = GameStates.win;
        }
    }

    void spawnEgg(GameObject egg)
    {
        egg.transform.position = new Vector2(Random.Range(-1.1f, 1.1f), spawn.transform.position.y);
        egg.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    //specify good to spawn good egg and rotten for rotten egg
    void makeEgg(string eggPrefab)
    {
        GameObject temp = null;
        switch (eggPrefab)
        {
            case "Good":
                temp = Instantiate(goodEgg);
                break;
            case "Rotten":
                temp = Instantiate(rottenEgg);
                break;
        }
        if(temp != null)
        {
            spawnEgg(temp);
        }
    }
}
