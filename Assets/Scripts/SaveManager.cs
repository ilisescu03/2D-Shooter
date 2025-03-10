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
    public SaveData(int highScore, float newAngle, int newCoins)
    {
        this.highScore = highScore;
        this.newAngle = newAngle;
        this.newCoins = newCoins;
    }
}
public static class SaveManager
{
    private static string path = Application.persistentDataPath + "/save.json";
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
    public static void SaveNewData(int new_highScore, float new_angle, int new_coins)
    {
        SaveData data = new SaveData(new_highScore, new_angle, new_coins);
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
    }
    public static void ResetData()
    {
        SaveNewData(0,2.5f,0);
    }
}