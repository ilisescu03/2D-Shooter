using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
[System.Serializable]
public class SaveData
{
    public int highScore;
    public float newAngle;
    public int newCoins;
    public bool[] weaponBools;
    public SaveData(int highScore, float newAngle, int newCoins, bool[] weaponBools)
    {
        this.highScore = highScore;
        this.newAngle = newAngle;
        this.newCoins = newCoins;
        this.weaponBools = new bool[weaponBools.Length];
        for (int i=0;i<weaponBools.Length;i++)
        {
            this.weaponBools[i] = weaponBools[i];
        }
    }
}
public static class SaveManager
{
    private static string path = Application.persistentDataPath + "/save.json";
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
                return new bool[3];
            }
        }
        return new bool[3];
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
    public static float LoadAngle()
    {
        Debug.Log(Application.persistentDataPath);
        if (File.Exists(path))
        {
            try
            {
                string json = File.ReadAllText(path);
                SaveData data = JsonUtility.FromJson<SaveData>(json);
                return data.newAngle;
            }
            catch (System.Exception e)
            {
                Debug.LogWarning("Failed to load save data");
                return 0;
            }
        }
        return 0;
    }
    public static void SaveNewData(int new_highScore, float new_angle, int new_coins, bool[] new_WeaponBools)
    {
        SaveData data = new SaveData(new_highScore, new_angle, new_coins, new_WeaponBools);
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
    }
    public static void ResetData()
    {
        SaveNewData(0,2.5f,0, new bool[3]);
    }
}