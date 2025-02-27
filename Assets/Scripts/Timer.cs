using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float time=0;
    private bool time_is_running = true;
    [SerializeField]
    private Text timeText;
    // Start is called before the first frame update
    void Start()
    {
        time_is_running = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(time_is_running)
        {
            if(time>=0)
            {
                time += Time.deltaTime;
                DisplayTime(time);
            }
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void resetTime()
    {
        time = 0;
        time_is_running = true;

    }
    public float ret_mins()
    {
        float minutes = Mathf.FloorToInt(time / 60);
        return minutes;
    }
    public float ret_secs()
    {
        float seconds = Mathf.FloorToInt(time % 60);
        return seconds;
    }
    public void stop_timer()
    {
        time_is_running = false;
    }
}
