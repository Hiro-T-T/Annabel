using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NextArea : MonoBehaviour {

    private int stg;

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
                break;
            case NextScene.stage2:
                stg = 3;
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
            SceneManager.LoadScene(stg);
        }
    }
}
