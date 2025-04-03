using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartPannel : MonoBehaviour
{
    [SerializeField]
    private Player player;
    [SerializeField]
    private Text HighScoreText;
    [SerializeField]
    private AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        audioManager.PlayMainMenuMusic();
    }

    // Update is called once per frame
    void Update()
    {
        HighScoreText.text = "High Score :" + player.get_high_score();
        
    }
    public void Hide()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        player.Respawn();
        audioManager.StopMainMenuMusic();
    }
    public void Show()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
        player.Die();
        audioManager.PlayMainMenuMusic();
    }
}
