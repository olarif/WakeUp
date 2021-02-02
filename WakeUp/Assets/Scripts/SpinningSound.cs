using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningSound : MonoBehaviour
{

    FMOD.Studio.EventInstance Rotation;
    string path = "event:/TurningGears";
    // Start is called before the first frame update
    void Start()
    {
        Rotation = FMODUnity.RuntimeManager.CreateInstance(path);
        Rotation.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
        Rotation.start();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //RotationSound();

    }
    void RotationSound()
    {

        // DoubleGear.setParameterByName("Material", Material);
        
        
    }

}
