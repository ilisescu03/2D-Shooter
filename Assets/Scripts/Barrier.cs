using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Barrier : MonoBehaviour
{
    [SerializeField]
    private GameObject Builtbarrier;
    [SerializeField]
    private Player player;
    private Coroutine damageCoroutine;
    [SerializeField]
    private Collider2D Trigger;
    private float health = 1000f;
    [SerializeField] private Image barFrame;
    [SerializeField] private Image healthBarUI;
    [SerializeField] private Camera uiCamera;
    private GameObject[] enemies;
    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer<=1f&& health!=1000f && health>0)
        {
            Repair(200f*Time.deltaTime);
        }
        if(distanceToPlayer <= 1f && health == 0f)
        {
            Build(200f*Time.deltaTime);
        }
        CheckForNearbyEnemies();
        UpdateHealthBar();
    }
    public void CheckForNearbyEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance <= 1f)
            {
                if (damageCoroutine == null)
                {
                    damageCoroutine = StartCoroutine(DamageOverTime());
                }
                return;
            }
        }
        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }
    public void Repair(float amount)
    {
        if(Input.GetKey(KeyCode.E))
        {
            health += amount;
            health = Mathf.Clamp(health, 0f, 1000f);
        }
        
    }
    public void Build(float amount)
    {
        if (Input.GetKey(KeyCode.E))
        {
            health += amount;
            health = Mathf.Clamp(health, 0f, 1000f);
            Builtbarrier.SetActive(true);
        }
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            health = 0f;
            Builtbarrier.SetActive(false);
        }
    }
    public float GetHealth()
    {
        return health;
    }
   
    private void UpdateHealthBar()
    {
        if (healthBarUI != null && uiCamera != null)
        {
            healthBarUI.fillAmount = health / 1000f;

            Vector3 worldPos = transform.position + Vector3.up * 1.5f; 
            Vector3 screenPos = uiCamera.WorldToScreenPoint(worldPos);
            barFrame.transform.position = screenPos;
            healthBarUI.transform.position = screenPos;
        }
    }
    private IEnumerator DamageOverTime()
    {
        while (true)
        {
            TakeDamage(100f);
            yield return new WaitForSeconds(4f);
        }
    }

}
