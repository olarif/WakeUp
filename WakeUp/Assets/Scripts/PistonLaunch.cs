using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonLaunch : MonoBehaviour
{

    public float pistonSpeed; //How fast the piston moves

    private GameObject playerGameObject;
    Rigidbody2D rb;
    public float pistonLaunchSpeed;

    public float speed = 5f;
    public float delay = 5;
    public float multiplier = 5;

    public float yMin = -2;
    public float yMax = 2;

    private float defTimer;
    private bool toggle;
    private bool launched;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        defTimer = delay;
        toggle = false;
        launched = false;

        yMax += transform.position.y + (yMax / 4);
        yMin += transform.position.y + (yMin / 4);
    }

    private void Update()
    {
        delay -= Time.deltaTime;

        //var fill = new Vector3(0, 0, this.transform.localEulerAngles.z);
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
            launched = false;
            RetractPiston();
        }

        if (!toggle)
        {
            launched = true;
            LaunchPiston();
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

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && launched)
        {
            if(transform.position.y < yMax)
            {
                //Vector3 rotation = new Vector3(this.pistonLaunchSpeed * Time.deltaTime, 0, this.transform.rotation.z); ;

                Rigidbody2D rb = playerGameObject.GetComponent<Rigidbody2D>();
                playerGameObject.transform.Translate(0, 1, 0);
                playerGameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * pistonLaunchSpeed, ForceMode2D.Impulse);
            }   
        }    
    }
}
