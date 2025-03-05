using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
[System.Serializable]
public class SaveData
{
    public int highScore;
    public float newAngle;
    public SaveData(int highScore, float newAngle)
    {
        this.highScore = highScore;
        this.newAngle = newAngle;
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
    public static void SaveNewData(int new_highScore, float new_angle)
    {
        SaveData data = new SaveData(new_highScore, new_angle);
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
    }
    public static void ResetData()
    {
        SaveNewData(0,2.5f);
    }
}