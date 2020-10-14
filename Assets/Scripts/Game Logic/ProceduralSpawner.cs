using UnityEngine;

public class ProceduralSpawner : MonoBehaviour
{
    public AsteriodPool pool;

    [Header("Spawn interval Options")]
    [Min(0)]
    public float spawnInterval = 0.75f;
    public float intervalDecreamentAmount = 0.05f;
    public float minInterval = 0.2f;
    public float decreamentIntervalEverySecound = 60f;

    [Header("Asteroid Speed Options")]
    public float asteroidSpeed = 2f;
    public float speedIncreamentAmount = 0.05f;
    public float maxSpeed = 0.2f;
    public float increamentSpeedEverySecound = 120f;


    public Bounds spawnBounds;

    private float nextSpawn = 0;
    private float nextSpeedIncreament = 0;
    private float nextIntervalDecreament = 0;

    private void Start()
    {
        nextSpeedIncreament = Time.time + increamentSpeedEverySecound;
        nextIntervalDecreament = Time.time + decreamentIntervalEverySecound;
    }

    private void Update()
    {
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
        var x = Random.value * spawnBounds.size.x + spawnBounds.min.x;
        var y = Random.value * spawnBounds.size.y + spawnBounds.min.y;
        var z = Random.value * spawnBounds.size.z + spawnBounds.min.z;
        var instance = pool.GetOrCreate();
        instance.transform.position = new Vector3(x, y, z);
        instance.SetSpeed(asteroidSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(spawnBounds.center, spawnBounds.size);
    }
}