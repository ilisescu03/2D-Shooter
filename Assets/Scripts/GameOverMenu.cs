using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameOverMenu : MonoBehaviour
{
    [SerializeField]
    private Text ScoreText;
    [SerializeField]
    private Text HighScoreText;
    [SerializeField]
    private Player player;
    [SerializeField]
    private Text ElapsedTime;
    [SerializeField]
    private Timer timer;
    [SerializeField]
    private StartPannel startPanel;
    
    // Start is called before the first frame update
    void Start()
    {

    }
    void Update()
    {
        int score = player.get_score();
        ScoreText.text = "Your score:" + score;
        int high_score = player.get_high_score();
        HighScoreText.text = "High score:" + high_score;
        ElapsedTime.text = string.Format("You survived:{0:00}:{1:00}", timer.ret_mins(), timer.ret_secs());
    }
    public void Show()
    {
        gameObject.SetActive(true);
        timer.stop_timer();
    }

    // Update is called once per frame
    public void Hide()
    {
        timer.resetTime();
        gameObject.SetActive(false);
    }
    public void MainMenu()
    {
        gameObject.SetActive(false);
        startPanel.Show();

    }

}
