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
        if (player.isPlayerInBarrierRange()&& health!=1000f && health>0)
        {
            Repair(100f*Time.deltaTime);
        }
        if(player.isPlayerInBarrierRange() && health == 0f)
        {
            Build(100f*Time.deltaTime);
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
            if (distance < 0.1f)
            {
                Debug.Log("Enemy touches the barrier");
                TakeDamage(50f);
                break; 
            }
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

}
