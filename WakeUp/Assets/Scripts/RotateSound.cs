using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSound : MonoBehaviour
{
    private float defaultZ;
    private float defaultY;
    private float defaultX;
    private Vector3 currentEulerAngles;
    private Vector3 defaultEulerAngles;
    private Vector3 checkAngle;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 defaultEulerAngles = transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentEulerAngles = transform.localEulerAngles;

        if (currentEulerAngles.z > defaultEulerAngles.z)
        {
            Rotate();
            defaultEulerAngles.z = currentEulerAngles.z;
        }
        else if (currentEulerAngles.z == defaultEulerAngles.z)
        {
            NotRotating();
        }
    }

    void Rotate()
    {
        Debug.Log("rotating");
    }

    void NotRotating()
    {
        Debug.Log("not rotating");
    }
}
