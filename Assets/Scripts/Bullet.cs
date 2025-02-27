using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            Animator animator = GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("impact");
            }
            Destroy(gameObject);
        }
        if(other.tag=="Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage(10);
            Destroy(gameObject);
        }
    }
    
}
