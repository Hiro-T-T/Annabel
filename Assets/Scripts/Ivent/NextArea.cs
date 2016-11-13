using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NextArea : MonoBehaviour {

    private int stg = 0;
    int changeSceneNumber = 0;

    public enum NextScene
    {
        stage1,
        stage2,
        stage3
    }

    public NextScene nxtscene;

	// Use this for initialization
	void Start () {
        switch (nxtscene)
        {
            case NextScene.stage1:
                stg = 2;
                changeSceneNumber = 4;
                break;
            case NextScene.stage2:
                stg = 2;
                changeSceneNumber = 5;
                break;
            case NextScene.stage3:
                stg = 4;
                break;
        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            GameManager.sceneNumber = changeSceneNumber;
            SceneManager.LoadScene(stg);
        }
    }
}
