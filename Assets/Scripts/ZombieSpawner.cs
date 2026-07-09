using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 5f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnZombie), 0f, spawnInterval);
    }

    void SpawnZombie()
    {
        if (spawnPoints.Length == 0) return;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
