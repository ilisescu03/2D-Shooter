using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Powerup : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private Text TimeText;
    [SerializeField]
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        image.enabled = false;
        TimeText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartTimer()
    {
        image.enabled = true;
        TimeText.enabled = true;
        StartCoroutine(CountdownTimer());
        
    }
    public IEnumerator CountdownTimer()
    {
   
        while (time > 0)
        {
            time -= Time.deltaTime;
            TimeText.text = time.ToString("F2");
            yield return null;
        }
        image.enabled = false;
        TimeText.enabled = false;


    }
}
