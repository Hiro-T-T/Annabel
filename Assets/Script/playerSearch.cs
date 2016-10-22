using UnityEngine;
using System.Collections;

public class playerSearch : MonoBehaviour {

    public GameObject player;
    public Vector3 playerPos;
    public Vector3 pos;
    public float dist;
    private float distX;
    private float distZ;
	// Use this for initialization
	void Start () {
       // player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
	
        playerPos.x = player.transform.position.x;
        playerPos.z = player.transform.position.z;
        pos.x = gameObject.transform.position.x;
        pos.z = gameObject.transform.position.z;
        distX = Mathf.Abs(playerPos.x - pos.x);
        distZ = Mathf.Abs(playerPos.z - pos.z);
        dist = Mathf.Sqrt((distX * distX) + (distZ * distZ));
        Debug.Log(dist);
        
	}
}
