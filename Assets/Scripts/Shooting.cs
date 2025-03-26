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
    private Transform minigunOrigin;
    [SerializeField]
    private GameObject bulletPrefab;
    private GameObject bullet;
    [SerializeField]
    private GameObject effect;
    private SpriteRenderer effectRenderer;
    [SerializeField]
    private GameObject minigunEffect;
    private SpriteRenderer minigunEffectRenderer;
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
        minigunEffectRenderer = minigunEffect.GetComponent<SpriteRenderer>();
        minigunEffectRenderer.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Shoot(float Offset)
    {


        if (!player.IsUsingMinigun())
        {
            bullet = Instantiate(bulletPrefab, origin.position + origin.up * Offset, origin.rotation);
            rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(origin.up * bulletForce, ForceMode2D.Impulse);

            effect.transform.position = origin2.position + origin2.up * Offset;
            effectRenderer.enabled = true;
        }
        else { 
            minigunEffectRenderer.enabled = true;
            bullet = Instantiate(bulletPrefab, minigunOrigin.position, minigunOrigin.rotation);
            rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(minigunOrigin.up * bulletForce, ForceMode2D.Impulse);
        }
      
        StartCoroutine(DisableEffect());
    }
    private IEnumerator DisableEffect()
    {
        if(player.get_fire_rate()<0.1f) yield return new WaitForSeconds(player.get_fire_rate());
        else yield return new WaitForSeconds(0.1f);
        effectRenderer.enabled = false;
        minigunEffectRenderer.enabled = false;
    }
    
}
