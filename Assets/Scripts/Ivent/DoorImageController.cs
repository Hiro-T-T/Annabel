using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DoorImageController : MonoBehaviour {

    public DoorController door;
    GameManager gm;
    string doorObj = "";
    public bool ImgOn = true;
    int stage = 0;
    int doorCount;

    public enum DoorState
    {
        Red,
        Blue,
        Right,
        Left,
        Stage1_Blue
    }

    public DoorState doorState;

    void Start()
    {
        switch (doorState)
        {
            case DoorState.Red:
                doorObj = "RedDoor";
                stage = 0;
                break;
            case DoorState.Blue:
                doorObj = "BlueDoor";
                stage = 0;
                break;
            case DoorState.Right:
                doorObj = "RightDoor";
                stage = 1;
                doorCount = 1;
                break;
            case DoorState.Left:
                doorObj = "LeftDoor";
                stage = 1;
                doorCount = 2;
                break;
            case DoorState.Stage1_Blue:
                doorObj = "BlueDoor";
                stage = 1;
                doorCount = 4;
                break;
        }
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        door = GameObject.Find(doorObj).GetComponent<DoorController>();
        ImgOn = true;
        gameObject.GetComponent<Image>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (door.open == true && ImgOn == true && stage == 0)
        {
            gameObject.GetComponent<Image>().enabled = true;
            Invoke("ImgOff", 1.0F);
        }
        if (gm.stateCount == doorCount && ImgOn == true && stage == 1 && gm.doorImgCount == doorCount)
        {
            gameObject.GetComponent<Image>().enabled = true;
            Invoke("ImgOff", 1.0F);
        }
    }

    public void ImgOff()
    {
        ImgOn = false;
        gameObject.GetComponent<Image>().enabled = false;
        
    }
}
