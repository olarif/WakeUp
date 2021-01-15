using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonLift : MonoBehaviour
{

    public float pistonSpeed; //How fast the piston moves
    Rigidbody2D rb;
    public float speed = 5f;
    public float delay = 2;

    public float yMin = -2;
    public float yMax = 2;

    private float defTimer;
    private bool toggle;


    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        defTimer = delay;
        toggle = false;

        yMax += transform.position.y + (yMax / 4);
        yMin += transform.position.y + (yMin / 4);
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
            LaunchPiston();
        }

        if (!toggle)
        {
            RetractPiston();
        }

    }

    public void LaunchPiston()
    {
        if (transform.position.y <= yMax)
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
        }
    }

    public void RetractPiston()
    {
        if (transform.position.y >= yMin)
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }
    }
}