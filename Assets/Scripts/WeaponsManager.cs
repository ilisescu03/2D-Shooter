using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsManager : MonoBehaviour
{
    [SerializeField]
    private Weapon[] Weapons;
    public void SetEquiped(int index)
    {
        Weapons[index].SetEquiped();
    }
    public void setActive(int index)
    {
        Weapons[index].SetSaved();
    }
    public void AssignToPlayer(int index)
    {
        Weapons[index].AssignToPlayer();
    }
}
