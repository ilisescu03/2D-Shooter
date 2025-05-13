using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blood : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float time;
    void Start()
    {
        StartCoroutine(BloodEffect());
    }
    IEnumerator BloodEffect()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    // Update is called once per frame
    
}
