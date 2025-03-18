using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class PostProcessingController : MonoBehaviour
{
    [SerializeField]
    private PostProcessVolume postProcessingVolume;
    private bool isOn=true;
    public void ToggleGraphics()
    {
        if (isOn) { postProcessingVolume.enabled = false; isOn = false; }
        else { postProcessingVolume.enabled = true; isOn = true;}
    }


}
