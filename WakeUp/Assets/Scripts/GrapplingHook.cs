using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{

    public LineRenderer line;
    public LayerMask mask;
    SpringJoint2D joint;
    Vector3 targetPos;
    RaycastHit2D hit;
    public float maxDistance = 90f;
    Vector2 lookDirection;
    bool checker = true;
    
    public float step = 0.02f;

    void Start()
    {
        joint = GetComponent<SpringJoint2D>();
        joint.enabled = false;
        line.enabled = false;
    }


    void LateUpdate()
    {
        line.SetPosition(0, transform.position);

        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;


        if (Input.GetMouseButtonDown(0) && checker == true)    
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, lookDirection, maxDistance, mask);

            if (hit.collider != null)
            {
                checker = false;
                SetRope(hit);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            checker = true;
            DestroyRope();
        }
    }

    void SetRope(RaycastHit2D hit)
    {
        joint.enabled = true;
        joint.connectedAnchor = hit.point;

        line.enabled = true;
        line.SetPosition(1, hit.point);
    }

    void DestroyRope()
    {
        joint.enabled = false;
        line.enabled = false;
    }
}
