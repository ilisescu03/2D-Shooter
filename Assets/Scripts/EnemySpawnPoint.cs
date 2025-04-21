using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField]
    private EnemySpawner spawner;
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
        Vector2 spawnPoint = new Vector2(transform.position.x, transform.position.y);
        if (index != 2)
        {
            enemy = Instantiate(enemies[index], spawnPoint, Quaternion.identity);
        }
        numberOfZombies++;
        spawner.IncreaseZombies();
    }
    int getSpawnTime()
    {
        int value = Random.Range(5, 25);
        return value;
    }
    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(getSpawnTime());
       

            if (numberOfZombies <= 80 && spawnEnabled) Spawn();
        }

    }
}
