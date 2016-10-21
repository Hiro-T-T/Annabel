using UnityEngine;
using System.Collections;

public class DoorEvent : MonoBehaviour {

    private GameManager gm;

	// Use this for initialization
	void Start () {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(gm.door == true)
        {
            Destroy(gameObject);
        }
	}
}
