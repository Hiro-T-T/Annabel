using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KeyImageController : MonoBehaviour {

    public DoorController door;
    GameManager gm;
    string doorObj = "";
    public bool ImgOn = true;
    int stage = 0;
    int doorCount = 0;

    public enum DoorState
    {
        Right,
        Left,
        Rock
    }

    public DoorState doorState;

    void Start()
    {
        switch (doorState)
        {
            case DoorState.Right:
                stage = 1;
                doorCount = 1;
                break;
            case DoorState.Left:
                stage = 1;
                doorCount = 2;
                break;
            case DoorState.Rock:
                stage = 1;
                doorCount = 3;
                break;
        }
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        ImgOn = true;
        gameObject.GetComponent<Image>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (gm.stateCount == doorCount && ImgOn == true && stage == 1)
        {
            gameObject.GetComponent<Image>().enabled = true;
            
            switch (doorCount)
            {
                case 1:
                    gm.hintNum = 1;
                    break;
                case 2:
                    gm.hintNum = 3;
                    break;
                case 3:
                    gm.hintNum = 4;
                    break;
            }
                
            Invoke("ImgOff", 1.0F);
        }
    }

    public void ImgOff()
    {
        ImgOn = false;
        gameObject.GetComponent<Image>().enabled = false;

    }
}
