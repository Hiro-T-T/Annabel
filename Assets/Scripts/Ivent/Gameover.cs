using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameManager.sceneNumber = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKey)
        {
            SceneManager.LoadScene(2);
        }
	}
}
