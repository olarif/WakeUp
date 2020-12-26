using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
   
    public Transform target;
    public float speed;

    //only Z axis needed since 2D game
    private Vector3 zAxis = new Vector3(0, 0, -1);

    void Update()
    {
        //spin
        transform.RotateAround(target.position, zAxis, speed * Time.deltaTime);
    }
}
