using UnityEngine;
using System.Collections;

public class KeyEvent : MonoBehaviour {

    private GameManager gm;

	// Use this for initialization
	void Start () {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            gm.door = true;
            Destroy(gameObject);
        }
    }
}
