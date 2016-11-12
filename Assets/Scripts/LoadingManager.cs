using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour {
    GameManager gm;
    int sceneNumber = 0;
    public float moveSpeed = 3.0F;
    // Use this for initialization
    void Start () {
        sceneNumber = GameManager.getSceneNumber();
        Invoke("SceneMove", moveSpeed);
	}
	
    void SceneMove()
    {
        SceneManager.LoadScene(sceneNumber);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
