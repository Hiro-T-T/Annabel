using UnityEngine;
using System.Collections;

public class BattleCameraControl : MonoBehaviour {
    FlagsInStageManager flagsInStageManager;
    MainCameraControl mainCameraControl;
    // Use this for initialization
    void Start () {
        flagsInStageManager = GameObject.Find("GameControlObject").GetComponent<FlagsInStageManager>();
        mainCameraControl = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainCameraControl>();
    }
	
	// Update is called once per frame
	void Update () {
	if(flagsInStageManager.batleMode == true)
        {
            
        }
    else
        {
            transform.position = mainCameraControl.cameraPos;
            transform.localEulerAngles = mainCameraControl.CameraRotate;

        }
	}
}
