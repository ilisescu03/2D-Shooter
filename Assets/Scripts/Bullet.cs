using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage = 10f;
    [SerializeField]
    private GameObject bulletHitEffect;
    public void setDamage(float damage)
    {
        this.damage = damage;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wall" || other.tag == "Edge")
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            if (other.CompareTag("Wall"))
            {
         
                Vector2 bulletDirection = rb.velocity.normalized;
                float angle = Mathf.Atan2(bulletDirection.y, bulletDirection.x) * Mathf.Rad2Deg - 90f; // Ajustează pentru sprite orientat în sus

                GameObject effect = Instantiate(
                    bulletHitEffect,
                    transform.position,
                    Quaternion.Euler(0, 0, angle)
                );
                Destroy(effect, 2f);
                Destroy(gameObject);
            }
            Destroy(gameObject);
        }
        if (other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage(damage, other);
            enemy.BloodEffect(transform.position);
            Destroy(gameObject);
        }
    }

}
