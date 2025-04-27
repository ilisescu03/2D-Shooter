using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public static void Move(Vector2 direction, Rigidbody2D rb, float speed, ref Vector2 currentVelocity, float smoothTime)
    {
        Vector2 targetVelocity = direction.normalized * speed;
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref currentVelocity, smoothTime);
    }
}

