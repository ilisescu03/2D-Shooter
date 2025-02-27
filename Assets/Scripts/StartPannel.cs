using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartPannel : MonoBehaviour
{
    [SerializeField]
    private Player player;
    [SerializeField]
    private Text RecordScoreText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RecordScoreText.text = "Record Score :" + player.get_record_score();
    }
    public void Hide()
    {
        gameObject.SetActive(false);
        player.Respawn();
    }
    public void Show()
    {
        gameObject.SetActive(true);
        player.Die();
    }
}
