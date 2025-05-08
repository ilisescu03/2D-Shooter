using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField]
    private EnemySpawner spawner;
    [SerializeField]
    private Transform[] spawnPoints;
    private List<GameObject> enemies;
    private int numberOfZombies;
    private bool spawnEnabled;
    private GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemies=spawner.getEnemiesList();
        numberOfZombies = spawner.getNumberOfZombies();
        spawnEnabled = spawner.IsSpawnEnabled();
        StopAllCoroutines();
        StartCoroutine(SpawnLoop());
    }

    // Update is called once per frame
    void Update()
    {
        enemies = spawner.getEnemiesList();
        numberOfZombies = spawner.getNumberOfZombies();
        spawnEnabled = spawner.IsSpawnEnabled();

    }
    void Spawn()
    {
        // audioMananger.PlayZombieSpawn();
        int index = Random.Range(0, enemies.Count);
        int spawnpointIndex = Random.Range(0, spawnPoints.Length);
        Vector2 spawnPoint = new Vector2(spawnPoints[spawnpointIndex].position.x, spawnPoints[spawnpointIndex].position.y);
        if (index != 2)
        {
            enemy = Instantiate(enemies[index], spawnPoint, Quaternion.identity);
        }
        numberOfZombies++;
        spawner.IncreaseZombies();
    }
    float getSpawnTime()
    {
        float value = Random.Range(spawner.retMinTime()/2f, spawner.retMaxTime()/2f);
        return value;
    }
    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(getSpawnTime());
       

            if (spawnEnabled) Spawn();
        }

    }
}
