using UnityEngine;
using System.Collections;

public class GuardSystem : MonoBehaviour {
    private PlayerMove playerMove;
    private FlagsInStageManager flagsInStageManager;
    private GameObject guardEffect;
	// Use this for initialization
	void Start () {
        flagsInStageManager = GameObject.Find("GameControlObject").GetComponent<FlagsInStageManager>();
        playerMove = GameObject.Find("player").GetComponent<PlayerMove>();
        guardEffect = gameObject.transform.FindChild("GuardEffect").gameObject;
    }
	
	// Update is called once per frame
	void Update () {
	if(playerMove.guardFlag == true)
        {
            guardEffect.SetActive(true);
        }
    else
        {
            guardEffect.SetActive(false);
        }
	}

}
