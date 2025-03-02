using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public static void Move(Vector2 direction, Rigidbody2D rb, float speed)
    {
        rb.velocity = direction.normalized * speed;
        
    }
}

