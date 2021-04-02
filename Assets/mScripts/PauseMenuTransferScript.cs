using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuTransferScript : MonoBehaviour
{
    public void RequestRestart()
    {
        FindObjectOfType<GameManagerScript>().RequestStartGame();
    }

    public void RequestMenu()
    {
        FindObjectOfType<GameManagerScript>().ShowPlayMenu();
    }

    public void RequestQuit()
    {
        Application.Quit();
    }
}
