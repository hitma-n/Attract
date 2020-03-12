using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFromStatictoDynamicChildren : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.transform.GetComponent<ChangeFromStatictoDynamicParent>().statictodynamic)
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }


}
