using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseMenuHandler : MonoBehaviour
{
    public GameObject fadePanel;

    public void RequestRestart()
    {
        fadePanel.SetActive(true);
        Invoke("TriggerRestart", 0.5f);
    }

    public void RequestQuit()
    {
        fadePanel.SetActive(true);
        Invoke("TriggerQuit", 0.5f);
    }

    public void TriggerRestart()
    {
        FindObjectOfType<GameManagerScript>().RequestStartGame();
    }

    public void TriggerQuit()
    {
        Application.Quit();
    }
}
