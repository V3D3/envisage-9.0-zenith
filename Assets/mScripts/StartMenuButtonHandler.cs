using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuButtonHandler : MonoBehaviour
{
    public Animator canvasAnimator;

    public void playButtonHover()
    {
        canvasAnimator.SetBool("PlayButtonFocused", true);
    }
    public void playButtonLeave()
    {
        canvasAnimator.SetBool("PlayButtonFocused", false);
    }
    public void playButtonDown()
    {
        canvasAnimator.SetBool("PlayButtonDown", true);
    }

    public void quitButtonHover()
    {
        canvasAnimator.SetBool("QuitButtonFocused", true);
    }
    public void quitButtonLeave()
    {
        canvasAnimator.SetBool("QuitButtonFocused", false);
    }
    public void quitButtonPressed()
    {
        canvasAnimator.SetBool("QuitButtonDown", true);
    }
    public void quitButtonPressPayload()
    {
        Application.Quit();
    }
    public void playButtonPressPayload()
    {
        FindObjectOfType<GameManagerScript>().ShowSelectMenu();
    }
}
