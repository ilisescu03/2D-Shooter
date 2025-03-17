using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    protected Vector2 SpawnPoint;
    
    //
    [SerializeField]
    protected float spawnTime;
    [SerializeField]
    protected float minTime;
    [SerializeField]
    protected float maxTime;
    protected Player player;
    public void set_spawnTime(float _minValue, float _maxValue)
    {
        minTime = _minValue;
        maxTime = _maxValue;
        
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        player = FindObjectOfType<Player>();

    }
    protected virtual void Update()
    {
        spawnTime = Random.Range(minTime, maxTime);
    }

    
    



}
