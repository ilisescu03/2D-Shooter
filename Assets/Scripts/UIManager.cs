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
    private Text CountdownText;
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
    private GameObject Audio;
    [SerializeField]
    private GameObject HUD;
    [SerializeField]
    private GameObject Keybinds;
    [SerializeField]
    private GameObject Shop;
    [SerializeField]
    private GameObject NotEnoughCoins;
    [SerializeField]
    private ControlsManager controlsManager;
    [SerializeField]
    private GameObject[] SelectFrame;
    private string temporaryText;
    [SerializeField]
    private GameObject ReloadingImage;
    [SerializeField]
    private GameObject StatsPannel;
    private void Update()
    {
        if (ReloadingImage.activeSelf)
        {
            ReloadingImage.transform.Rotate(0, 0, 135 * Time.deltaTime);
        }

    }
    public void ShowStatsPannel(Weapon weapon)
    {
        StatsPannel.SetActive(true);
        StatsPannel.GetComponent<WeaponStats>().SetStats(weapon);
    }
    public void HideStatsPannel()
    {
        StatsPannel.SetActive(false);
    }
    public void ShowReloading()
    {
        ReloadingImage.SetActive(true);
    }
    public void HideReloading()
    {
        ReloadingImage.SetActive(false);
    }
    public void SetSelectFrame(int index)
    {
        for (int i = 0; i < SelectFrame.Length; i++)
        {
            if (i == index)
            {
                SelectFrame[i].SetActive(true);
            }
            else SelectFrame[i].SetActive(false);
        }
    }
    public void Fullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void KeybindsShow()
    {
        Keybinds.SetActive(true);
    }
    public void KeybindsHide()
    {
        Keybinds.SetActive(false);
    }
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
    public void ShowCountdownText()
    {
        CountdownText.gameObject.SetActive(true);
    }
    public void HideCountdownText()
    {
        CountdownText.gameObject.SetActive(false);
    }
    public void SetCountdownText(int value)
    {
        CountdownText.text = "Next Wave: " + value; 
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
    public void AudioShow()
    {
        Audio.SetActive(true);
    }
    public void AudioHide()
    {
        Audio.SetActive(false);
    }
    public bool isOptionsActive()
    {
        return options.activeSelf;
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
