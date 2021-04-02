using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMenuHandler : MonoBehaviour
{
    const int SHIP_ZN = 0;  //zion
    const int SHIP_TR = 1;  //tyrant
    const int SHIP_DF = 2;  //defiant
    const int SHIP_MS = 3;  //messenger
    const int SHIP_ENV = 4; //env dream
    const int SHIP_AU = 5;  //aurora
    const int SHIP_SP = 6;  //sparrow

    public int selectedShip = SHIP_ENV;
    string[] shipnames = new string[7] { "ZION", "TYRANT", "DEFIANT", "MESSENGER", "ENV / DREΔM", "AURORA", "SPARROW" };

    public Button prevShipButton;
    public GameObject prevShipButtonBack;
    public Button nextShipButton;
    public GameObject nextShipButtonBack;

    public Text shipName;

    public Animator CanvasAnimator;
    public Animator ShipListAnimator;
    public GameObject ShipList;

    public void ChooseShip()
    {
        FindObjectOfType<GameManagerScript>().SetShip(selectedShip);
        FindObjectOfType<GameManagerScript>().ShowTutorial();
    }

    void SetShip(int i)
    {
        i = (i + 7) % 7;
        shipName.text = shipnames[i];
        selectedShip = i;

        i -= 4;
        Vector3 pos = ShipList.transform.position;
        ShipList.transform.position = new Vector3(pos.x, (i * 30f) - 0.5f, pos.z);
    }

    void NextShipBeginTransition()
    {
        ShipListAnimator.SetTrigger("SwitchUp");
    }
    void PrevShipBeginTransition()
    {
        ShipListAnimator.SetTrigger("SwitchDown");
    }

    public void ShiftShipNext()
    {
        SetShip(selectedShip + 1);
        ShipListAnimator.ResetTrigger("SwitchUp");
    }

    public void ShiftShipPrev()
    {
        SetShip(selectedShip - 1);
        ShipListAnimator.ResetTrigger("SwitchDown");
    }

    public void PrevShipButtonFocused()
    {
        prevShipButtonBack.GetComponent<Image>().color = new Color(1, 1, 1, 0.1f);
    }
    public void PrevShipButtonBlurred()
    {
        prevShipButtonBack.GetComponent<Image>().color = new Color(1, 1, 1, 0);
    }
    public void PrevShipButtonDown()
    {
        prevShipButtonBack.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        prevShipButton.GetComponentInChildren<Text>().color = new Color(0, 0, 0, 1);
    }
    public void PrevShipButtonUp()
    {
        prevShipButtonBack.GetComponent<Image>().color = new Color(1, 1, 1, 0.1f);
        prevShipButton.GetComponentInChildren<Text>().color = new Color(1, 1, 1, 1);

        PrevShipBeginTransition();
    }

    public void NextShipButtonFocused()
    {
        nextShipButtonBack.GetComponent<Image>().color = new Color(1, 1, 1, 0.1f);
    }
    public void NextShipButtonBlurred()
    {
        nextShipButtonBack.GetComponent<Image>().color = new Color(1, 1, 1, 0);
    }
    public void NextShipButtonDown()
    {
        nextShipButtonBack.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        nextShipButton.GetComponentInChildren<Text>().color = new Color(0, 0, 0, 1);
    }
    public void NextShipButtonUp()
    {
        nextShipButtonBack.GetComponent<Image>().color = new Color(1, 1, 1, 0.1f);
        nextShipButton.GetComponentInChildren<Text>().color = new Color(1, 1, 1, 1);

        NextShipBeginTransition();
    }
    public void ChooseButtonFocused()
    {
        CanvasAnimator.SetBool("ChooseButtonFocused", true);
    }
    public void ChooseButtonBlurred()
    {
        CanvasAnimator.SetBool("ChooseButtonFocused", false);
    }
    public void ChooseButtonDown()
    {
        CanvasAnimator.SetBool("ChooseButtonDown", true);
        Invoke("ChooseShip", 0.25f);
    }
}
