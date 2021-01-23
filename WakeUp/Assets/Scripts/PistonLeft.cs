using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonLeft : MonoBehaviour
{
    public float pistonSpeed; //How fast the piston moves
    Rigidbody2D rb;
    public float speed = 5f;
    public float delay = 2;

    public float xMin = -2;
    public float xMax = 2;

    private float defTimer;
    private bool toggle;


    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        defTimer = delay;
        toggle = false;

        xMax += transform.position.x + (xMax / 4);
        xMin += transform.position.x + (xMin / 4);
    }

    private void Update()
    {
        delay -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        if (delay <= 0)
        {
            delay = defTimer;
            toggle = !toggle;
        }

        if (toggle)
        {
            RetractPiston();
        }

        if (!toggle)
        {
            LaunchPiston();
        }

    }

    public void LaunchPiston()
    {
        if (transform.position.x <= xMax)
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
        }
    }

    public void RetractPiston()
    {
        if (transform.position.x >= xMin)
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }
    }
}
