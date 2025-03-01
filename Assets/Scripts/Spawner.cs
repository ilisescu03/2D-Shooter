using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    protected Vector2 SpawnPoint;
    
    
    [SerializeField]
    protected float spawnTime;
    
    protected Player player;
    public void set_spawnTime(float value)
    {
        spawnTime = value;
        
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        player = FindObjectOfType<Player>();

    }

    
    



}
