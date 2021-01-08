using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edges : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Gears")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
