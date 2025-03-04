using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text High_Score_Text;
    [SerializeField]
    private Text Score_Text;
    [SerializeField]
    private GameObject warning;
    public void Set_Text(int score, int high_score)
    {
        Score_Text.text = "Score:" + score;
        High_Score_Text.text = "High Score:" + high_score;
    }
    public void Show()
    {
        warning.SetActive(true);
    }
    public void Hide()
    {
        warning.SetActive(false);
    }
    public void Save()
    {
        Player player = FindObjectOfType<Player>();
        int high_score = player.get_high_score();
        SaveManager.SaveHighScore(high_score);
    }
    public void DeleteData()
    {
        SaveManager.ResetHighScore();

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
