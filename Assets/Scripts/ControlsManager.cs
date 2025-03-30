using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsManager : MonoBehaviour
{
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

    public void set_ControlsIndex(int index)
    {
        player.set_ControlsIndex(index);
        Debug.Log("Controls set to " + index);
    }
}
