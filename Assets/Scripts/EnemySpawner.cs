using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    private int prefabIndex=0;
    private GameObject enemy;
    [SerializeField]
    private GameObject[] enemyPrefab;
    private int numberOfZombies;
    private List<GameObject> activeEnemyPrefab;
    [SerializeField]
    private AudioManager audioMananger;

    // Start is called before the first frame update
    protected override void Start()
    {
        numberOfZombies = 0;
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
    public void ResetNumberOfZombies()
    {
        numberOfZombies = 0;
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
        SpawnPoint = new Vector2(getValue(-11f, 11f, -6.5f, 6.5f), getValue(-4f, 4f, -1f, 1f));
    }
    void Spawn()
    {
        audioMananger.PlayZombieSpawn();
        int index = Random.Range(0, activeEnemyPrefab.Count);
        enemy = Instantiate(activeEnemyPrefab[index], SpawnPoint, Quaternion.identity);
        numberOfZombies++;
    }
    public void DecreaseZombies()
    {
        numberOfZombies--;
    }
    
    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            getPosition();

            if(numberOfZombies<=80) Spawn();
        }

    }


}
