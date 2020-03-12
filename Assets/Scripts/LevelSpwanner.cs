using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpwanner : MonoBehaviour
{
    //Referenced in ChangeBallLayer
    public static int levelCt;

    public GameObject[] levels;

    GameObject currPlat;
    int nowLevel;

    //Used to increment te nowLevel only once
    bool onlyOnce;
    
    // Start is called before the first frame update
    void Start()
    {
        onlyOnce = false;
        //PlayerPrefs.SetInt("Level", 7);
        levelCt = PlayerPrefs.GetInt("Level");

        nowLevel = levelCt / 2;
        Debug.Log(nowLevel);
        //This if is for random level generation on all level are completed
        if (levelCt == levels.Length)
        {
            levelCt = Random.Range(0, levels.Length+1);
            nowLevel = levelCt / 2;
        }

        currPlat = Instantiate(levels[nowLevel], transform.position, Quaternion.identity);
        currPlat.transform.SetParent(GameObject.Find("LevelSpwan").transform);

        //here ChangeBallLayer.cs script is referenced
        ChangeBallLayer.countOfObjs = currPlat.transform.GetChild(0).transform.GetChild(0).childCount;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MoveHole.nextLevel &&!onlyOnce)
        {
            levelCt++;

            //here ChangeBallLayer.cs script is referenced
            ChangeBallLayer.countOfObjs = currPlat.transform.GetChild(1).transform.GetChild(0).childCount;
            onlyOnce = true;
        }
    }
}
