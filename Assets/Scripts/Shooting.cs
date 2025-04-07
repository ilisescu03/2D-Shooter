using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
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
        minigunlight2D.enabled = false;
        effectRenderer =effect.GetComponent<SpriteRenderer>();
        initialEffect.position = effect.transform.position;
        effectRenderer.enabled = false;
        minigunEffectRenderer = minigunEffect.GetComponent<SpriteRenderer>();
        minigunEffectRenderer.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void Shoot(float Offset, float damage)
    {


        if (!player.IsUsingMinigun())
        {
            bullet = Instantiate(bulletPrefab, origin.position + origin.up * Offset, origin.rotation);
            bullet.GetComponent<Bullet>().setDamage(damage);
            rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(origin.up * bulletForce, ForceMode2D.Impulse);
            
            effect.transform.position = origin2.position + origin2.up * Offset;
            light2D.transform.position = origin.position + origin.up*Offset;
            effectRenderer.enabled = true;
            light2D.enabled = true;
        }
        else { 
            minigunEffectRenderer.enabled = true;
            minigunlight2D.enabled = true;
            bullet = Instantiate(bulletPrefab, minigunOrigin.position, minigunOrigin.rotation);
            rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(minigunOrigin.up * bulletForce, ForceMode2D.Impulse);
            
            minigunlight2D.transform.position = minigunOrigin.position;
        }
      
        StartCoroutine(DisableEffect());
    }
    private IEnumerator DisableEffect()
    {
        if(player.get_fire_rate()<0.1f) yield return new WaitForSeconds(player.get_fire_rate());
        else yield return new WaitForSeconds(0.1f);
        effectRenderer.enabled = false;
        light2D.enabled = false;
        minigunEffectRenderer.enabled = false;
        minigunlight2D.enabled = false;
    }
    
}
