using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Assets\Scripts\Piston.cs(42,56): error CS1526: A new expression requires (), [], or {} after type
//Assets\Scripts\Piston.cs(42,56): error CS1003: Syntax error, ',' expected
public class Piston : MonoBehaviour
{
    //Public Floats
    public float delay; //how long the piston pauses befroe firing agian
    public float timeFiring; //How long the piston fires
    public float pistonSpeed; //How fast the piston moves
    public float horizontalDirection; //Horizontal direction the piston goes. This is realtive to the orientation of the piston!


    float LocalDelay;
    float LocalFiringTime;
   
    void Start()
    {
        LocalDelay = delay;
        LocalFiringTime = timeFiring;
    }


    // Update is called once per frame
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


                //transform.Translate(new Vector2(0, 1) * moveSpeed * Time.deltaTime);
                //Vector2 movement = new Vector2(rb.velocity.x, jumpForce);

                //pistonSpeed
                transform.Translate(new Vector2(horizontalDirection,0) * (pistonSpeed));

            }
            else
            {
                //change firing direction
                pistonSpeed = pistonSpeed * -1;
                //reset variables
                LocalDelay = delay;
                LocalFiringTime = timeFiring;
            }
        }
    }
}
