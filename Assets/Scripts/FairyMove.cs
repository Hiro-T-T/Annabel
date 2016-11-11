using UnityEngine;
using System.Collections;

public class FairyMove : MonoBehaviour {
    Vector3 playerForward = new Vector3(0.0f, 0.0f, 0.0f);
    Vector3 playerRight = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 playerDistance = new Vector3(-1.5f, 0.0f, 1.5f);
    Vector3 fairyMoveEndPos = new Vector3(0.0f, 0.0f, 0.0f);
    Vector3 fairyMovePos = new Vector3(0.0f, 0.0f, 0.0f);
    GameObject player;
    PlayerMove playerMove;
    FlagsInStageManager flagsInStageManager;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMove = player.GetComponent<PlayerMove>();
        flagsInStageManager = GameObject.Find("GameControlObject").GetComponent<FlagsInStageManager>();
        transform.position = player.transform.position - playerDistance;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        //カメラダミー正面方向取得
        playerForward = player.transform.TransformDirection(Vector3.forward);
        //カメラダミー横方向取得
        playerRight = player.transform.TransformDirection(Vector3.right);

        transform.rotation = Quaternion.RotateTowards(player.transform.rotation, playerMove.q, 20.0f);

        fairyMoveEndPos = player.transform.position;

       fairyMoveEndPos += playerDistance;

        fairyMovePos += (fairyMoveEndPos - fairyMovePos) * 0.25f; 
        transform.position = fairyMovePos;
    }
}
