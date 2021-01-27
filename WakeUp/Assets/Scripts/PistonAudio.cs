using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonAudio : MonoBehaviour
{
    private Vector3 pos, oldpos;
    private FMOD.Studio.EventInstance pistonLoop;
    private FMOD.Studio.PLAYBACK_STATE pbState;

    // Start is called before the first frame update
    void Start()
    {
        pistonLoop = FMODUnity.RuntimeManager.CreateInstance("event:/LaunchingPiston");
        
        oldpos = transform.position;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        pistonLoop.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform, GetComponent<Rigidbody2D>()));
        pistonLoop.getPlaybackState(out pbState);
        pos = transform.position;
        if(pos!=oldpos)
        {
            if(pbState!=FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                pistonLoop.start();
            }
        }
        else if(pos == oldpos&&pbState== FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            pistonLoop.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        oldpos = pos;
    }
     void OnDestroy()
    {
        pistonLoop.release();
    }
}
