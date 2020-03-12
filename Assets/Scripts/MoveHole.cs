using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is responsible for moving the hole according to the mouse delta position

public class MoveHole : MonoBehaviour
{
    //Referenced by ChangeBallLayer
    public static bool isGameOver;

    //Referenced in ChangeBallLayer.cs script
    public static bool holeFreez;

    //Referenced in LevelSpwanner.cs Script
    public static bool nextLevel;
    public Transform hole;
    public float speed;
    public float mobileTouchSpeed;
    public GameObject stGamePanel;


    bool movePlayer;
    float bottomX, topX, leftZ, rightZ;



    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        holeFreez = false;
        nextLevel = false;

        bottomX = -3.370001f;
        topX = 4.09f;
        leftZ = -1.74f;
        rightZ = 1.98f;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0) && Input.mousePresent && !isGameOver && !holeFreez)
        {
            stGamePanel.SetActive(false);
            movePlayer = true;
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && !isGameOver && !holeFreez)
        {

            stGamePanel.SetActive(false);

            // Get movement of the finger since last frame
            float screenSize = Mathf.Max(Screen.width, Screen.height);
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            Vector2 touchDeltaPercentage = touchDeltaPosition / screenSize;
            Vector3 currPos = hole.position;
            Vector3 newPos = hole.position + new Vector3(-touchDeltaPercentage.y * mobileTouchSpeed,
                0, touchDeltaPercentage.x * mobileTouchSpeed);

            newPos.x = Mathf.Clamp(newPos.x, bottomX, topX);
            newPos.z = Mathf.Clamp(newPos.z, leftZ, rightZ);

            hole.GetComponent<Rigidbody>().MovePosition(newPos);

        }
        else
            movePlayer = false;


        if (nextLevel)
        {
            leftZ = -1.90f;
            rightZ = 1.84f;
            bottomX = -20.08f;
            topX = -12.5f;
        }

    }

    void FixedUpdate()
    {
        if (movePlayer)
        {
            Vector2 delta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            Vector3 newPos = hole.position + new Vector3(-delta.y * speed ,
                0,delta.x * speed);

            newPos.x = Mathf.Clamp(newPos.x, bottomX, topX);
            newPos.z = Mathf.Clamp(newPos.z, leftZ, rightZ);


            hole.GetComponent<Rigidbody>().MovePosition(newPos);
        }

        

    }

}
