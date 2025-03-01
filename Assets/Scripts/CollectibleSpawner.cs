using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : Spawner
{
    private GameObject collectible;
    [SerializeField]
    private GameObject[] collectiblePrefab;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        StartCoroutine(SpawnLoop());
    }
    void getPosition()
    {
        float x = Random.Range(-8f, 8f);
        float y = Random.Range(-4.06f, 4.06f);
        SpawnPoint = new Vector2(x, y);
    }
    void Spawn()
    {
        int index = Random.Range(0, collectiblePrefab.Length);
        collectible = Instantiate(collectiblePrefab[index], SpawnPoint, Quaternion.identity);
    }
    IEnumerator SpawnLoop()
    {
        while (true)
        {
            spawnTime = Random.Range(30, 60);
            yield return new WaitForSeconds(spawnTime);
            getPosition();

            if (player.get_score() >= 700) Spawn();
        }

    }


}
