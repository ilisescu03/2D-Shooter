using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class Shooting : MonoBehaviour
{
    [SerializeField]
    private Transform origin4;
    [SerializeField]
    private Transform origin3;
    [SerializeField]
    private Light2D light2D2;
    [SerializeField]
    private Transform origin;
    [SerializeField]
    private Transform origin2;
    [SerializeField]
    private Transform minigunOrigin;
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private GameObject otherBulletPrefab;
    private GameObject bullet;
    [SerializeField]
    private GameObject effect;
    [SerializeField]
    private Transform[] TreesPositions;
    
    private bool isTree=false;
    [SerializeField]
    private GameObject effect2;
    private SpriteRenderer effectRenderer;
    private SpriteRenderer effectRenderer2;
    [SerializeField]
    private Light2D light2D;
    [SerializeField]
    private Light2D minigunlight2D;
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
        light2D.enabled = false;
        light2D2.enabled = false;
        minigunlight2D.enabled = false;
        effectRenderer = effect.GetComponent<SpriteRenderer>();
        effectRenderer2 = effect2.GetComponent<SpriteRenderer>();
        initialEffect.position = effect.transform.position;
        effectRenderer.enabled = false;
        effectRenderer2.enabled = false;
        minigunEffectRenderer = minigunEffect.GetComponent<SpriteRenderer>();
        minigunEffectRenderer.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
   
        
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tree"))
        {
            
          if(!isTree) isTree=true;
          isTree=false;
        }
    }
    public void Shoot(float Offset, float damage)
    {


        if (!player.IsUsingMinigun())
        {
            if (player.getBulletType() == 0)
            {
                if (player.getWeaponTypeIndex() == 0)
                {
                    bullet = Instantiate(bulletPrefab, origin.position + origin.up * Offset, origin.rotation);
                    bullet.GetComponent<Bullet>().setDamage(damage);
                    rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(origin.up * bulletForce, ForceMode2D.Impulse);

                    effect.transform.position = origin2.position + origin2.up * Offset;
                    light2D.transform.position = origin.position + origin.up * Offset;
                    effectRenderer.enabled = true;
                    if(!isTree) light2D.enabled = true;
                }
                else
                {
                    bullet = Instantiate(bulletPrefab, origin4.position + origin4.up * Offset, origin4.rotation);
                    bullet.GetComponent<Bullet>().setDamage(damage);
                    rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(origin3.up * bulletForce, ForceMode2D.Impulse);

                    effect2.transform.position = origin3.position + origin3.up * Offset;
                    light2D2.transform.position = origin4.position + origin4.up * Offset;
                    effectRenderer2.enabled = true;
                    if(!isTree) light2D2.enabled = true;

                }
            }

            else
            {
                bullet = Instantiate(otherBulletPrefab, origin4.position + origin4.up * Offset, origin4.rotation);
                bullet.GetComponent<Bullet>().setDamage(damage);
                rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(origin3.up * bulletForce, ForceMode2D.Impulse);

                effect2.transform.position = origin3.position + origin3.up * Offset;
                light2D2.transform.position = origin4.position + origin4.up * Offset;
                effectRenderer2.enabled = true;
                if(!isTree) light2D2.enabled = true;


            }

        }
        else
        {
            minigunEffectRenderer.enabled = true;
            if(!isTree) minigunlight2D.enabled = true;
            bullet = Instantiate(otherBulletPrefab, minigunOrigin.position, minigunOrigin.rotation);
            bullet.GetComponent<Bullet>().setDamage(25f);
            rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(minigunOrigin.up * bulletForce, ForceMode2D.Impulse);

            minigunlight2D.transform.position = minigunOrigin.position;
        }

        StartCoroutine(DisableEffect());
    }

    private IEnumerator DisableEffect()
    {
        if (player.get_fire_rate() < 0.1f) yield return new WaitForSeconds(player.get_fire_rate());
        else yield return new WaitForSeconds(0.1f);
        effectRenderer.enabled = false;
        light2D.enabled = false;
        effectRenderer2.enabled = false;
        light2D2.enabled = false;

        minigunEffectRenderer.enabled = false;
        minigunlight2D.enabled = false;
    }

}
