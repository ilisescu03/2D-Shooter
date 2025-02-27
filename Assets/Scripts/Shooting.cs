using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private Transform origin;
    [SerializeField]
    private GameObject bulletPrefab;
    private GameObject bullet;
    [SerializeField]
    private float bulletForce;
    private Rigidbody2D rb;
    [SerializeField]
    private Player player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Shoot()
    {
        bullet = Instantiate(bulletPrefab, origin.position, origin.rotation);
        rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(origin.up * bulletForce, ForceMode2D.Impulse);
    }
    
}
