using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drewsCamera : MonoBehaviour
{
    public Vector3 offset;
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3 (offset.x, player.position.y + offset.y, offset.z);
    }
}
