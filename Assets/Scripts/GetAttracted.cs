using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is responsible for attracting the object towards itself

public class GetAttracted : MonoBehaviour
{
    #region var Declaration
    GameObject attractedTo;

    Vector3 offset;
    float magsqr;
    float forceMagnitude;
    Rigidbody rb;
    #endregion

    void Start()
    {
        attractedTo = GameObject.Find("Hole");
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        offset = attractedTo.transform.position - transform.position;
        magsqr = offset.sqrMagnitude;


        //Attraction is also dependent on holeFreez of MoveHole.cs script
        if (magsqr < 0.75f && transform.gameObject.layer != 10 && !MoveHole.holeFreez)
        {

            forceMagnitude = 66.74f * (attractedTo.GetComponent<Rigidbody>().mass * rb.mass)
                                 / Mathf.Pow(1.5f, 2);
            rb.AddForce(offset.normalized * forceMagnitude);

            //rb.AddForceAtPosition(offset, offset.normalized * forceMagnitude);

        }
        if(transform.gameObject.layer == 10)
        {
            rb.velocity = Vector3.zero;
            rb.velocity = new Vector3(0, -9.41f, 0);
        }

    }


}
