using UnityEngine;
using System.Collections;

public class FieldAndBattleBGMControl : MonoBehaviour {
    AudioSource fieldBGM;
    AudioSource battleBGM;
    FlagsInStageManager flagsInStageManager;
    // Use this for initialization
    void Start () {
        fieldBGM = GameObject.Find("FieldBGM").GetComponent<AudioSource>();
        battleBGM = GameObject.Find("BattleBGM").GetComponent<AudioSource>();
        flagsInStageManager = GameObject.Find("GameControlObject").GetComponent<FlagsInStageManager>();
       
    }
	
	// Update is called once per frame
	void Update () {

            if(flagsInStageManager.batleMode == false)
            {
            battleBGM.volume = 0.0f;
            fieldBGM.enabled = true;
            battleBGM.enabled = false;
            
            if(fieldBGM.volume <= 1.0f)
            {
                fieldBGM.volume += 0.01f;
            }
            }
            else
            {
            fieldBGM.volume = 0.0f;
            fieldBGM.enabled = false;
            battleBGM.enabled = true;
           
            if (battleBGM.volume <= 1.0f)
            {
                battleBGM.volume += 0.01f;
            }
        }
       
    }
}
