using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSelect : MonoBehaviour
{
    public List<GameObject> ships = new List<GameObject>();
    public int selectedIndex = 4;

    void Start()
    {
        selectedIndex = FindObjectOfType<GameManagerScript>().selectedShip;
        updateModel();
    }

    public void updateModel()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (i != selectedIndex)
            {
                this.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
            else
            {
                this.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }
}
