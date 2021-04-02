using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePlay : MonoBehaviour
{
    public Animator PauseMenuAnimator;

    public AudioSource PauseSound;
    public AudioSource BGM;
    public AudioSource PauseUIElementSound;

    public GameObject aimIndicator;

    int pausePressed = 0;
    // Start is called before the first frame update
    void Start()
    {
        pausePressed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (pausePressed == 0)
            {
                BGM.Pause();
                pausePressed = 1;
                PauseSound.Play();
                StartCoroutine("SlowMo");
                aimIndicator.SetActive(false);
                PauseMenuAnimator.SetBool("Paused", true);
                CursorControlStatic.CursorUnlock();
            }
            else if (pausePressed == 1)
            {
                pausePressed = 0;
                BGM.UnPause();
                StartCoroutine("UnSlowMo");
                aimIndicator.SetActive(true);
                PauseMenuAnimator.SetBool("Paused", false);
                CursorControlStatic.CursorLock();
            }
        }
    }
    IEnumerator SlowMo()
    {
        Time.timeScale = 1f;
        for (int i = 0; i < 30; i++)
        {
            yield return new WaitForSecondsRealtime(0.017f);
            Time.timeScale -= 0.033f;
        }
        Time.timeScale = 0f;
    }
    IEnumerator UnSlowMo()
    {
        Time.timeScale = 0f;
        for (int i = 0; i < 30; i++)
        {
            yield return new WaitForSecondsRealtime(0.017f);
            Time.timeScale += 0.033f;
        }
        Time.timeScale = 1f;
    }

    public void Play()
    {
        pausePressed = 0;
        BGM.UnPause();
        StartCoroutine("UnSlowMo");
        aimIndicator.SetActive(true);
        PauseMenuAnimator.SetBool("Paused", false);
        CursorControlStatic.CursorLock();
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        PauseMenuAnimator.SetBool("Restart", true);
        // GameManger, Please restart the level
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        PauseMenuAnimator.SetBool("Menu", true);
        // GameManger, please go to main menu
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        PauseMenuAnimator.SetBool("Quit", true);
    }
}

