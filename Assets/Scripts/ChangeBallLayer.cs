using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeBallLayer : MonoBehaviour
{
    #region var Declaration
    //Called from LevelSpwanner;
    public static float countOfObjs;

    //The following variable is used to move platform backwards
    //This variable is also referenced in CameraScript
    public static bool movePlatform;

    public Transform levelSp;

    //This is used for the progress bar
    public Image ldBar;

    public int LayerOnEnter; // BallInHole
    public int LayerOnExit;  // BallOnTable
    public GameObject particlesObj;

    Rigidbody otherRb;
    float currCt;
    Vector3 oldPos;
    Transform Hole;
    Vector3 lastFrame;

    //Set holeFreez of MoveHole to false only once
    bool onlyOnce;

    #endregion

    void Start()
    {
        onlyOnce = false;
        currCt = 0;
        Hole = transform.parent;
        lastFrame = Hole.position;
    }

    void Update()
    {
        if (MoveHole.holeFreez)
        {
            //Hole is moved
            StartCoroutine(moveHole());
        }

        //This if condition is responsible for moving the ball to next level
        if (movePlatform)
        {
            Hole.position = 
                Vector3.MoveTowards(Hole.position, new Vector3(-12.6f, Hole.position.y, Hole.position.z),5*Time.deltaTime);
        }

        //if hole reached to next level and hole is freezed then unfreez the hole
        if (Hole.transform.position.x == -12.6f && MoveHole.holeFreez && !onlyOnce)
        {
            MoveHole.holeFreez = false;
            MoveHole.nextLevel = true;

            //The trigger of hole is now on
            Hole.transform.GetChild(2).transform.GetComponent<SphereCollider>()
                .enabled = true;

            movePlatform = false;

            //Current count of objs is made 0;
            currCt = 0;

            onlyOnce = true;
        }

        if (Hole.position.z == 0.14f && MoveHole.holeFreez)
        {
            movePlatform = true;
        }
        else
        {
            lastFrame = Hole.position;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);
            ++currCt;
            ldBar.fillAmount = currCt / countOfObjs;
            otherRb = other.gameObject.GetComponent<Rigidbody>();
            otherRb.isKinematic = false;
            other.gameObject.layer = LayerOnEnter;
            
  
            Debug.Log(currCt);
            //Here particles are played
            if (currCt == countOfObjs && MoveHole.nextLevel)
            {
                particlesObj.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                particlesObj.transform.GetChild(1).GetComponent<ParticleSystem>().Play();

                StartCoroutine(loadAgain());

            }
            else if (currCt == countOfObjs)
            {

                //Hole is freezed so that user cannot interfere
                MoveHole.holeFreez = true;

                //The trigger of hole is offed for certain time
                Hole.transform.GetChild(2).transform.GetComponent<SphereCollider>()
                    .enabled = false;

                //Door is now opened
                levelSp.transform.GetChild(0).GetComponent<DoorOpen>().moveDoor = true;

                //The Progress Meter is again made to zero
                ldBar.fillAmount = 0;

            }
            
            
        }
        //You Loose
        else if (other.gameObject.tag == "Enemy")
        {
            otherRb = other.gameObject.GetComponent<Rigidbody>();
            otherRb.isKinematic = false;

            //Here gameover is true for movehole
            MoveHole.isGameOver = true;
            other.gameObject.layer = LayerOnEnter;
            StartCoroutine(shakeCamera());
            Destroy(other.gameObject,0.5f);
            StartCoroutine(loadOnFail());

        }
    }

    //This function is responsible for moving the hole to middle
    IEnumerator moveHole()
    {
        yield return new WaitForSeconds(0.5f);
        oldPos = Hole.position;

        Hole.position = Vector3.MoveTowards(Hole.position, new Vector3(oldPos.x,
              oldPos.y, 0.14f), 2 * Time.deltaTime);
    }

    IEnumerator shakeCamera()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject.Find("Main Camera").GetComponent<CameraShake>().enabled = true;
        CameraShake.shakeDuration = 0.5f;
    }


    IEnumerator loadOnFail()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("SampleScene");
    }

    IEnumerator loadAgain()
    {
        yield return new WaitForSeconds(1.5f);
        PlayerPrefs.SetInt("Level",++LevelSpwanner.levelCt);
        SceneManager.LoadScene("SampleScene");
    }

}
