using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {

    public int doorCount = 0;
    public int stage = 0;
    public bool open = false;

    GameManager gm;

    public enum DoorType
    {
        Tutorial_Red,
        Tutorial_Blue,
        Stage1_Right,
        Stage1_Left,
        Stage1_Rock,
        Stage1_Blue
    }

    public DoorType doorType;
    
	void Start () {
        doorCount = 0;
        open = false;
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        switch (doorType)
        {
            // ドアの種類とステージの判別
            case DoorType.Tutorial_Red:
                doorCount = 1;
                stage = 0;
                break;
            case DoorType.Tutorial_Blue:
                doorCount = 2;
                stage = 0;
                break;
            case DoorType.Stage1_Right:
                doorCount = 1;
                stage = 1;
                break;
            case DoorType.Stage1_Left:
                doorCount = 2;
                stage = 1;
                break;
            case DoorType.Stage1_Rock:
                doorCount = 3;
                stage = 1;
                break;
            case DoorType.Stage1_Blue:
                doorCount = 4;
                stage = 1;
                break;
        }
	}

    void Update() {
        Debug.Log(open);
        // ステージごとに処理分け
        switch (stage)
        {
            case 0:
                if (open == true)
                {
                    gm.tutorialState = doorCount;
                    Debug.Log("Des");
                    Destroy(this.gameObject);
                }
                break;
        }
	}

    void OnTriggerStay(Collider col)
    {
        switch (stage)
        {
            case 1:
                if (col.gameObject.CompareTag("Player") && Input.GetAxis("Attack") == 1 && gm.stateCount == doorCount)
                {
                    if(doorCount == 1)
                    {
                        gm.hintNum = 2;
                    }
                    switch (doorCount)
                    {
                        case 1:
                            gm.hintNum = 2;
                            break;
                        case 3:
                            gm.hintNum = 5;
                            break;
                    }
                    gm.doorImgCount = doorCount;
                    Instantiate(Resources.Load("CounterMagic1"), new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
                    Destroy(gameObject);
                }
                break;
        }
    }

}