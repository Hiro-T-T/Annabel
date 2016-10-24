using UnityEngine;
using System.Collections;

public class BlueDoorScript : MonoBehaviour {
    public BlueImg bmg;
	// Use this for initialization
	void Start () {
        bmg = GameObject.Find("BlueDoor").GetComponent<BlueImg>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(bmg.blDst == true)
        {
            Destroy(gameObject);
        }
	}
}
