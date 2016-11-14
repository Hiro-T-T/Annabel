using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour {
    TalkSettings talkSet;
    FlagsInStageManager flagsInStageManager;
    float count;
	// Use this for initialization
	void Start () {
        GameObject.FindGameObjectWithTag("EndImg").GetComponent<Image>().enabled = false;
        talkSet = GameObject.Find("GameControlObject").GetComponent<TalkSettings>();
        flagsInStageManager = GameObject.Find("GameControlObject").GetComponent<FlagsInStageManager>();
        GameManager.clear = true;
	}
	
	// Update is called once per frame
	void Update () {
        if(flagsInStageManager.talkMode != 0)
        {
            count += Time.deltaTime;
        }
      
	    if(count > 1)
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
