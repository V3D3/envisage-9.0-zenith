using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public MFlight.Demo.Plane moveScript;
    public ShipSelect shipSelectScript;

    public GameObject plane;
    public GameObject portal;
    public GameObject aimIndicator;

    public void Start()
    {
        shipSelectScript.selectedIndex = RequestSelectedShip();
        shipSelectScript.updateModel();
        aimIndicator.SetActive(false);
        CursorControlStatic.CursorUnlock();
    }

    public void Update()
    {
        if((plane.transform.position - portal.transform.position).magnitude < 40)
        {
            CursorControlStatic.CursorUnlock();
            RequestStartGame();
        }
    }

    public void EnableFlight()
    {
        moveScript.forceMult = 100f;
        CursorControlStatic.CursorLock();
        aimIndicator.SetActive(true);
    }

    public void RequestStartGame()
    {
        FindObjectOfType<GameManagerScript>().RequestStartGame();
    }
    public int RequestSelectedShip()
    {
        return FindObjectOfType<GameManagerScript>().selectedShip;
    }
}
