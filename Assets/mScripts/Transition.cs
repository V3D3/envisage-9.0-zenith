using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Transition : MonoBehaviour
{
    bool now = false;
    
    void LateUpdate()
    {
        if (GetComponent<VideoPlayer>().isPrepared && !now)
        {
            now = true;
        }  else if (GetComponent<VideoPlayer>().isPaused && now)
        {
            FindObjectOfType<GameManagerScript>().StartLevel();
        }
    }
}
