using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParralaxDonkey : MonoBehaviour
{
    public Transform player;
    public Vector3 origin;
    public float multiplier;



    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3((player.position.x - origin.x)*multiplier + origin.x, (player.position.y - origin.y) * multiplier + origin.y, origin.z);
    }
}
