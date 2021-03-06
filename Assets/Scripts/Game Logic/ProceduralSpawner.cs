﻿using UnityEngine;

public class ProceduralSpawner : MonoBehaviour
{
    public AsteriodPool pool;

    [Header("Spawn interval Options")]
    [Min(0)]
    public float startSpawnInterval = 0.75f;
    public float intervalDecreamentAmount = 0.05f;
    public float minInterval = 0.2f;
    public float decreamentIntervalEverySecound = 60f;

    [Header("Asteroid Speed Options")]
    public float startAsteroidSpeed = 2f;
    public float speedIncreamentAmount = 0.05f;
    public float maxSpeed = 0.2f;
    public float increamentSpeedEverySecound = 120f;


    public Bounds spawnBounds;
    public bool Spawning { get; private set; } = false;

    private float nextSpawn = 0;
    private float nextSpeedIncreament = 0;
    private float nextIntervalDecreament = 0;
    private float asteroidSpeed;
    private float spawnInterval;

    public void StartSpawning()
    {
        if (Spawning) ResetSpawner();

        asteroidSpeed = startAsteroidSpeed;
        spawnInterval = startSpawnInterval;
        nextSpeedIncreament = Time.time + increamentSpeedEverySecound;
        nextIntervalDecreament = Time.time + decreamentIntervalEverySecound;
        nextSpawn = 0f;
        Spawning = true;
    }

    public void ResetSpawner()
    {
        pool.CollecteAll();
        Spawning = false;
    }

    private void Update()
    {
        if (Spawning == false) return;

        if(nextSpeedIncreament < Time.time)
        {
            asteroidSpeed = Mathf.Min(asteroidSpeed + speedIncreamentAmount,maxSpeed);
            nextSpeedIncreament = Time.time + increamentSpeedEverySecound;
        }

        if (nextIntervalDecreament < Time.time)
        {
            spawnInterval = Mathf.Max(spawnInterval - intervalDecreamentAmount, minInterval);
            nextIntervalDecreament = Time.time + decreamentIntervalEverySecound;
        }

        if (nextSpawn < Time.time)
        {
            Spawn();
            nextSpawn = Time.time + spawnInterval;
        }
    }

    private void Spawn()
    {
        //Get Random Position
        var x = Random.value * spawnBounds.size.x + spawnBounds.min.x;
        var y = Random.value * spawnBounds.size.y + spawnBounds.min.y;
        var z = Random.value * spawnBounds.size.z + spawnBounds.min.z;

        //Get asteroid from pool and set its position and speed
        pool.GetOrCreate(new Vector3(x, y, z), Quaternion.identity).SetSpeed(asteroidSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        //Draw spawn bounds
        Gizmos.DrawWireCube(spawnBounds.center, spawnBounds.size);
    }
}