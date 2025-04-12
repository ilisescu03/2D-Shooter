using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    private Vector3 Offset = new Vector3(0, 0, -10);
    public float smooth = 5f;


    void Update()
    {
        if (player != null)
        {
            Vector3 targetPosition = player.position + Offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, smooth * Time.deltaTime);
        }
    }
}
