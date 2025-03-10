using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectingWeapon : MonoBehaviour
{
    [SerializeField]
    private Weapon weapon;
    [SerializeField]
    private GameObject buy;
    [SerializeField]
    private GameObject select;
    [SerializeField]
    private GameObject selected;
    
    public void HideBuy()
    {
        if (weapon.Buy())
        {
            buy.SetActive(false);
            ShowSelect();
        }
    }
    public void ShowSelect()
    {
        select.SetActive(true);
    }
    public void HideSelect()
    {
        select.SetActive(false);
    }
    public void ShowSelected()
    {
        selected.SetActive(true);
        weapon.AssignToPlayer();
    }
    public void HideSelected()
    {
        selected.SetActive(false);
    }
}
