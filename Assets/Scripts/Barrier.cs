using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Barrier : MonoBehaviour
{
    [SerializeField]
    private GameObject Builtbarrier;
    [SerializeField] private GameObject HUD;
    [SerializeField] private GameObject EKeyImage;
    [SerializeField] private GameObject EKeyPressedImage;
    [SerializeField]
    private GameObject coinPrefab;
    [SerializeField]
    private Player player;
    private Coroutine damageCoroutine;
    [SerializeField]
    private Collider2D Trigger;
    private float health = 1000f;
    private float repairProgress = 0f;
    [SerializeField] private Image barFrame;
    [SerializeField] private Image healthBarUI;
    [SerializeField] private Camera uiCamera;
    private GameObject[] enemies;
    [SerializeField] private AudioSource DestroyAudio;
    [SerializeField] private AudioSource RepairAudio;

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
     
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        bool isInRange = player.CheckClosestBarrierDistance();
        bool isRepairable = player.GetNearestBarrierHealth() != 1000f && player.GetNearestBarrierHealth() > 0;
        bool isRebuildable = player.GetNearestBarrierHealth() == 0f;
   
        // Gestionare UI pentru E key
        if (isInRange && (isRepairable || isRebuildable))
        {
 
            if (Input.GetKey(KeyCode.E))
            {
                EKeyImage.SetActive(false);
                EKeyPressedImage.SetActive(true);
            }
            else
            {
                EKeyImage.SetActive(true);
                EKeyPressedImage.SetActive(false);
            }
        }
        
        else
        {
            
                
             EKeyImage.SetActive(false);
             EKeyPressedImage.SetActive(false);
            
        }

        // Executare reparare/construire
        if (isInRange)
        {
            if (isRepairable)
                Repair(250f * Time.deltaTime);
            else if (isRebuildable)
                Build(250f * Time.deltaTime);
        }

        // Resetare dacă player-ul e "mort"
        if (player.get_state() == false)
        {
            health = 1000f;
            repairProgress = 0f;
            Builtbarrier.SetActive(true);
            DestroyAudio.Stop();
            RepairAudio.Stop();
            EKeyImage.SetActive(false);
            EKeyPressedImage.SetActive(false);
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
            if (distance <= 2f)
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
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (Input.GetKey(KeyCode.E)&&distance<=1.3f)
        {
            if (!RepairAudio.isPlaying) RepairAudio.Play();
            repairProgress += amount;

            while (repairProgress >= 250f)
            {
                repairProgress -= 250f;
                Instantiate(coinPrefab, transform.position, Quaternion.identity);
            }

            health += amount;
            health = Mathf.Clamp(health, 0f, 1000f);
        }
        else
        {
            RepairAudio.Stop();
        }
    }

    public void Build(float amount)
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (Input.GetKey(KeyCode.E) && distance <= 1.3f)
        {
            if (!RepairAudio.isPlaying) RepairAudio.Play();
            repairProgress += amount;

            while (repairProgress >= 250f)
            {
                repairProgress -= 250f;
                Instantiate(coinPrefab, transform.position, Quaternion.identity);
            }

            health += amount;
            health = Mathf.Clamp(health, 0f, 1000f);
            Builtbarrier.SetActive(true);
        }
        else
        {
            RepairAudio.Stop();
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            DestroyAudio.Play();
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
