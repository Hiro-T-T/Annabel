using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TreasureEvent : MonoBehaviour {

    public DoorController blueDoor;
    private bool treOn = false;

    void Start()
    {
        treOn = false;
        blueDoor = GameObject.Find("BlueDoor").GetComponent<DoorController>(); 
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Player") && Input.GetAxis("Attack") == 1 && treOn == false)
        {
            blueDoor.open = true;
            treOn = true;
            this.GetComponent<Animator>().SetTrigger("onTrigger");
        }
    }
}
