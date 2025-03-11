using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsManager : MonoBehaviour
{
    [SerializeField]
    private Weapon[] Weapons;

    public void setActive(int index)
    {
        Weapons[index].SetSaved();
    }
}
