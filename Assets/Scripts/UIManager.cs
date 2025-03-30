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
    private Text Wave_Text;
    [SerializeField]
    private GameObject warning;
    [SerializeField]
    private GameObject options;
    [SerializeField]
    private GameObject Gameplay;
    [SerializeField]
    private GameObject Controls;
    [SerializeField]
    private GameObject AimControls;
    [SerializeField]
    private GameObject Video;
    [SerializeField]
    private GameObject HUD;
    [SerializeField]
    private GameObject Shop;
    [SerializeField]
    private GameObject NotEnoughCoins;
    [SerializeField]
    private ControlsManager controlsManager;
    private string temporaryText;
    public void NotEnoughCoinsShow()
    {
        NotEnoughCoins.SetActive(true);
    }
    public void HideAmmoText()
    {
        temporaryText = AmmoText.text;
        AmmoText.text="";
    }
    public void SetControlsIndex(int index)
    {
        controlsManager.set_ControlsIndex(index);
    }
    public void ShowAmmoText()
    {
        AmmoText.text = temporaryText;
    }
    public void NotEnoughCoinsHide()
    {
        NotEnoughCoins.SetActive(false);
    }
    public void Set_Ammo_Text(int ammo, int maxammo)
    {
        AmmoText.text = ammo + "/" + maxammo;
    }
    public void Set_Text(int score, int high_score, int index)
    {
        Score_Text.text = "Score:" + score;
        High_Score_Text.text = "High Score:" + high_score;
        Wave_Text.text = "Wave:" + index;
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
    public void ControlsShow()
    {
        Controls.SetActive(true);
    }
    public void ControlsHide()
    {
        Controls.SetActive(false);
    }
    public void AimControlsShow()
    {
        AimControls.SetActive(true);
    }
    public void AimControlsHide()
    {
        AimControls.SetActive(false);
    }
    public void VideoHide()
    {
        Video.SetActive(false);
    }
    public void VideoShow()
    {
        Video.SetActive(true);
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
        bool[] WeaponBools = player.get_WeaponBools();
        SaveManager.SaveNewData(high_score, angle, coins,  WeaponBools);
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
