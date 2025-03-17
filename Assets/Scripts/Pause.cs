using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private bool isPaused = false;
    [SerializeField]
    private StartPannel startPanel;
    [SerializeField]
    private Player player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    public void set_state(bool value)
    {
        isPaused = value;
    }*/
    public bool get_state()
    {
        return isPaused;
    }
    public void TogglePause()
    {
        isPaused = true;
        Time.timeScale = 0;
        gameObject.SetActive(true);
    }
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
    public void MainMenu()
    {
       gameObject.SetActive(false);
        isPaused = false;
        player.Die();
        startPanel.Show();

    }
}
