using UnityEngine;
using System.Collections;

public class CanvasTargetControl : MonoBehaviour {
    FlagsInStageManager flagsInStageManager;
    GameObject canvasTarget;
    // Use this for initialization
    void Start () {
        flagsInStageManager = GameObject.Find("GameControlObject").GetComponent<FlagsInStageManager>();
        canvasTarget = Resources.Load("CanvasTarget") as GameObject;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

   void canvasTargetAppear()
    {
        Instantiate(canvasTarget);
    }
}
