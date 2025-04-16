using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    private bool spawnEnabled = true;
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
    public void DisableSpawn()
    {
        spawnEnabled = false;
        StopAllCoroutines();
    }
    public void EnableSpawn()
    {
        spawnEnabled = true;
        StopAllCoroutines();
        StartCoroutine(SpawnLoop());
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
        int maxAttempts = 10; // Numărul maxim de încercări pentru a găsi o poziție validă
        int attempts = 0;
        bool positionValid = false;

        while (!positionValid && attempts < maxAttempts)
        {
            // Generează o poziție aleatorie
            Vector2 potentialPosition = new Vector2(
                getValue(-42f, 23f, -6.5f, 6.5f),
                getValue(-20f, 20f, -1f, 1f)
            );

            // Verifică dacă există un collider cu layer-ul "Obstacle" în poziția generată
            Collider2D hit = Physics2D.OverlapPoint(potentialPosition, LayerMask.GetMask("Obstacle"));

            if (hit == null)
            {
                // Dacă nu există obstacole, poziția este validă
                SpawnPoint = potentialPosition;
                positionValid = true;
            }

            attempts++;
        }

        if (!positionValid)
        {
            Debug.LogWarning("Nu s-a găsit o poziție validă pentru spawn după " + maxAttempts + " încercări.");
            SpawnPoint = Vector2.zero; // Poziție fallback
        }
    }

    void Spawn()
    {
       // audioMananger.PlayZombieSpawn();
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

            if(numberOfZombies<=80&&spawnEnabled) Spawn();
        }

    }


}
