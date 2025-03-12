using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : Character
{
    [SerializeField]
    private int coins;
    [SerializeField]
    private HealthBar healthbar;
    [SerializeField]
    private int score;
    [SerializeField]
    private int high_score;
    private float fire_rate;
    private Shooting shooting;
    private bool canShoot = true;
    private bool isAlive = false;
    private bool invicibility = false;
    private float angle=2.5f;
    [SerializeField]
    protected Vector2 spawnpoint;
    private int ammo;
    private int maxammo;
    [SerializeField]
    UIManager uiManager;
    [SerializeField]
    private GameOverMenu menu;
    [SerializeField]
    private Pause pause;
    [SerializeField]
    private EnemySpawner spawner;
    [SerializeField]
    private Weapon weapon;
    [SerializeField]
    private GameObject WeaponObject;
    [SerializeField]
    private float Offset;
    [SerializeField]
    private bool[] WeaponBools;
    [SerializeField]
    private WeaponsManager weaponsManager;
    // Start is called before the first frame update
    public int get_coins() {  return coins; }
    public int get_score() { return score; }
    public int get_high_score() { return high_score; }
    public bool get_state() { return isAlive; }
    public void set_fire_rate(float value) { fire_rate = value; }
    public void set_offset(float value) { Offset = value; }
    public void setBool(int index) { WeaponBools[index] = true; }
    public void setNewWeapon(int value, Weapon newWeapon, GameObject _WeaponObject) 
    { 
        maxammo = value;
        ammo = value;
        weapon.Unassign();
        if (WeaponObject != null)
        {
            Destroy(WeaponObject);
        }

      
        WeaponObject = Instantiate(_WeaponObject, transform.position, Quaternion.identity);
        WeaponObject.transform.SetParent(transform);
        WeaponObject.transform.localPosition = Vector3.zero; 
        WeaponObject.transform.localRotation = Quaternion.identity;
        WeaponObject.transform.localScale = new Vector3(-1, -1, 1);
        if (!WeaponObject.activeSelf) WeaponObject.SetActive(true);
        weapon = newWeapon;
        
    }
    
    public float get_fire_rate() { return fire_rate; }
    public void set_invicibility(bool value) { invicibility = value; }
    public void set_angle(int value)
    {
        angle += value;
    }
    public void AddCoins(int value)
    {
        coins += value;
    }
    public bool RemoveCoins(int value)
    {
        if (value <= coins)
        {
            coins -= value;
            return true;
        }
        else
        {
            uiManager.NotEnoughCoinsShow();
            return false;
        }
    }
    public float get_angle()
    {
        return angle;
    }
    public bool[] get_WeaponBools()
    {
        return WeaponBools;
    }
    protected override void Start()
    {
        maxammo = weapon.getAmmo();
        fire_rate = weapon.getFireRate();
        ammo = maxammo;
        shooting = GetComponent<Shooting>();
        high_score =SaveManager.LoadHighScore();
        coins = SaveManager.LoadCoins();
        coins = 10000;
        angle = SaveManager.LoadAngle();
        WeaponBools = SaveManager.LoadWeapons();
        if(WeaponBools==null)
        {
            WeaponBools = new bool[3];
        }
        for(int i=0;i<WeaponBools.Length;i++)
        {
            if(WeaponBools[i]) weaponsManager.setActive(i);
        }
        uiManager.Set_Text(score, high_score);
       
    }

    // Update is called once per frame
    protected override void Update()
    {
        uiManager.Set_Ammo_Text(ammo, maxammo);
        uiManager.SetCoinsText(coins);
        GetInput();
        base.Update();
        if (direction != Vector2.zero)
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
        healthbar.fillAmount = health / maxhealth;

    }
    public void ResetAmmo()
    {
        ammo = maxammo;
    }
    public void TakeDamage(float damage)
    {
        if (invicibility) return;
        health -= damage;
        StartCoroutine(DamageEffect());
        health = Mathf.Max(health, 0.0f);
        if(health==0)
        {
            Die();
            menu.Show();
        }
    }
    public void Die()
    {
        isAlive = false;
        Debug.Log("Dead");
        
    }
    public void Heal(float points)
    {
        health += points;
        if (health > maxhealth) health = maxhealth;
    }
    public void Increase_Score(int points)
    {
        score += points;
        if (score > high_score) high_score = score;
        uiManager.Set_Text(score, high_score);
        if (score >= 100) spawner.set_spawnTime(8);
        if (score >= 200) spawner.set_spawnTime(6);
        if (score >= 350) spawner.set_spawnTime(5);
        if (score >= 500) spawner.set_spawnTime(3);
        if(score>=550) spawner.set_spawnTime(2);
        if(score>=700) spawner.set_spawnTime(1.5f);
        if(score>=1000) spawner.set_spawnTime(1);
    }
    public void Respawn()
    {
        isAlive = true;
        Time.timeScale = 1;
        menu.Hide();
        transform.position = new Vector2(spawnpoint.x, spawnpoint.y);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        health = maxhealth;
        ammo = maxammo;
        score = 0;
        spawner.set_spawnTime(10);
        uiManager.Set_Text(score, high_score);
    }
    void GetInput()
    {

        //Primeste de la tastatura input
        if (isAlive)
        {
            direction = Vector2.zero;
            if (Input.GetKey(KeyCode.W))
            {
                direction += Vector2.up;
            }
            if (Input.GetKey(KeyCode.S))
            {
                direction += Vector2.down;
            }
            if (Input.GetKey(KeyCode.A))
            {
                direction += Vector2.left;
            }
            if (Input.GetKey(KeyCode.D))
            {
                direction += Vector2.right;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(0, 0, 3.5f);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(0, 0, -3.5f);
            }
            /*
            if (Input.GetKey(KeyCode.P))
            {
                TakeDamage(10);
            }
            
            if (Input.GetKey(KeyCode.O))
            {
                Heal(10);
            }
            if (Input.GetKey(KeyCode.X))
            {
                Increase_Score(10);
            }
            if (Input.GetKey(KeyCode.R))
            {
                Respawn();
            }
            */
            if (Input.GetKey(KeyCode.Space) && canShoot && ammo>0)
            {

                shooting.Shoot(Offset);
                ammo -= 1;
                canShoot = false;
                StartCoroutine(Timer());
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (pause.get_state() == false) pause.TogglePause();
                else pause.ResumeGame();
            }
        }
        
    }
    IEnumerator DamageEffect()
    {
        SpriteRenderer effect = GetComponent<SpriteRenderer>();
        effect.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        effect.color = Color.white;
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(fire_rate);
        canShoot = true;
    }
    public void Reset_Data()
    {
        SaveManager.ResetData();
        high_score = 0;
        score = 0;
        uiManager.Set_Text(score, high_score);
    }
    
}
