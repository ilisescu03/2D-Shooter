using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Powerup : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private Image background;
    [SerializeField]
    private Image icon;
    [SerializeField]
    private Text TimeText;
    [SerializeField]
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        image.enabled = false;
        TimeText.enabled = false;
        background.enabled = false;
        icon.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartTimer()
    {
        image.enabled = true;
        TimeText.enabled = true;
        background.enabled = true;
        icon.enabled = true;
        StartCoroutine(CountdownTimer());
        
    }
    public IEnumerator CountdownTimer()
    {
        float aux = time;
        while (time > 0)
        {
            time -= Time.deltaTime;
            TimeText.text = time.ToString("F2");
            yield return null;
        }
        time = aux;
        image.enabled = false;
        TimeText.enabled = false;
        background.enabled = false;
        icon.enabled = false;


    }
}
