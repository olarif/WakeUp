using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSound : MonoBehaviour
{
    private float tempZ;


    void Start()
    {
        tempZ = transform.eulerAngles.z;
    }

    void Update()
    {
        if (transform.eulerAngles.z > tempZ)
        {
            IsRotating();
            RotateLeft();
            
        }
        else if (transform.eulerAngles.z < tempZ)
        {
            IsRotating();
            RotateRight();
        }
        tempZ = transform.eulerAngles.z;
    }

    void RotateRight()
    {
     //   Debug.Log("right");
        
    }

    void RotateLeft()
    {
       // Debug.Log("left");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Gear")
        DoubleGearSound();
    }
    void IsRotating()
    {
      //  Debug.Log("rotating");
        
    }
    void DoubleGearSound()
    {
        string path= "event:/DoubleGear";
        FMOD.Studio.EventInstance DoubleGear = FMODUnity.RuntimeManager.CreateInstance(path);
        DoubleGear.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform));

        // DoubleGear.setParameterByName("Material", Material);
        DoubleGear.start();
        DoubleGear.release();
    }
}
