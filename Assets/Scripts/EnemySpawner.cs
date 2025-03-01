using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    private GameObject enemy;
    [SerializeField]
    private GameObject enemyPrefab;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        StartCoroutine(SpawnLoop());
    }
    // Update is called once per frame

    float getValue(float min, float max, float excluded_min, float excluded_max)
    {
        float value;
        do
        {
            value = Random.Range(min, max);


        } while (value >= excluded_min && value <= excluded_max);
        return value;
    }
    void getPosition()
    {
        SpawnPoint = new Vector2(getValue(-8f, 8f, -4.07f, 4.07f), getValue(-4.06f, 4.06f, -1.9f, 1.9f));
    }
    void Spawn()
    {
        enemy = Instantiate(enemyPrefab, SpawnPoint, Quaternion.identity);
    }
    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            getPosition();

            Spawn();
        }

    }


}
