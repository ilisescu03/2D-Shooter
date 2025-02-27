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
    private int record_score;
    [SerializeField]
    private float fire_rate;
    private Shooting shooting;
    private bool canShoot = true;
    private bool isAlive = false;
    [SerializeField]
    protected Vector2 spawnpoint;
    [SerializeField]
    private Text Score_Text;
    [SerializeField]
    private Text Record_Score_Text;
    [SerializeField]
    private GameOverMenu menu;
    [SerializeField]
    private Pause pause;
    [SerializeField]
    private Spawner spawner;
    // Start is called before the first frame update
    public int get_score() { return score; }
    public int get_record_score() { return record_score; }
    public bool get_state() { return isAlive; }
    protected override void Start()
    {
        shooting = GetComponent<Shooting>();
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        GetInput();
        base.Update();
        healthbar.fillAmount = health / maxhealth;

    }
    public void TakeDamage(float damage)
    {
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
        if (score > record_score) record_score = score;
        Score_Text.text = "Score:" + score;
        Record_Score_Text.text = "Record Score:" + record_score;
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
        Score_Text.text = "Score:" + score;
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
    
}
