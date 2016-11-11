using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {

    public int doorCount = 0;
    public bool open = false;

    GameManager gm;

    public enum DoorType
    {
        Tutorial_Red,
        Tutorial_Blue,
        Stage1_1,
        Stage1_2,
        Stage1_3
    }

    public DoorType doorType;
    
	void Start () {
        doorCount = 0;
        open = false;
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        switch (doorType)
        {
            case DoorType.Tutorial_Red:
                doorCount = 1;
                break;
            case DoorType.Tutorial_Blue:
                doorCount = 2;
                break;
            
        }
	}
	
	void Update () {
        Debug.Log(open);
        if(open == true)
        {
            gm.stateCount = doorCount;
            Debug.Log("Des");
            Destroy(this.gameObject);
        }
	}

}