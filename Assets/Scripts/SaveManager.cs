using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
[System.Serializable]
public class SaveData
{
    public int highScore;
    public float newAngleSliderValue;
    public int newCoins;
    public bool[] weaponBools;
    public int weaponID;
    public int aimControlsIndex;
    public bool autoSave;
    public SaveData(int highScore, float newAngleSliderValue, int newCoins, bool[] weaponBools, int weaponID, int aimControlsIndex, bool autoSave)
    {
        this.highScore = highScore;
        this.newAngleSliderValue = newAngleSliderValue;
        this.newCoins = newCoins;
        this.weaponBools = new bool[weaponBools.Length];
        this.autoSave = autoSave;
        for (int i=0;i<weaponBools.Length;i++)
        {
            this.weaponBools[i] = weaponBools[i];
        }
        this.weaponID = weaponID;
        this.aimControlsIndex = aimControlsIndex;
    }
}
public static class SaveManager
{
    #if UNITY_EDITOR
        private static string path = Application.dataPath + "/EditorSave/save.json";
    #else
        private static string path = Application.persistentDataPath + "/save.json";
    #endif

    public static bool LoadAutoSave()
    {
        Debug.Log(Application.persistentDataPath);
        if(File.Exists(path))
        {
            try
            {
                string json = File.ReadAllText(path);
                SaveData data = JsonUtility.FromJson<SaveData>(json);
                return data.autoSave;
            }
            catch (System.Exception e)
            {
                Debug.LogWarning("Failed to load save data");
                return false;
            }
        }
        return false;
    }
    public static int LoadAimControlsIndex()
    {
        Debug.Log(Application.persistentDataPath);
        if (File.Exists(path))
        {
            try
            {
                string json = File.ReadAllText(path);
                SaveData data = JsonUtility.FromJson<SaveData>(json);
                return data.aimControlsIndex;
            }
            catch (System.Exception e)
            {
                Debug.LogWarning("Failed to load save data");
                return 0;
            }
        }
        return 0;
    }
    public static int LoadWeapon()
    {
        Debug.Log(Application.persistentDataPath);
        if (File.Exists(path))
        {
            try
            {
                string json = File.ReadAllText(path);
                SaveData data = JsonUtility.FromJson<SaveData>(json);
                return data.weaponID;
            }
            catch (System.Exception e)
            {
                Debug.LogWarning("Failed to load save data");
                return 0;
            }
           
        }
        return 0;
    }
    public static bool[] LoadWeapons()
    {
        Debug.Log(Application.persistentDataPath);
        if (File.Exists(path))
        {
            try
            {
                string json = File.ReadAllText(path);
                SaveData data = JsonUtility.FromJson<SaveData>(json);
                return data.weaponBools;
            }
            catch (System.Exception e)
            {
                Debug.LogWarning("Failed to load save data");
                return new bool[9];
            }
        }
        return new bool[9];
    }
    public static int LoadHighScore()
    {
        Debug.Log(Application.persistentDataPath);
        if (File.Exists(path))
        {
            try
            {
                string json = File.ReadAllText(path);
                SaveData data = JsonUtility.FromJson<SaveData>(json);
                return data.highScore;
            }
            catch (System.Exception e)
            {
                Debug.LogWarning("Failed to load save data");
                return 0;
            }
        }
        return 0;
    }
    public static int LoadCoins()
    {
        Debug.Log(Application.persistentDataPath);
        if (File.Exists(path))
        {
            try
            {
                string json = File.ReadAllText(path);
                SaveData data = JsonUtility.FromJson<SaveData>(json);
                return data.newCoins;
            }
            catch (System.Exception e)
            {
                Debug.LogWarning("Failed to load save data");
                return 0;
            }
        }
        return 0;
    }
    public static float LoadNewAngleSliderValue()
    {
        Debug.Log(Application.persistentDataPath);
        if (File.Exists(path))
        {
            try
            {
                string json = File.ReadAllText(path);
                SaveData data = JsonUtility.FromJson<SaveData>(json);
                return data.newAngleSliderValue;
            }
            catch (System.Exception e)
            {
                Debug.LogWarning("Failed to load save data");
                return 0;
            }
        }
        return 0;
    }
    public static void SaveNewData(int new_highScore, float new_AngleSliderValue, int new_coins, bool[] new_WeaponBools, int new_WeaponID, int new_aimControlsIndex, bool new_autoSave)
    {
        SaveData data = new SaveData(new_highScore, new_AngleSliderValue, new_coins, new_WeaponBools, new_WeaponID, new_aimControlsIndex, new_autoSave);
        string json = JsonUtility.ToJson(data);
        Directory.CreateDirectory(Path.GetDirectoryName(path));
        File.WriteAllText(path, json);
    }
    public static void ResetData()
    {
        SaveNewData(0,0.7f,0, new bool[9], 0, 0, false);
    }
}