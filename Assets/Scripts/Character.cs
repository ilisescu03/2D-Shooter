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
    [SerializeField]
    protected Animator animator;
    private Vector2 currentVelocity = Vector2.zero; // ADĂUGAT
    [SerializeField]
    private float smoothTime = 0.1f;
    protected Vector2 direction;
    protected Vector2 rotateDirection;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Movement.Move(direction, rb, speed, ref currentVelocity, smoothTime);
        Rotation.Rotate(transform, rotateDirection);
        
    }
}
