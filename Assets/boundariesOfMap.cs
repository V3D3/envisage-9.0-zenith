using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class boundariesOfMap : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject BlackScreen, startPoint;
    public Transform ship;
    public float radiusOfTube, edgeOfMap;
    private Material currentMat;

    public GameObject warning;
    public AudioSource warningSound;

    public Text Deviation;
    void Start()
    {
        currentMat = BlackScreen.GetComponent<Renderer>().material;
        ChangeAlpha(currentMat, 1);
    }

    bool isWarningSoundPlaying = false;
    // Update is called once per frame
    void Update()
    {
        float r = Mathf.Sqrt(Mathf.Pow(ship.position.x - startPoint.transform.position.x, (float)2) + Mathf.Pow(ship.position.y - startPoint.transform.position.y, (float)2));

        Deviation.text = "> DEVIATION FROM TETHER: " + string.Format("{0:0.00}", r);
        if (r > radiusOfTube && r < edgeOfMap)
        {
            //(r - radiusOfTube) / (edgeOfMap - radiusOfTube)
            ChangeAlpha(currentMat, (r - radiusOfTube) / (edgeOfMap - radiusOfTube));
            warning.SetActive(true);
            if (!isWarningSoundPlaying)
            {
                warningSound.Play();
                isWarningSoundPlaying = true;
            }
            
        }
        else if (r >= edgeOfMap)
        {
            FindObjectOfType<GameManagerScript>().Lose();
        }
        else if (r < radiusOfTube)
        {
            ChangeAlpha(currentMat, 0);
            warning.SetActive(false);
            warningSound.Stop();
            isWarningSoundPlaying = false;
        }
    }

    void ChangeAlpha(Material mat, float alphaVal)
    {
        Color oldColor = mat.color;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaVal);
        mat.SetColor("_Color", newColor);
    }
}
