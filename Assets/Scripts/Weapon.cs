using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private float damage;
    [SerializeField]
    private AudioClip gunShot;
    [SerializeField]
    private int ammoPerRound;
    [SerializeField]
    private int ID;
    [SerializeField]
    private int price;
    [SerializeField]
    private bool isBought;
    [SerializeField]
    private bool isEquiped;
    [SerializeField]
    private string name;
    [SerializeField]
    private int maxAmmo;
    [SerializeField]
    private float fireRate;
    [SerializeField]
    private Player player;
    [SerializeField]
    private GameObject WeaponObject;
    [SerializeField]
    private float Offset;
    [SerializeField]
    private SelectingWeapon selectingWeapon;
    [SerializeField]
    private float fireRatePercentage;
    [SerializeField]
    private float damagePercentage;
    public float getFireRatePercentage()
    {
        return fireRatePercentage;
    }
    public float getDamagePercentage()
    {
        return damagePercentage;
    }
    public bool Buy()
    {
        if (player.RemoveCoins(price))
        {
            isBought = true;
            player.setBool(ID);
        }
        else isBought = false;
        return isBought;
    }
    
    public Sprite getSprite()
    {
        return spriteRenderer.sprite;
    }
    public void SetSaved()
    {
        isBought = true;
        selectingWeapon.HideBuy();
    }
    public void SetEquiped()
    {
        isEquiped = true;
        selectingWeapon.ShowSelected();
    }
    public void AssignToPlayer()
    {
        player.setNewWeapon(maxAmmo, ammoPerRound, this, WeaponObject, gunShot);
        player.set_fire_rate(fireRate);
        player.set_offset(Offset);
        player.setDamage(damage);
        
        isEquiped = true;
    }
    public void Unassign()
    {
        isEquiped = false;
    }
    public string getName()
    {
        return name;
    }
    public int getAmmo()
    {
        return maxAmmo;
    }
    public int getAmmoPerRound()
    {
        return ammoPerRound;
    }
    public float getFireRate()
    {
        return fireRate;
    }
    public float getOffset()
    {
        return Offset;
    }
    public float getDamage()
    {
        return damage;
    }
    public int getID()
    {
        return ID;
    }
}
