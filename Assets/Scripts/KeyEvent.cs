using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KeyEvent : MonoBehaviour {

    public DoorController redDoor;

	// Use this for initialization
	void Start () {
        redDoor = GameObject.Find("RedDoor").GetComponent<DoorController>();
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
            redDoor.open = true;
            Debug.Log("open");
            Destroy(gameObject);
        }
    }
}
