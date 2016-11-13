using UnityEngine;
using System.Collections;

public class FlagsInStageManager : MonoBehaviour {
    public bool isSpiderSwinging = false;
    public bool stageStarted = false;
    public int stageNumber;
    public float stageTimer = 0f;
    public float stageTimeMax = 0f;
    public bool playerInAirFlag = false;
    public bool targetLockOn = false;
    public int startTime = 120;
    public bool gameClear = false;
    public bool gameOver = false;
    public int talkMode = -1;
    public bool pauseFlag = false;
    public bool matchFlag = false;
    public float matchTime = 0.0f;
    public float matchNowTimeCount = 0.0f;
    public Vector3 matchStartPos = new Vector3(0.0f, 0.0f, 0.0f);
    public bool batleMode = false;
    public bool cameraMode = false;
    private MainCameraControl mainCameraControl;
    //   Button Button1;

    private bool startTrigger = false;
    
    int gamecounter = 0;

    public string gameOverChangeSceneName;
    int sceneChangeCounter = 190;

    public Animator animator;
    public Animator pauseAnim;

    //playermove player ;
    //stageClearedFlag stageClearProcess ;

    //public GameObject helpObj ;
    //public GameObject optionObj ;

    int buttonCount = 0;

    public bool helpFlag = false;
    public bool optionFlag = false;

    TalkProcess talk;
    public GameObject match;

    bool[] mouseButtonFlag = new bool[3];
    private PlayerMove player;

    private CameraControl cameraControl;
    // Use this for initialization
    void Start()
    {
        	player = GameObject.Find("player").GetComponent<PlayerMove>();
        cameraControl = GameObject.Find("CameraDammy").GetComponent<CameraControl>();
        mainCameraControl = Camera.main.GetComponent<MainCameraControl>();
        talk = GameObject.Find("GameControlObject").GetComponent<TalkProcess>();
        /*animator = GameObject.Find("ActionUI").GetComponent<Animator>();
		pauseAnim = GameObject.Find("Pausemenu").GetComponent<Animator>();
		Button1 = GameObject.Find("Button1").GetComponent<Button>();
		if (GameObject.Find("ClearFlagObject") != null)
		{
	//		stageClearProcess = GameObject.Find("ClearFlagObject").GetComponent<stageClearedFlag>();
		}*/
        /////////       
        //match = Resources.Load("Prefab/GameMainObjects/Match") as GameObject ;
    }

    void Update()
    {

    //    Cursor.lockState = CursorLockMode.Locked;

        if (stageStarted == true && gameClear == false && gameOver == false) pauseProcess();
        stageUnlock();

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            buttonCount++;
            if (buttonCount == 1)
            {
                mouseButtonFlag[0] = true;
            }
        }
        else
        {
            buttonCount = 0;
        }
        //戦闘終了処理
        
        if (batleMode == true)
        {
            GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
            if(enemys.Length == 0)
            {
                player.guardFlag = false;
                player.guardTime = 0;
                cameraControl.transform.eulerAngles = mainCameraControl.transform.localEulerAngles;
                batleMode = false;
            }
        }
        //カメラモード処理
        if (Input.GetAxis("Camera") == 1)
        {
            if(batleMode == false) cameraMode = true;
        }
        else cameraMode = false;
    //    Debug.Log(cameraMode);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gamecounter > 1)
        {
            if (talkMode != 0 && gameOver == false)
            {
                timerUpdate();
                if (matchFlag == true) matchProcess();
            }
            if (talkMode != 0) gameStartProcess();


            gameOverProcess();
        }
        gamecounter++;
    }

    void stageUnlock()
    {
        /*
        int nowStageNum = 0;

        if (Application.loadedLevelName == "tutorial") nowStageNum = 1;
        if (Application.loadedLevelName == "stage1") nowStageNum = 2;
        if (Application.loadedLevelName == "stage2") nowStageNum = 3;
        if (Application.loadedLevelName == "stage3") nowStageNum = 4;
        if (Application.loadedLevelName == "stage4") nowStageNum = 5;
        */

        if (gameClear == true)
        {

            //	if( stageClearProcess != null )
            //	{
            //		if( stageClearProcess.clearedStage < nowStageNum )
            //		{
            //
            //		stageClearProcess.clearedStage = nowStageNum ;

            //		}

            //	}
        }

    }

    void timerUpdate()
    {
        if (stageStarted == true && gameClear == false) stageTimer += Time.deltaTime;
        if (stageTimeMax <= stageTimer && stageTimeMax > 0.0f)
        {
            stageTimer = stageTimeMax;
            gameOver = true;
        }
    }

    void pauseProcess()
    {



        if (Input.GetButtonDown("Cancel"))
        {
            if (pauseFlag == true)
            {
                if (helpFlag == false && optionFlag == false)
                {
                    pauseFlag = false;
                    Time.timeScale = 1.0f;
                   // Screen.lockCursor = true;
                    pauseAnim.SetBool("pause", false);
                }

            }

        }

        /*if( Input.GetButtonDown("Pause") )
		{
			if(pauseFlag == false)
			{
				pauseAnim.SetBool( "pause" , true );
				pauseFlag = true ;
				Time.timeScale = 0.0f ;
				Screen.lockCursor = false;
				Button1.Select();
				
			}else {

				if( helpFlag == false && optionFlag == false )
				{
					pauseFlag = false ;
					Time.timeScale = 1.0f ;
					Screen.lockCursor = true;
					pauseAnim.SetBool( "pause" , false );
				}
			}
		}*/

        if (helpFlag == true)
        {

            ////helpObj.SetActive(true);
            if (Input.GetButtonDown("Cancel") || mouseButtonFlag[0] == true || Input.GetButtonDown("Jump"))
            {

                helpFlag = false;

            }

        }
        else
        {
            //helpObj.SetActive(false);
        }

        if (optionFlag == true)
        {

            //	optionObj.SetActive(true);
            if (Input.GetButtonDown("Cancel") || Input.GetButtonDown("Pause") || Input.GetButtonDown("Jump"))
            {

                optionFlag = false;

            }

        }
        else
        {
            //	optionObj.SetActive(false);
        }


        for (int i = 0; i < 3; i++)
        {
            mouseButtonFlag[i] = false;
        }

    }

    void gameStartProcess()
    {

        startTime--;

        if (startTime <= 0)
        {
            stageStarted = true;
            //animator.SetBool( "gameStart" , true );
        }
        else
        {
            //animator.SetBool( "gameStart" , true );
        }

    }

    void gameOverProcess()
    {

        if (gameOver == true)
        {
            animator.SetBool("gameOver", true);
            sceneChangeCounter--;
       //     if (sceneChangeCounter <= 0) SceneManager.LoadScene(gameOverChangeSceneName);
        }
    }

    void matchProcess()
    {

        matchNowTimeCount += Time.deltaTime;
        Debug.Log(matchTime - matchNowTimeCount);
        if (matchNowTimeCount >= matchTime)
        {
            if (match.gameObject.activeInHierarchy == true)
            {
      //          talk.playTalk(202, 0);
            }
            match.gameObject.SetActive(false);
            if (matchNowTimeCount >= matchTime + 0.7f)
            {
                matchNowTimeCount = 0.0f;
                //	player.transform.position = matchStartPos ;
                match.SetActive(true);
                //	match.transform.parent = player.transform ;
            }

        }

    }

}
