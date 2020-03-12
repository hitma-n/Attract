using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollow : MonoBehaviour
{
   
    //This script is used for the character which is not used in the game

    public Transform[] points;
    public float moveTime;

    int currIndex;
    int ptsCount;
    Vector3 pos;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        currIndex = 0;
        ptsCount = points.Length;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != points[currIndex].position)
        {
            pos = Vector3.MoveTowards(transform.position, points[currIndex].position, moveTime * Time.deltaTime);
            transform.LookAt(pos);
            rb.MovePosition(pos);
            
        }
        else
        {
            currIndex = (currIndex + 1) % ptsCount;
        }
    }
}
