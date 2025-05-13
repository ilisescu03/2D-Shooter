using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject sAmmoImage;
    [SerializeField] private GameObject pAmmoImage;
    [SerializeField]
    private Toggle AutoSaveToggle;
    [SerializeField]
    private Toggle FullscreenToggle;
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
    private Slider RotationSensitivity;
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
    [SerializeField]
    private AudioManager audioManager;
    [SerializeField]
    private GameObject HammerImage;
    public float speed = 100f; // viteza de rotire
    private float targetRotation; // ținta de rotire
    private float initialRotation; // unghiul inițial
    private bool HammerRotatingClockWise=true;
    private void Start()
    {
        if(Screen.fullScreen)
        {
           FullscreenToggle.isOn = true;
           audioManager.StopSFX();
        }
        else
        {
            FullscreenToggle.isOn = false;
            audioManager.StopSFX();
        }
        initialRotation = HammerImage.transform.rotation.eulerAngles.z;
        targetRotation = initialRotation + 45f;
    }
    public void setSAmmoImage()
    {
        sAmmoImage.SetActive(true);
        pAmmoImage.SetActive(false);
    }
    public void setPAmmoImage()
    {
        pAmmoImage.SetActive(true);
        sAmmoImage.SetActive(false);
    }
    private void Update()
{
    if (ReloadingImage.activeSelf)
    {
        ReloadingImage.transform.Rotate(0, 0, 135 * Time.deltaTime);
    }

    if (HammerImage != null)
    {
        float currentRotation = NormalizeAngle(HammerImage.transform.eulerAngles.z);
        float step = speed * Time.deltaTime;

        if (HammerRotatingClockWise)
        {
            currentRotation += step;
            if (currentRotation >= 0f)
            {
                currentRotation = 0f;
                HammerRotatingClockWise = false;
            }
        }
        else
        {
            currentRotation -= step;
            if (currentRotation <= -45f)
            {
                currentRotation = -45f;
                HammerRotatingClockWise = true;
            }
        }

        HammerImage.transform.rotation = Quaternion.Euler(0, 0, currentRotation);
    }
}

// Functie ca sa normalizeze unghiul intre -180 si 180
private float NormalizeAngle(float angle)
{
    angle = angle % 360;
    if (angle > 180)
        angle -= 360;
    return angle;
}
    
    public void OnRotationSensitivityChanged()
    {
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.angleSliderValue = RotationSensitivity.value;
        }
    }
    public void SetRotationSensitivity(float value)
    { 
        RotationSensitivity.value = value;
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
    public void ToggleAutoSaveButton(bool value, bool value1)
    {
        if(value==true)
        {
            AutoSaveToggle.isOn = true;
            if(!value1) audioManager.StopSFX();
        }
        else
        {
            AutoSaveToggle.isOn = false;
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
    public bool isHUDActive()
    {
        return HUD.activeSelf;
    }
    public void Save()
    {
        Player player = FindObjectOfType<Player>();
        int high_score = player.get_high_score();
        float _angleSliderValue = player.angleSliderValue;
        int coins = player.get_coins();
        bool[] WeaponBools = player.get_WeaponBools();
        int WeaponID = player.getWeaponID();
        int AimControlsIndex = player.get_AimControlsIndex();
        bool AutoSave = AutoSaveToggle.isOn;
        SaveManager.SaveNewData(high_score, _angleSliderValue, coins,  WeaponBools, WeaponID, AimControlsIndex, AutoSave);
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
