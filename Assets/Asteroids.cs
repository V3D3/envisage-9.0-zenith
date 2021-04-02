using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    public GameObject[] AsteroidModelsList;
    public GameObject RealmCube;

    public Transform plane;
    public Vector3 InitialLocation = new Vector3(0, 0, 0);

    public float radius = 1;
    public Vector3 regionSize = Vector3.one;
    public int rejectionSample = 30;

    public float minspeed = 5f;
    public float maxspeed = 15f;

    public float bigAsteroidsPercent = 10f;
    public float bigSize;

    public float mediumAseroidPercent = 10f;
    public float mediumSize;

    public float smallSize;

    public float deviationValuePercent;

    private List<Vector3> points;
    private List<GameObject>[] asteroids = new List<GameObject>[3];

    #region Poission Disc Sampling 3D
    public List<Vector3> GenerateLocation(float radius, Vector3 sampleRegionSize, int numSampleBeforeRejection = 30)
    {
        float cellSize = radius / Mathf.Sqrt(3);

        int[,,] grid = new int[Mathf.CeilToInt(sampleRegionSize.x / cellSize), Mathf.CeilToInt(sampleRegionSize.y / cellSize), Mathf.CeilToInt(sampleRegionSize.z / cellSize)];

        points = new List<Vector3>();
        List<Vector3> spawnPoints = new List<Vector3>();

        spawnPoints.Add(sampleRegionSize * 0.5f);
        while (spawnPoints.Count > 0)
        {
            int spawnIndex = Random.Range(0, spawnPoints.Count);
            Vector3 spawnCenter = spawnPoints[spawnIndex];
            bool candidateAccepted = false;

            for(int i = 0; i < numSampleBeforeRejection; i++)
            {
                Vector3 dir = new Vector3(Random.Range(-1f,1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
                Vector3 candidate = spawnCenter + dir * Random.Range(radius, 2 * radius);

                if(IsValid(candidate, sampleRegionSize, cellSize, radius, points, grid))
                {

                    points.Add(candidate);
                    spawnPoints.Add(candidate);
                    grid[(int)(candidate.x / cellSize), (int)(candidate.y / cellSize), (int)(candidate.z / cellSize)] = points.Count;
                    candidateAccepted = true;
                    break;
                }
            }
            if (!candidateAccepted)
            {
                spawnPoints.RemoveAt(spawnIndex);
            }
        }
        
        return points;
    }

    bool IsValid(Vector3 candidate, Vector3 sampleRegionSize, float cellSize, float radius,List<Vector3> points, int[,,] grid)
    {
        if(candidate.x>=0 && candidate.x<sampleRegionSize.x && candidate.y >= 0 && candidate.y < sampleRegionSize.y && candidate.z >= 0 && candidate.z < sampleRegionSize.z)
        {
            int cellX = (int)(candidate.x / cellSize);
            int cellY = (int)(candidate.y / cellSize);
            int cellZ = (int)(candidate.z / cellSize);

            int searchStartX = Mathf.Max(0, cellX - 2);
            int searchEndX = Mathf.Min(cellX + 2, grid.GetLength(0) - 1);

            int searchStartY = Mathf.Max(0, cellY - 2);
            int searchEndY = Mathf.Min(cellY + 2, grid.GetLength(1) - 1);

            int searchStartZ = Mathf.Max(0, cellZ - 2);
            int searchEndZ = Mathf.Min(cellZ + 2, grid.GetLength(2) - 1);

            for(int x = searchStartX; x <= searchEndX; x++)
            {
                for (int y = searchStartY; y <= searchEndY; y++)
                {
                    for (int z = searchStartZ; z <= searchEndZ; z++)
                    {
                        int pointIndex = grid[x, y, z] - 1;
                        if (pointIndex != -1)
                        {
                            float sqrDst = (candidate - points[pointIndex]).sqrMagnitude;
                            if (sqrDst < radius * radius)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

    private void InstantiateAsteroids(Vector3 InitialLocation, int n = 0)
    {
        points = GenerateLocation(radius, regionSize, rejectionSample);
        asteroids[n] = new List<GameObject>();

        
            for (int i = 0; i < points.Count; i++)
            {
                GameObject asteroid = Instantiate(AsteroidModelsList[Random.Range(0, AsteroidModelsList.Length)], points[i] + InitialLocation - regionSize/2, Random.rotation) as GameObject;
                
                asteroid.transform.parent = transform;
                
                if ( (i % 10) <= (int)(bigAsteroidsPercent / 10))
                {
                    asteroid.transform.localScale += Vector3.one * Random.Range(bigSize * (1 - deviationValuePercent / 10), bigSize * (1 + deviationValuePercent / 10));
                }
                else if ( (i % 10) <= (int)((bigAsteroidsPercent + mediumAseroidPercent) / 10))
                {
                    asteroid.transform.localScale += Vector3.one * Random.Range(mediumSize * (1 - deviationValuePercent / 10), mediumSize * (1 + deviationValuePercent / 10));
                }
                else
                {
                    asteroid.transform.localScale += Vector3.one * Random.Range(smallSize * (1 - deviationValuePercent / 10), smallSize * (1 + deviationValuePercent / 10));
                }

                asteroid.AddComponent<AsteroidAnimation>().speed = Random.Range(minspeed, maxspeed);
                //asteroid.AddComponent<MeshCollider>().convex = true;
                asteroid.AddComponent<CapsuleCollider>();
                asteroid.tag = "asteroid";
                asteroid.name += n;
                asteroids[n].Add(asteroid);
            }
    }

    private void DestroyAsteroid(int n = 0)
    {
        if (asteroids[n] != null)
        {
            foreach (GameObject al in asteroids[n])
            {
                Destroy(al);
            }
            
        }
    }

    private int back = 0;
    private int mid = 1;
    private int front = 2;
    private void DynamicSpawn()
    {
        if(Mathf.Abs((plane.transform.position.z-InitialLocation.z))>regionSize.z/2)
        {
            if (plane.transform.position.z > InitialLocation.z)
            {
                // plane is moving forward
                DestroyAsteroid(back);
                InstantiateAsteroids(InitialLocation + new Vector3(0, 0, 2 * regionSize.z), back);
                mid = front;
                front = back;
                back++;

                if (back > 2)
                {
                    back = 0;
                }
                InitialLocation += new Vector3(0, 0, regionSize.z);

                
            }
            else if(plane.transform.position.z < InitialLocation.z)
            {
                // plane is moving backward
                DestroyAsteroid(front);
                InstantiateAsteroids(InitialLocation - new Vector3(0, 0, 2 * regionSize.z), front);
                mid = back;
                back = front;
                front--;
                if (front < 0)
                {
                    front = 2;
                }
                InitialLocation -= new Vector3(0, 0, regionSize.z);
            }            
        }
    }

    private void Awake()
    {
        InitialLocation = plane.position;

        InstantiateAsteroids(InitialLocation - new Vector3(0, 0, regionSize.z), 0);
        InstantiateAsteroids(InitialLocation, 1);
        InstantiateAsteroids(InitialLocation + new Vector3(0, 0, regionSize.z), 2);
    }

    private void Update()
    {
        DynamicSpawn();
    }

    public bool sameRadius;
    public float displayRadius;

    public void Start()
    {
        radius = 70 - (FindObjectOfType<GameManagerScript>().currentLevel * 10);
    }
}