using UnityEngine;
using System.Collections;

public class KeyController : MonoBehaviour {

    bool treOn = false;
    GameManager gm;
    int treasureNumber = 0;
    int stateNumber = 0;

    public enum TreasureState
    {
        Stage1_right,
        Stage1_left,
        Stage1_rock
    }

    public TreasureState treasureState;

    void Start()
    {
        switch (treasureState)
        {
            case TreasureState.Stage1_right:
                stateNumber = 1;
                treasureNumber = 1;
                break;
            case TreasureState.Stage1_left:
                stateNumber = 2;
                treasureNumber = 2;
                break;
            case TreasureState.Stage1_rock:
                stateNumber = 2;
                treasureNumber = 3;
                break;
        }
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        treOn = false;

    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Player") && Input.GetAxis("Attack") == 1 && treOn == false && gm.battleCount == stateNumber)
        {
            treOn = true;
            this.GetComponent<Animator>().SetTrigger("onTrigger");
            Invoke("NumberChange", 1F);
        }
    }

    void NumberChange()
    {
        gm.stateCount = treasureNumber;
    }
}
