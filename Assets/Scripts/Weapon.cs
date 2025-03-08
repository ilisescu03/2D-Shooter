using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private string name;
    [SerializeField]
    private int maxAmmo;
    [SerializeField]
    private float fireRate;
    [SerializeField]
    private Player player;
    public void AssignToPlayer()
    {
        player.setNewWeapon(maxAmmo, this);
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
