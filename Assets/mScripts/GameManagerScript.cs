using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public int currentLevel = 0;
    public int selectedShip = 4;
    public int timeTaken = 0;
    public bool tutorialShown = false;
    const int MAX_LEVEL = 3;

    const int SPLASH = 0;
    const int PLAYMENU = 1;
    const int SELECTMENU = 2;
    const int TUTORIAL = 3;
    const int TRANSITION = 4;
    const int PLAYMODE = 5;
    const int WINMENU = 6;
    const int LOSEMENU = 7;

    const int SHIP_ZN = 0;  //zion
    const int SHIP_TR = 1;  //tyrant
    const int SHIP_DF = 2;  //defiant
    const int SHIP_MS = 3;  //messenger
    const int SHIP_ENV = 4; //env dream
    const int SHIP_AU = 5;  //aurora
    const int SHIP_SP = 6;  //sparrow

    public void Win()
    {
        CursorControlStatic.CursorUnlock();
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene(WINMENU);
        DontDestroyOnLoad(this.gameObject);
    }

    public void Lose()
    {
        CursorControlStatic.CursorUnlock();
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene(LOSEMENU);
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetShip(int ship)
    {
        selectedShip = ship;
    }

    public void StartLevel()
    {
        CursorControlStatic.CursorLock();
        if(currentLevel >= 3)
        {
            Win();
            return;
        }
        //uses currentLevel
        //level: 0. 1. 2
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene(PLAYMODE);
        DontDestroyOnLoad(this.gameObject);
    }

    public void NextLevel()
    {
        currentLevel++;
        RequestStartGame();
    }

    public void ShowPlayMenu()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene(PLAYMENU);
        timeTaken = 0;
        currentLevel = 0;
        selectedShip = 4;
        DontDestroyOnLoad(this.gameObject);
    }

    public void ShowSelectMenu()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene(SELECTMENU);
        DontDestroyOnLoad(this.gameObject);
    }

    public void ShowTutorial()
    {
        if(!tutorialShown)
        {
            DontDestroyOnLoad(this.gameObject);
            SceneManager.LoadScene(TUTORIAL);
            tutorialShown = true;
            DontDestroyOnLoad(this.gameObject);
        }  else
        {
            RequestStartGame();
        }
    }

    public void RequestStartGame()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene(TRANSITION);
        DontDestroyOnLoad(this.gameObject);
        if (currentLevel >= 1)
        {
            timeTaken += (int) Time.timeSinceLevelLoad;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
