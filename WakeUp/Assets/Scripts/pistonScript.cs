using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pistonScript : MonoBehaviour
{
    public float yMin = 0;
    public float yMax = 5;
    public float speed = 5;
    private float originY;
    public float timer = 5;
    public bool test;
    private float defSpeed;
    private float defTimer;

    void Start()
    {
        var newPostion = transform.position;
        originY = transform.position.y;
        defSpeed = speed;
        defTimer = timer;
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        timer -= Time.deltaTime;
        speed = defSpeed;

        if (timer <= 0)
        {
            speed = -speed;
            timer = defTimer;
            LaunchPiston();
        }

    }

    void LaunchPiston()
    {
        var newPosition = transform.position;
        newPosition.y += speed * Time.deltaTime;
        newPosition.y = Mathf.Clamp(newPosition.y, originY + yMin, originY + yMax);
        transform.Translate(transform.position.x, newPosition.y, 0);
    }



}
