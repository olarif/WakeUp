using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FmodPlayer : MonoBehaviour
{
    private float distance = 1f;
    private float Material;


    // Update is called once per frame
    void FixedUpdate()
    {
        MaterialCheck();
        Debug.DrawRay(transform.position, Vector3.down * distance, Color.blue);
        
    }
    void MaterialCheck()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, Vector3.down, distance, 1 << 6);
        if (hit.collider)
         {
            if (hit.collider.CompareTag("Floor"))
            {
                Material = 0f;
            }
            else if (hit.collider.CompareTag("Gear"))
            {
                Material = 1f;
            }
            else Material = 0f;
         }
    }

    void PlayFootstepsEvent(string path)
    {
        FMOD.Studio.EventInstance Footsteps = FMODUnity.RuntimeManager.CreateInstance(path);
        Footsteps.setParameterByName("Material", Material);
        Footsteps.start();
        Footsteps.release();
    }
}
