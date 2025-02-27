using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected Rigidbody2D rb;
    [SerializeField]
    protected float health;
    [SerializeField]
    protected float maxhealth;

    protected Vector2 direction;
    protected Vector2 rotateDirection;
    // Start is called before the first frame update
    protected virtual void Start()
    {
   
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Movement.Move(direction, rb, speed);
        Rotation.Rotate(transform, rotateDirection);
        
    }
}
