using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuAudioPersist : MonoBehaviour
{
    const int PLAYMENU = 1;
    const int SELECTMENU = 2;
    const int TUTORIAL = 3;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        //will be called once
    }

    private void Update()
    {
        if ((SceneManager.GetActiveScene().buildIndex != PLAYMENU) && (SceneManager.GetActiveScene().buildIndex != SELECTMENU))
        {
            Destroy(this.gameObject);
        }
    }
}
