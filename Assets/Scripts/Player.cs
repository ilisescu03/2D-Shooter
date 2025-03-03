using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : Character
{
    [SerializeField]
    private HealthBar healthbar;
    [SerializeField]
    private int score;
    [SerializeField]
    private int high_score;
    [SerializeField]
    private float fire_rate;
    private Shooting shooting;
    private bool canShoot = true;
    private bool isAlive = false;
    private bool invicibility = false;
  
    [SerializeField]
    protected Vector2 spawnpoint;

    [SerializeField]
    UIManager uiManager;
    [SerializeField]
    private GameOverMenu menu;
    [SerializeField]
    private Pause pause;
    [SerializeField]
    private EnemySpawner spawner;
    // Start is called before the first frame update
    public int get_score() { return score; }
    public int get_high_score() { return high_score; }
    public bool get_state() { return isAlive; }
    public void set_fire_rate(float value) { fire_rate = value; }
    public float get_fire_rate() { return fire_rate; }
    public void set_invicibility(bool value) { invicibility = value; }
    protected override void Start()
    {
        
        shooting = GetComponent<Shooting>();
        high_score =SaveManager.LoadHighScore();
        uiManager.Set_Text(score, high_score);
       
    }

    // Update is called once per frame
    protected override void Update()
    {
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
                transform.Rotate(0, 0, 10f);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(0, 0, -10f);
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
            if (Input.GetKey(KeyCode.Space) && canShoot)
            {

                shooting.Shoot();

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
    public void Reset_Score()
    {
        SaveManager.ResetHighScore();
        high_score = 0;
        score = 0;
        uiManager.Set_Text(score, high_score);
    }
    
}
