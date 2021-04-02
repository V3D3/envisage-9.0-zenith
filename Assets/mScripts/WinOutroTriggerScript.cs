using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinOutroTriggerScript : MonoBehaviour
{
    public Text scoreText;

    public void Start()
    {
        scoreText.text = "time taken: " + FindObjectOfType<GameManagerScript>().timeTaken.ToString() + " s";
    }

    public void RequestReset()
    {
        FindObjectOfType<GameManagerScript>().ShowPlayMenu();
    }
}
