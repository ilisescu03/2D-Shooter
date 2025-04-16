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
        int maxattempts = 10;
        int attempts = 0;
        bool positionValid = false;
        while (!positionValid && attempts < maxattempts)
        {
            float x = Random.Range(-40f, 20f);
            float y = Random.Range(-20f, 20f);
            Vector2 potentialPosition = new Vector2(x, y);
            Collider2D hit = Physics2D.OverlapPoint(potentialPosition, LayerMask.GetMask("Obstacle"));
            if (hit==null)
            {
                SpawnPoint = potentialPosition;
                positionValid = true;
            }
            attempts++;
        }
        if (!positionValid)
        {
            Debug.Log("Nu am gasit o pozitie valida dupa " + maxattempts + " incercari.");
            SpawnPoint = Vector2.zero;
        }

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
            spawnTime = Random.Range(15, 30);
            yield return new WaitForSeconds(spawnTime);
            getPosition();

            if (player.get_score() >= 700) Spawn();
        }

    }


}
