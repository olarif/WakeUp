using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningSound : MonoBehaviour
{
    public float MinDistance;
    public Transform Player;
    private float scale;
    private float xPosMax, xPosMin, yPosMax, yPosMin;
    private Vector3 audioPos;
    public GameObject gear;

    FMOD.Studio.EventInstance Rotation;
    string path = "eevent:/SpinningGears";
    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PlayerMovement>().transform;
        scale = transform.localScale.x;
        xPosMin = transform.position.x - (scale);
        xPosMax= transform.position.x + (scale);
        yPosMin = transform.position.y - (scale);
        yPosMax= transform.position.y + (scale);

        Rotation = FMODUnity.RuntimeManager.CreateInstance(path);
        
       // Rotation.setProperty(FMOD.Studio.EVENT_PROPERTY.MAXIMUM_DISTANCE, scale*MinDistance);
        Rotation.start();
    }

    // Update is called once per frame
    void Update()
    {
        float xPos = Mathf.Clamp(Player.position.x, xPosMin, xPosMax);
        float yPos = Mathf.Clamp(Player.position.y, yPosMin, yPosMax);
        audioPos = new Vector3(xPos, yPos, transform.position.z);
        Rotation.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(audioPos));

    }
    void RotationSound()
    {

        // DoubleGear.setParameterByName("Material", Material);
        
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(audioPos, 0.25f);
    }
    private void OnDisable()
    {
        //Rotation.release();
    }

}
