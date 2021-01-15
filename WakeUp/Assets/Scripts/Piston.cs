using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : MonoBehaviour
{

    public float delay; //how long the piston pauses befroe firing agian
    public float timeFiring; //How long the piston fires
    public float pistonSpeed; //How fast the piston moves
    public float horizontalDirection; //Horizontal direction the piston goes. This is realtive to the orientation of the piston!

    private GameObject playerGameObject;
    public float pistonLaunchSpeed;
    public bool launched = false;

    private float originX;
    private float originY;


    float LocalDelay;
    float LocalFiringTime;

    void Start()
    {
        LocalDelay = delay;
        LocalFiringTime = timeFiring;
        launched = false;

        originX = transform.position.x;
        originY = transform.position.y;

        playerGameObject = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        if (LocalDelay > 0)
        {
            //Delay Countdown
            LocalDelay = LocalDelay - 1;
        }
        else
        {
            if (LocalFiringTime > 0)
            {
                //Firing Time Countdown
                LocalFiringTime = LocalFiringTime - 1;

                //pistonSpeed
                transform.Translate(new Vector2(horizontalDirection, 0) * (pistonSpeed));
            }
            else
            {
                //change firing direction
                pistonSpeed = -pistonSpeed;
                //reset variables
                LocalDelay = delay;
                LocalFiringTime = timeFiring;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            float posX = transform.position.x;
            float posY = transform.position.y;

            if (posX > originX && posY > originY)
            {         
                Rigidbody2D rb = playerGameObject.GetComponent<Rigidbody2D>();
                rb.AddForce(Vector2.up * pistonLaunchSpeed, ForceMode2D.Impulse);
            }
        }
        
    }

}