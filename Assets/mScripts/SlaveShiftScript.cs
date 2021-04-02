using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlaveShiftScript : MonoBehaviour
{
    public SelectMenuHandler masterScript;
    public AudioSource audioPlayer;
    public AudioClip clickSound;
    public void ShiftUp()
    {
        audioPlayer.Play();
        masterScript.ShiftShipNext();
    }

    public void ShiftDown()
    {
        audioPlayer.Play();
        masterScript.ShiftShipPrev();
    }
}
