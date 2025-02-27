using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Vector2 SpawnPoint;
    private GameObject collectible;
    [SerializeField]
    private GameObject[] collectiblePrefab;
    private GameObject enemy;
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private float spawnTime;
    private float collectibleSpawnTime=55;
    private Player player;
    public void set_spawnTime(float value)
    {
        spawnTime = value;
        player = FindObjectOfType<Player>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnLoop());
        StartCoroutine(CollectibleSpawnLoop());
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
        SpawnPoint = new Vector2(getValue(-8f,8f,-4.07f, 4.07f), getValue(-4.06f,4.06f,-1.9f,1.9f));
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
    void getCollectiblePosition()
    {
        float x = Random.Range(-8f, 8f);
        float y = Random.Range(-4.06f, 4.06f);
        SpawnPoint = new Vector2(x,y);
    }
    void CollectibleSpawn()
    {
        int index = Random.Range(0, collectiblePrefab.Length);
        collectible = Instantiate(collectiblePrefab[index], SpawnPoint, Quaternion.identity);
    }
    IEnumerator CollectibleSpawnLoop()
    {
        while (true)
        {
            collectibleSpawnTime = Random.Range(30, 60);
            yield return new WaitForSeconds(collectibleSpawnTime);
            getCollectiblePosition();

            if (player.get_score() >= 700) CollectibleSpawn();
        }

    }



}
