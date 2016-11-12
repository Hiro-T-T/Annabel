using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public int battleCount = 0;
    public int stateCount = 0;
    public int doorImgCount = 0;
    public int hintNum = 0;
    public static int sceneNumber = 0;
    public static bool clear = false;
    public int tutorialState = 0;

    public static int getSceneNumber()
    {
        return sceneNumber;
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}
}
