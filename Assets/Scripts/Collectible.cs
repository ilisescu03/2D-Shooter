using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Collectible : MonoBehaviour
{
    [SerializeField]
    private string type;
    private Player player;
    void Start()
    {
        player = FindObjectOfType<Player>();
        
    }
    void Update()
    {
        Clear();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
            if (type == "Health")
            {

                player.Heal(20);
                Destroy(gameObject);
            }
            else if (type == "FireRate")
            {
                    Powerup powerup=GameObject.Find("FireRate").GetComponent<Powerup>();
                    powerup.StartTimer();
                    StartCoroutine(HandleFireRate());

            }
            else
            {
                    Debug.Log("Invalid collectible type!!");
            }
        }
    }
        
        
     
    private void Clear()
    {
        if (player.get_state() == false) Destroy(gameObject);
    }
    private IEnumerator HandleFireRate()
    {
        float value = player.get_fire_rate();
        player.set_fire_rate(0.1f);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(5f);
        player.set_fire_rate(value);
        Debug.Log("Fire rate reset");
        Destroy(gameObject);
    }
    
}
