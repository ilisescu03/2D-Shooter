using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
[System.Serializable]
public class SaveData
{
    public int highScore;
    public SaveData(int highScore)
    {
        this.highScore = highScore;
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
    public static void SaveHighScore(int new_highScore)
    {
        SaveData data = new SaveData(new_highScore);
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
    }
    public static void ResetHighScore()
    {
        SaveHighScore(0);
    }
}