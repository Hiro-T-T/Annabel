using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TreasureEvent : MonoBehaviour {

    bool treOn = false;

    public enum TreasureState
    {
        Tutorial,
        Stage1,
        Stage2,
        Stage3
    }

    public TreasureState treasureState;

    void Start()
    {
        treOn = false;
        
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Player") && Input.GetAxis("Attack") == 1 && treOn == false)
        {
            treOn = true;
            switch (treasureState)
            {
                case TreasureState.Tutorial:
                    DoorController blueDoor = GameObject.Find("BlueDoor").GetComponent<DoorController>();
                    blueDoor.open = true;
                    break;
                case TreasureState.Stage1:
                    GameManager gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
                    gm.stateCount = 3;
                    break;
            }
            this.GetComponent<Animator>().SetTrigger("onTrigger");
        }
    }
}
