using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{

    private EnemySpawner enemySpawner;
    [SerializeField]
    private int ID;
    [SerializeField]
    private int ScorePoints;
    private bool isAttacking = false;
    private Player target;
    private GameObject coin;
    [SerializeField]
    private GameObject coinPrefab;
    // Start is called before the first frame update
    protected override void Start()
    {
        target = FindObjectOfType<Player>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        
        FollowPlayer();
        FixedAttack();
        Clear();
        base.Update();
        if(target.nextWave())
        {
            Destroy(gameObject);
        }
        
    }
    private void Clear()
    {
        if (target.get_state()==false) Destroy(gameObject);
    }
    private void FollowPlayer()
    {
        Vector2 direction = (target.transform.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);


    }
    public int GetCollectibleSpawnChance()
    {
        int value = Random.Range(0, 4);
        return value;
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        StartCoroutine(DamageEffect());
        health = Mathf.Max(health, 0.0f);
        if (health == 0)
        {
            if(GetCollectibleSpawnChance()==0)
            {
                coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            }
            enemySpawner.DecreaseZombies();
            Destroy(gameObject);
            target.Increase_Score(ScorePoints);
        }
    }
    IEnumerator DamageEffect()
    {
        SpriteRenderer effect = GetComponent<SpriteRenderer>();
        effect.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        effect.color = Color.white;
    }
    public static void ClearAll()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
    }
    public void FixedAttack()
    {
        if (Vector2.Distance(transform.position, target.transform.position) < 0.8f)
        {
            if (!isAttacking)
            {
                isAttacking = true;
                StartCoroutine(ApplyPeriodicDamage());
            }
        }
        else isAttacking = false;
    }
    IEnumerator ApplyPeriodicDamage()
    {
        while (isAttacking)
        {
            target.TakeDamage(10);
            yield return new WaitForSeconds(2f); 
        }
    }

}
