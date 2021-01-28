using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Transform target;
    public float speed;
    public float direction;
    public Rigidbody2D rb;
    float Onesec = 1f;

    //only Z axis needed since 2D game
    private Vector3 zAxis = new Vector3(0, 0, -1);

    void Update()
    {
        StartCoroutine(CountDownrotate());
        //spin
        //transform.RotateAround(target.position, zAxis, direction * speed * Time.deltaTime);
    }   
    void FixedUpdate()
    {
        if(rb!=null)
        {
            rb.rotation += 1f;
        }
        
    }


    // StartCoroutine(CountDownrotate());
    //spin
    //transform.RotateAround(target.position, zAxis, direction * speed * Time.deltaTime);

    private IEnumerator CountDownrotate()
    {
        transform.RotateAround(target.position, zAxis, direction * speed * Time.deltaTime);
        yield return Onesec;
    }
}
