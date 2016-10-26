using UnityEngine;
using System.Collections;

public class EnemyTargetPosMove : MonoBehaviour {
    PlayerMove player;
    FlagsInStageManager flagsInStageManager;
    // Use this for initialization
    void Start () {
        player = GameObject.Find("player").GetComponent<PlayerMove>();
        flagsInStageManager = GameObject.Find("GameControlObject").GetComponent<FlagsInStageManager>();
    }

    // Update is called once per frame
    Vector3 movePosEnd = new Vector3(0.0f, 0.0f, 0.0f);
    void FixedUpdate()
    {

        if (flagsInStageManager.batleMode == false)
        {
            movePosEnd = player.transform.position;
            transform.position = movePosEnd;

        }
        else if(flagsInStageManager.batleMode == true)
        {
            Vector3 playerPos = player.transform.position;
            Vector3 enemyPos = player.targetEnemyPosition.transform.position;
            
            movePosEnd += (enemyPos - movePosEnd) * 0.02f;
            Vector3 movePos;
            movePos = movePosEnd;
            transform.position = movePos;
        }
    }
	   
	
}
