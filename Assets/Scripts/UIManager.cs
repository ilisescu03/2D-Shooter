using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text High_Score_Text;
    [SerializeField]
    private Text GameplayCoinsNumber;
    [SerializeField]
    private Text ShopCoinsNumber;
    [SerializeField]
    private Text AmmoText;
    [SerializeField]
    private Text Score_Text;
    [SerializeField]
    private GameObject warning;
    [SerializeField]
    private GameObject options;
    [SerializeField]
    private GameObject Gameplay;
    [SerializeField]
    private GameObject HUD;
    [SerializeField]
    private GameObject Shop;
    [SerializeField]
    private GameObject NotEnoughCoins;
    public void NotEnoughCoinsShow()
    {
        NotEnoughCoins.SetActive(true);
    }
    public void NotEnoughCoinsHide()
    {
        NotEnoughCoins.SetActive(false);
    }
    public void Set_Ammo_Text(int ammo, int maxammo)
    {
        AmmoText.text = ammo + "/" + maxammo;
    }
    public void Set_Text(int score, int high_score)
    {
        Score_Text.text = "Score:" + score;
        High_Score_Text.text = "High Score:" + high_score;
    }
    public void SetCoinsText(int value)
    {
        GameplayCoinsNumber.text = value + " ";
        ShopCoinsNumber.text = value + " ";
    }
    public void Show()
    {
        warning.SetActive(true);
    }
    public void Hide()
    {
        warning.SetActive(false);
    }
    public void OptionsShow()
    {
        options.SetActive(true);
    }
    public void OptionsHide()
    {
        options.SetActive(false);
    }
    public void ShopShow()
    {
        Shop.SetActive(true);
    }
    public void ShopHide()
    {
        Shop.SetActive(false);
    }
    public void GameplayShow()
    {
        Gameplay.SetActive(true);
    }
    public void GameplayHide()
    {
        Gameplay.SetActive(false);
    }
    public void HUDChangeValue()
    {
        if (HUD.activeSelf)
        {
            HUD.SetActive(false);
        }
        else HUD.SetActive(true);
    }
    public void Save()
    {
        Player player = FindObjectOfType<Player>();
        int high_score = player.get_high_score();
        float angle = player.get_angle();
        int coins = player.get_coins();
        SaveManager.SaveNewData(high_score, angle, coins );
    }
    public void DeleteData()
    {
        SaveManager.ResetData();

        Quit();
    }
    public void Quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
