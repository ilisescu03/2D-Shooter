using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image fill;
    [SerializeField]
    private float changeSpeed = 15.0f;
    public float fillAmount { get; set; } = 1.0f;
    // Start is called before the first frame update
 

    // Update is called once per frame
    void Update()
    {
        fill.fillAmount = Mathf.Lerp(fill.fillAmount, fillAmount, changeSpeed * Time.deltaTime);
    }
}
