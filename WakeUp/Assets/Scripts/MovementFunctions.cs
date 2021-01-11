using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementFunctions : MonoBehaviour
{
    float x, y;
    float t;
    public float ofsetX, ofsetY,rangeX,rangeY,speedX,speedY;
    public bool Func1, Func2,Func3,Func4,Func5,Func6;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Func1)
        {
            t += Time.deltaTime;
            x = (Mathf.Cos(speedX * t));
            y = (Mathf.Sin(speedY * t));
            transform.position = new Vector3(((x * rangeX) - ofsetX), ((y * rangeY) - ofsetY), 0);
        }    
        if (Func2)
        {
            t += Time.deltaTime;
            x = 1- (Mathf.Cos(speedX*t));
            y = 1- (Mathf.Sin(speedY*t));
            y *=y ;
            x *= x ;
            transform.position = new Vector3(((x * rangeX) - ofsetX), ((y * rangeY) - ofsetY), 0);
        }
        if (Func3)
        {
            t += Time.deltaTime;
            x = (Mathf.Cos(speedX * t));
            y = (Mathf.Sin(speedY * t));
            y *= y*y;
            x *= x*x;
            transform.position = new Vector3(((x * rangeX) - ofsetX), ((y * rangeY) - ofsetY), 0);
        }
        if (Func4)
        {
            t += Time.deltaTime;
            x = (Mathf.Cos(speedX * t));
            y = (Mathf.Sin(speedY * t));
            y *=  y-1;
            x *= x-1;
            transform.position = new Vector3(((x * rangeX) - ofsetX), ((y * rangeY) - ofsetY), 0);
        }
        if (Func5)
        {
            t += Time.deltaTime;
            x = (Mathf.Cos(speedX * t));
            y = (Mathf.Sin(speedY * t));
            y *= y ;
            y = 1 - y;
            x *= x;
            x = 1 - x;
            transform.position = new Vector3(((x * rangeX) - ofsetX), ((y * rangeY) - ofsetY), 0);
        }       
        if (Func6)
        {
            t += Time.deltaTime;
            y = (Mathf.Cos(speedX * t));
            x = (Mathf.Cos(speedY * t));
            // y *= y;
            //x *= x;
            transform.position = new Vector3(((x * rangeX)- ofsetX), ((y * rangeY)- ofsetY), 0);
        }
    }
}
