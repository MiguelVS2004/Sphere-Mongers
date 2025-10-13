using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public List<Sphere> spheres = new List<Sphere>();
    public int currWave;
    private int waveValue;
    public List<GameObject> spheresToSpawn = new List<GameObject>();
    public Transform minSpawn, maxSpawn;
    public float spawnHeight = 5f; 
    public int spawnIndex;
    public int waveDuration;
    private float waveTimer;

    // Start is called before the first frame update
    void Start()
    {
        GenerateWave();
        waveTimer = 5f; // First wave spawns after 5 seconds
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        waveTimer -= Time.fixedDeltaTime;

        if (waveTimer <= 0)
        {
            // Spawn all spheres in the wave at once
            SpawnWave();
            currWave++;
            GenerateWave();
            waveTimer = 10f; // Reset timer for next wave
        }
    }

    public void SpawnWave()
    {
        foreach (GameObject spherePrefab in spheresToSpawn)
        {
            Vector3 spawnPoint = SelectSpawnPoint();
            Instantiate(spherePrefab, spawnPoint, Quaternion.identity);
        }
    }

    public Vector3 SelectSpawnPoint()
    {
        Vector3 spawnPoint = Vector3.zero;

        // Random X position between minSpawn and maxSpawn
        spawnPoint.x = Random.Range(minSpawn.position.x, maxSpawn.position.x);

        // Random Z position between minSpawn and maxSpawn
        spawnPoint.z = Random.Range(minSpawn.position.z, maxSpawn.position.z);

        // Fixed height
        spawnPoint.y = spawnHeight;

        return spawnPoint;
    }

    public void GenerateWave()
    {
        waveValue = currWave * 10;
        GenerateSpheres();
    }

    public void GenerateSpheres()
    {
        List<GameObject> generatedSpheres = new List<GameObject>();

        while (waveValue > 0 && generatedSpheres.Count < 50)
        {
            int randSphereId = Random.Range(0, spheres.Count);
            int randSphereCost = spheres[randSphereId].cost;

            if (waveValue - randSphereCost >= 0)
            {
                generatedSpheres.Add(spheres[randSphereId].spherePrefab);
                waveValue -= randSphereCost;
            }
            else
            {
                break;
            }
        }

        spheresToSpawn.Clear();
        spheresToSpawn = generatedSpheres;
    }
}

[System.Serializable]
public class Sphere
{
    public GameObject spherePrefab;
    public int cost;
}