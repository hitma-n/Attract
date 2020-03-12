using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFromStatictoDynamicFirstChildren : MonoBehaviour
{

    public GameObject attractedTo;
    Vector3 offset;
    float magsqr;

    // Update is called once per frame
    void Update()
    {
        offset = attractedTo.transform.position - transform.position;
        magsqr = offset.sqrMagnitude;

        if (magsqr < 0.5f && transform.gameObject.layer != 10 && !MoveHole.holeFreez)
        {
            
            transform.parent.transform.GetComponent<ChangeFromStatictoDynamicParent>().statictodynamic
                = true;
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
