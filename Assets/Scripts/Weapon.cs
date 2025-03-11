using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
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
    public bool Buy()
    {
        if (player.RemoveCoins(price)) isBought = true;
        else isBought = false;
        return isBought;
    }
    public void SetSaved()
    {
        isBought = true;
    }
    public void AssignToPlayer()
    {
        player.setNewWeapon(maxAmmo, this, WeaponObject);
        isEquiped = true;
    }
    public void Unassign()
    {
        isEquiped = false;
    }
    public int getAmmo()
    {
        return maxAmmo;
    }
    public float getFireRate()
    {
        return fireRate;
    }
   
}
