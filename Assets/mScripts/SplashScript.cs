using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScript : MonoBehaviour
{
    public void RequestPlayMenu()
    {
        FindObjectOfType<GameManagerScript>().ShowPlayMenu();
    }
}
