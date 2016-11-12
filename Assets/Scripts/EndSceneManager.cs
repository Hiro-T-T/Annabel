using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour {
    TalkSettings talkSet;
	// Use this for initialization
	void Start () {
        GameObject.FindGameObjectWithTag("EndImg").GetComponent<Image>().enabled = false;
        talkSet = GameObject.Find("GameControlObject").GetComponent<TalkSettings>();
        GameManager.clear = true;
	}
	
	// Update is called once per frame
	void Update () {
	    if(talkSet.count == 1)
        {
            GameObject.FindGameObjectWithTag("EndImg").GetComponent<Image>().enabled = true;
            Invoke("MoveScene", 3.0F);
        }
	}

    void MoveScene()
    {
        GameManager.sceneNumber = 0;
        SceneManager.LoadScene(2);
    }
}
