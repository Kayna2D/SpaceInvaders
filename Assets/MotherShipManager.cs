using UnityEngine;

public class MotherShipManager : MonoBehaviour
{
    public GameObject motherShipPrefab;

    public float minSpawnTime = 30f;
    public float maxSpawnTime = 50f;

    public float spawnX = -9f;
    public float spawnY = 4.5f;

    private float nextSpawnTime;

    void Start()
    {
        ScheduleNextSpawn();
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnMotherShip();
            ScheduleNextSpawn();
        }
    }

    void ScheduleNextSpawn()
    {
        nextSpawnTime = Time.time +
                        Random.Range(minSpawnTime, maxSpawnTime);
    }

    void SpawnMotherShip()
    {
        Instantiate(
            motherShipPrefab,
            new Vector3(spawnX, spawnY, 0f),
            Quaternion.identity
        );
    }
}