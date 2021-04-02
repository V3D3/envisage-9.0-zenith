using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dying : MonoBehaviour
{
    int maxHits = 5;
    int health = 5;
    public AudioSource SFX;
    public AudioClip crash1, crash2;

    public Text healthLabel;

    void Start()
    {
        maxHits = 7 - FindObjectOfType<GameManagerScript>().currentLevel;
        health = maxHits;
    }

    int instanceId = 0;
    int oldInstanceId = 0;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (Random.Range(0, 2) == 1)
        {
            SFX.clip = crash1;
            SFX.Play();
        }
        else
        {
            SFX.clip = crash2;
            SFX.Play();
        }
        if(collision.gameObject.CompareTag("asteroid"))
        {
            instanceId = collision.gameObject.GetInstanceID();

            if (instanceId != oldInstanceId)
            {
                health--;
                oldInstanceId = instanceId;
            }

            healthLabel.text = "> SHIP INTEGRITY: " + string.Format("{0:0.00}", health * 100f / maxHits) + "%";
            
            if (health <= 0)
            {
                FindObjectOfType<GameManagerScript>().Lose();
            }
        }
    }
}
