using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WeaponStats : MonoBehaviour
{
    [SerializeField]
    private Weapon weapon;
    [SerializeField]
    private Text WeaponName;
    [SerializeField]
    private Image FireRateFill;
    [SerializeField]
    private Image DamageFill;
    [SerializeField]
    private Image WeaponIcon;
    public void SetStats(Weapon _weapon)
    {
        weapon = _weapon;
        WeaponIcon.sprite = weapon.getSprite();
        WeaponName.text = weapon.getName();
        FireRateFill.fillAmount = weapon.getFireRate() / 10;
        DamageFill.fillAmount = weapon.getDamage() / 100;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
