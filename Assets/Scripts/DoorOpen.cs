using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{

    //Called by ChangeBallLayer script
    public bool moveDoor;

    public Transform door;

    // Start is called before the first frame update
    void Start()
    {
        moveDoor = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if (moveDoor)
        {
            door.position = Vector3.Lerp(door.position,new Vector3(door.position.x,
                -0.48f,door.position.z),2*Time.deltaTime);
        }
    }
}
