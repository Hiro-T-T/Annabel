using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KeyEvent : MonoBehaviour {

    private GameManager gm;
    public RedImg redImg;

	// Use this for initialization
	void Start () {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        redImg = GameObject.Find("RedDoor").GetComponent<RedImg>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ImgOff()
    {

    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            redImg.ImgOn = true;
            gm.door = true;
            Destroy(gameObject);
        }
    }
}
