using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public int counter = 0;
	// Use this for initialization
	void Start () {
        counter = 0;
	}
	
	// Update is called once per frame
	void Update () {
        counter++;
	}
}
