using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Collectible : MonoBehaviour
{
    [SerializeField]
    private string type;
    private Player player;
    
    private UIManager uiManager;
    void Start()
    {
        player = FindObjectOfType<Player>();
        uiManager = FindObjectOfType<UIManager>();
        
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
            else
            {   
                if (type == "Coins")
                {
                    player.AddCoins(1);
                    Destroy(gameObject);
                }
                if (type == "Ammo")
                {
                    player.ResetAmmo();
                    Destroy(gameObject);
                }
                else if (type == "InfiniteFire" || type == "Armor")
                {
                    if (type == "InfiniteFire")
                    {
                        Powerup powerup = GameObject.Find("InfiniteFire").GetComponent<Powerup>();
                        powerup.StartTimer();
                        StartCoroutine(HandleInfiniteFire());
                    }
                    else if (type == "Armor")
                    {
                        Powerup powerup = GameObject.Find("Armor").GetComponent<Powerup>();
                        powerup.StartTimer();
                        StartCoroutine(HandleArmor());

                    }
                }
                else
                {
                    Debug.Log("Invalid collectible type!!");
                }
            }
        }
    }
        
        
     
    private void Clear()
    {
      if (player.get_state() == false) Destroy(gameObject);
    }
    private IEnumerator HandleArmor()
    {
        player.set_invicibility(true);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(5f);
        player.set_invicibility(false);
        Debug.Log("Armor reset");
        Destroy(gameObject);
    }
    private IEnumerator HandleInfiniteFire()
    {
        uiManager.HideAmmoText();
        float value = player.get_fire_rate();
        player.enableMinigun(true);
        player.set_fire_rate(0.04f);
        player.set_InfiniteFire(true);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(5f);
        player.set_fire_rate(value);
        player.set_InfiniteFire(false);
        player.enableMinigun(false);
        uiManager.ShowAmmoText();
        Debug.Log("Fire rate reset");
        Destroy(gameObject);
    }
    
}
