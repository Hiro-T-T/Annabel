using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HintController : MonoBehaviour {

    public Text hintTxt;
    GameManager gm;
    int hintNumber = 0;

    public enum StageType
    {
        Tutorial,
        Stage1,
        Stage2,
        Stage3
    }

    public StageType stageType;

	// Use this for initialization
	void Start () {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update () {
        
        switch (stageType)
        {
            case StageType.Tutorial:
                hintNumber = gm.stateCount;
                // hintNumber = 1;
                TutorialHint();
                break;
            case StageType.Stage1:
                hintNumber = gm.hintNum;
                // hintNumber = 2;
                Stage1Hint();
                break;
            case StageType.Stage2:
                hintNumber = 3;
                break;
            case StageType.Stage3:
                hintNumber = 4;
                break;
        }

        /*switch (hintNumber)
        {
            case hintNumber = 1:
                TutorialHint();
                break;
            case hintNumber = 2:
                Stage1Hint();
                break;
            case hintNumber = 3:

        }*/
    }

    void TutorialHint()
    {
        switch (hintNumber)
        {
            case 0:
                hintTxt.text = "まずは黄色い鍵を探そう";
                break;
            case 1:
                hintTxt.text = "宝箱を見つけよう";
                break;
            case 2:
                hintTxt.text = "オーラに触れて次のステージに進もう";
                break;
        }

    }

    void Stage1Hint()
    {
        switch(hintNumber)
        {
            case 0:
                hintTxt.text = "まずは光の鍵を探そう";
                break;
            case 1:
                hintTxt.text = "扉を開けよう";
                break;
            case 2:
                hintTxt.text = "闇の鍵を探そう";
                break;
            case 3:
                hintTxt.text = "魔導書を見つけよう";
                break;
            case 4:
                hintTxt.text = "岩を壊そう";
                break;
            case 5:
                hintTxt.text = "敵を倒して最後の扉を開こう";
                break;
        }
    }

}
