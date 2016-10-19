using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {

    public Material[] materialCol;
    public int colNum = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {



        this.GetComponent<Renderer>().material = materialCol[colNum];
	}
}
