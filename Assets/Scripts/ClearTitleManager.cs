using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClearTitleManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.gameObject.GetComponent<Image>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	    if(GameManager.clear == true)
        {
            this.gameObject.GetComponent<Image>().enabled = true;

        }
    }
}
