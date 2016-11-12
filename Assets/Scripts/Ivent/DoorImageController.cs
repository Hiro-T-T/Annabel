using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DoorImageController : MonoBehaviour {

    public DoorController door;
    string doorObj = "";
    public bool ImgOn = true;

    public enum DoorState
    {
        Red,
        Blue,
    }

    public DoorState doorState;

    void Start()
    {
        switch (doorState)
        {
            case DoorState.Red:
                doorObj = "RedDoor";
                break;
            case DoorState.Blue:
                doorObj = "BlueDoor";
                break;
        }

        door = GameObject.Find(doorObj).GetComponent<DoorController>();
        ImgOn = true;
        gameObject.GetComponent<Image>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (door.open == true && ImgOn == true)
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
