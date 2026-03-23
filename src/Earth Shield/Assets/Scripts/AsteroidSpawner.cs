using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject largeAsteroid;
    public GameObject mediumAsteroid;
    public GameObject smallAsteroid;

    public float spawnInterval = 2f;
    public float spawnDistance = 1.5f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnAsteroid();
            timer = 0f;
        }
    }

    void SpawnAsteroid()
    {
        Camera cam = Camera.main;

        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        Vector2 spawnPos = Vector2.zero;

        int side = Random.Range(0, 4);

        switch (side)
        {
            case 0: // Top
                spawnPos = new Vector2(Random.Range(-width, width), height);
                break;
            case 1: // Bottom
                spawnPos = new Vector2(Random.Range(-width, width), -height);
                break;
            case 2: // Left
                spawnPos = new Vector2(-width, Random.Range(-height, height));
                break;
            case 3: // Right
                spawnPos = new Vector2(width, Random.Range(-height, height));
                break;
        }

        spawnPos *= spawnDistance;

        GameObject prefabToSpawn;

        float rand = Random.value;

        if (rand < 0.6f)
            prefabToSpawn = largeAsteroid;
        else if (rand < 0.85f) 
            prefabToSpawn = mediumAsteroid;
        else 
            prefabToSpawn = smallAsteroid;

        Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
    }
}
