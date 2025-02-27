using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (other.tag == "Player"&&type=="Health")
        {
            
            player.Heal(20);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Invalid collectible type!!");
        }
        
    }
    private void Clear()
    {
        if (player.get_state() == false) Destroy(gameObject);
    }

}
