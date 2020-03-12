using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform hole;
    Vector3 offset;
    public float speed;

    void Start()
    {
        offset = transform.position - hole.position;
    }

    void FixedUpdate()
    {
        //Variable declared in ChangeBallLayer
        if (ChangeBallLayer.movePlatform)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(hole.position.x + offset.x,
                hole.position.y+offset.y,transform.position.z), speed * Time.deltaTime);
        }
    }
}
