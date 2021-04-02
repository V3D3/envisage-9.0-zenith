using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;


public class RealmCubes : MonoBehaviour
{
    public GameObject realmCube;
    public Text score;
    public AudioSource SFX;
    public AudioClip cubeGet;
    public int cubeLeft;

    [Header("Spawn Cubes")]
    [Tooltip("This the radius from the middle line( make sure it is within the warning limit)")]
    float radius = 50f;
    int numberOfCubesToSpawn = 3;
    private float[] realmCubePositions = { 200f, 400f, 600f, 800f, 1000f, 1200f, 1400f, 1600f, 1800f, 2000f, 2200f, 2500f };

    // this will places cubes in random positions
    // each cube's x and y position are random choosen on a circle and the z coordinate is given by the above array
    void Start()
    {
        numberOfCubesToSpawn = 3 + (FindObjectOfType<GameManagerScript>().currentLevel * 4);

        // to clear the spawn area
        Collider[] nearObjects = Physics.OverlapSphere(transform.position, 100);
        foreach (var item in nearObjects)
        {
            if (item.tag != "Player")
            {
                Destroy(item.gameObject);
            }
        }

        // to spawn the cubes
        int[] indexes = new int[numberOfCubesToSpawn];
        for (int i = 0; i < numberOfCubesToSpawn; i++)
        {
            bool canSpawn = true;
            indexes[i] = 0;
            int index = Random.Range(0, realmCubePositions.Length - 1);
            for (int j = 0; j < i; j++)
            {
                if (index == indexes[j])
                {
                    i--;
                    canSpawn = false;
                    break;
                }
            }

            if (canSpawn)
            {
                indexes[i] = index;

                float angle = Mathf.Deg2Rad * Random.Range(0f, 360f);
                float x = radius * Mathf.Cos(angle);
                float y = radius * Mathf.Cos(angle);

                Instantiate(realmCube, new Vector3(x, y + 285f, realmCubePositions[index]), Quaternion.identity);
            }

        }

        // initializing the score
        cubeLeft = numberOfCubesToSpawn;
        score.text = "> REALMCUBES IN VICINITY: " + cubeLeft.ToString();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("realmCube"))
        {
            other.gameObject.SetActive(false);
            cubeLeft--;
            SFX.clip = cubeGet;
            SFX.Play();
            score.text = "> REALMCUBES IN VICINITY: " + cubeLeft.ToString();
        }

        if (cubeLeft <= 0)
        {
            Time.timeScale = 0.2f;
            StartCoroutine("transitionScene");
        }

    }

    IEnumerator transitionScene()
    {
        yield return new WaitForSeconds(1);
        Time.timeScale = 1f;
        FindObjectOfType<GameManagerScript>().NextLevel();
    }
}
