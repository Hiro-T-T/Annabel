using UnityEngine;
using System.Collections;

public class BattleWall : MonoBehaviour {
    FlagsInStageManager flagsInStageManager;
    BoxCollider boxCollider;
	// Use this for initialization
	void Start () {
        boxCollider = gameObject.GetComponent<BoxCollider>();
        flagsInStageManager = GameObject.Find("GameControlObject").GetComponent<FlagsInStageManager>();
    }
	
	// Update is called once per frame
	void Update () {
	if(flagsInStageManager.batleMode == false)
        {
            boxCollider.isTrigger = true;
        }
    else
        {
            boxCollider.isTrigger = false;
        }
	}
}
