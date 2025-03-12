using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private Transform origin;
    [SerializeField]
    private Transform origin2;
    [SerializeField]
    private GameObject bulletPrefab;
    private GameObject bullet;
    [SerializeField]
    private GameObject effect;
    private SpriteRenderer effectRenderer;
    [SerializeField]
    private Transform initialEffect;
    [SerializeField]
    private float bulletForce;
    private Rigidbody2D rb;
    [SerializeField]
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        effectRenderer=effect.GetComponent<SpriteRenderer>();
        initialEffect.position = effect.transform.position;
        effectRenderer.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Shoot(float Offset)
    {
        
        bullet = Instantiate(bulletPrefab, origin.position+origin.up*Offset, origin.rotation);
        rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(origin.up * bulletForce, ForceMode2D.Impulse);
        effect.transform.position = origin2.position + origin2.up * Offset;
        effectRenderer.enabled = true;
        StartCoroutine(DisableEffect());
    }
    private IEnumerator DisableEffect()
    {
        if(player.get_fire_rate()<0.1f) yield return new WaitForSeconds(player.get_fire_rate());
        else yield return new WaitForSeconds(0.1f);
        effectRenderer.enabled = false;
    }
    
}
