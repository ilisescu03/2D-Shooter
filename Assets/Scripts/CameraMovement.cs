using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    private Vector3 Offset = new Vector3(0, 0, -10);
    public float smooth = 5f;

    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.1f;
    private float dampingSpeed = 2f;
    private Vector3 initialPosition;


    void Update()
    {
        if (player != null)
        {
            
            Vector3 targetPosition = player.position + Offset;

           
            if (shakeDuration > 0)
            {
                targetPosition += Random.insideUnitSphere * shakeMagnitude;
                shakeDuration -= Time.deltaTime * dampingSpeed;
            }

           
            transform.position = Vector3.Lerp(transform.position, targetPosition, smooth * Time.deltaTime);
        }
    }
    public void TriggerShake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }
}
