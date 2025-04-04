using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    int prefabIndex=0;
    private GameObject enemy;
    [SerializeField]
    private GameObject[] enemyPrefab;

    private List<GameObject> activeEnemyPrefab;
    [SerializeField]
    private AudioManager audioMananger;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        activeEnemyPrefab = new List<GameObject>();
        activeEnemyPrefab.Add(enemyPrefab[prefabIndex]);
        StopAllCoroutines();
        StartCoroutine(SpawnLoop());
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        
    }
    public void insertInVector()
    {
        if (prefabIndex+ 1 < enemyPrefab.Length)
        {

            prefabIndex++;
            activeEnemyPrefab.Add(enemyPrefab[prefabIndex]);

        }
       
    }
    public void clearVector()
    {
        activeEnemyPrefab.Clear();
        prefabIndex = 0;
        activeEnemyPrefab.Add(enemyPrefab[prefabIndex]);
    }
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
        audioMananger.PlayZombieSpawn();
        int index = Random.Range(0, activeEnemyPrefab.Count);
        enemy = Instantiate(activeEnemyPrefab[index], SpawnPoint, Quaternion.identity);
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
