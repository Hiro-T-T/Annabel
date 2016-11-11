using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HpScript : MonoBehaviour {

    public Text txt;
    PlayerMove playerMove;

	// Use this for initialization
	void Start () {
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
	}
	
	// Update is called once per frame
	void Update () {
        txt.text = playerMove.hp.ToString();
	}
}
