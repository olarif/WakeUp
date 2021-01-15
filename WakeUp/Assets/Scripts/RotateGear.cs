using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGear : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;

   
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();

        float rotation = rb.rotation;
    }

    private void FixedUpdate()
    {
        rb.rotation -= speed;
    }
}
