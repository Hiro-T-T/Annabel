using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TalkProcess : MonoBehaviour {

	public int playTalkNum = -1 ;								//再生されている会話の番号 マイナス値で無効
	public int playTalkMode = 1 ;								//再生されている会話のモード　説明はplayTalk関数を参照

	bool[] mouseButtonFlag = new bool[3] ;

	public Text serifText ;
	public Text nameText ;

	float deleteTime = 0.0f ;

	TalkSettings talksetting ;
	FlagsInStageManager flagsInStageManager ;

	GameObject sTalkWindow ;
	GameObject bTalkWindow ;

	AudioSource audioSource ;

	Image faceImage ;

	public Sprite[] faceGraph = new Sprite[20] ;

	public class talk
	{

		public float talkTimeCount ;					//現在再生時間
		public int talkPlayNow ;						//現在描画しているセリフ
		public int talkMax ;							//会話の最大個数
		public string[] talkString = new string[2000] ;
		public string[] nameString = new string[2000] ;
		public float[] drawTime = new float[2000] ;
		public int[] drawFace = new int[2000] ;
		public AudioClip[] voice = new AudioClip[2000] ;

		public void talkInit()
		{

			for( int i = 0 ; i < 255 ; i ++ )
			{

				talkString[i] = "nodata" ;
				nameString[i] = "nodata" ;
				drawTime[i] = 0 ;

			}

			talkMax = 0 ;
			talkPlayNow = 0 ;

		}

	}

	talk[] talkClass = new talk[2000] ;

	

	// Use this for initialization
	void Start () {
		
		talkProcessInit();

	}
	
	void Update ()
	{
			
		if( Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space) ||
			Input.GetButtonDown("Submit") )
		{
			mouseButtonFlag[0] = true ;
		}
        
	}

	// Update is called once per frame
	void FixedUpdate () {
			
		nowPlayTalkProcess( playTalkNum , playTalkMode );
		for( int i = 0 ; i < 3 ; i ++ )
		{
			mouseButtonFlag[i] = false ;
		}

	}

	public void playTalk( int talkNum , int talkMode )
	{
		//talkNum = 会話の識別番号
		//talkMode = 会話のモード、0はプレイヤーの操作を制限　1はプレイヤーが自由に動けるスタイルで
		//モード0時はクリックorスペースキーで飛ばせるようにしてあります
		//モード0時はsetTalkのdrawTimeの効果は無効になるのでご注意
		
		if( playTalkNum < 0 )
		{

			if( talkMode == 1 )
			{
				Instantiate( sTalkWindow );

			}else if( talkMode == 0 || talkMode == 2 ){
				Instantiate( bTalkWindow );
			
			}

			

			talkClass[talkNum].talkTimeCount = 0 ;
			talkClass[talkNum].talkPlayNow = 0 ;
			playTalkNum = talkNum ;
			playTalkMode = talkMode ;
			flagsInStageManager.talkMode = talkMode ;
			if( talkMode == 2 ) flagsInStageManager.talkMode = 0 ;

		
			
		}
		Debug.Log( "TalkPlay" );
	}

	//この関数はvoid Start()で入れといてください。Updateで入れると無限に追加されてエラーになります
	public void setTalk( int talkNum , float drawTime , string charaName , string talkString )
	{
		//talkNum 登録する会話ナンバー、playTalkのtalkNumで呼び出したい番号をいれます
		//drawTime そのセリフを描画しておく時間です。秒単位
		//drawFace 描画する顔グラの識別番号です。
		//charaName 描画したいキャラクターの名前を入れます
		//talkString 描画したいセリフを入れます

		deleteTime = 0.0f ;
		talkClass[talkNum].talkString[talkClass[talkNum].talkMax] = talkString ;
		talkClass[talkNum].nameString[talkClass[talkNum].talkMax] = charaName ;
		talkClass[talkNum].drawTime[talkClass[talkNum].talkMax] = drawTime ;
		//talkClass[talkNum].drawFace[talkClass[talkNum].talkMax] = drawFace ;

	    talkClass[talkNum].voice[talkClass[talkNum].talkMax] = Resources.Load("Voice/"+talkNum+"_"+talkClass[talkNum].talkMax) as AudioClip ;

		talkClass[talkNum].talkMax ++ ;

		

	}

	//いじっちゃやーよ（はぁと）
	void nowPlayTalkProcess( int talkNum , int talkMode )
	{

		if( GameObject.Find("TalkSerif") != null ) serifText = GameObject.Find("TalkSerif").GetComponent<Text>();
		if( GameObject.Find("TalkName") != null ) nameText = GameObject.Find("TalkName").GetComponent<Text>();

		if( talkNum >= 0 )
		{
			if( talkMode == 0 || talkMode == 2 ){

			//	if( GameObject.Find("expression") != null ) faceImage = GameObject.Find("expression").GetComponent<Image>();
				//faceImage.sprite = faceGraph[ talkClass[talkNum].drawFace[talkClass[talkNum].talkPlayNow] ] ;

				if( mouseButtonFlag[0] == true )
				{
					talkClass[talkNum].talkTimeCount += 1.0f ;
				}else {
					talkClass[talkNum].talkTimeCount = 0.0f ;
				}

				if( talkClass[talkNum].talkTimeCount == 1.0f )
				{

					talkClass[talkNum].talkPlayNow ++ ;
				//	if( talkClass[talkNum].voice[talkClass[talkNum].talkPlayNow] != null && talkClass[talkNum].talkPlayNow > 0 )
					//{
						//audioSource.Stop();
						//audioSource.PlayOneShot( talkClass[talkNum].voice[talkClass[talkNum].talkPlayNow] );
				//	}
				}

			}else if( talkMode == 1 ){

				talkClass[talkNum].talkTimeCount += Time.deltaTime ;

				//Debug.Log( talkClass[talkNum].drawTime[talkClass[talkNum].talkPlayNow] );

				//セリフ転換処理
				if( talkClass[talkNum].talkTimeCount > talkClass[talkNum].drawTime[talkClass[talkNum].talkPlayNow] )
				{
					talkClass[talkNum].talkPlayNow ++ ;
					talkClass[talkNum].talkTimeCount = 0.0f ;
					if( talkClass[talkNum].voice[talkClass[talkNum].talkPlayNow] != null )
					{
						audioSource.Stop();
						audioSource.PlayOneShot( talkClass[talkNum].voice[talkClass[talkNum].talkPlayNow] );
					}
				}
			}

			//セリフの表示
			if( talkClass[talkNum].talkPlayNow < talkClass[talkNum].talkMax ) 
			{
				serifText.text = talkClass[talkNum].talkString[talkClass[talkNum].talkPlayNow] ;
				nameText.text = talkClass[talkNum].nameString[talkClass[talkNum].talkPlayNow] ;
			}

			//会話の終了
			if( talkClass[talkNum].talkPlayNow >= talkClass[talkNum].talkMax ) 
			{
				//audioSource.Stop();
				playTalkNum = -1 ;
				if( talkMode == 2 ) flagsInStageManager.gameClear = true ;
			}
		}else {
			
		
			deleteTime += Time.deltaTime ;
			flagsInStageManager.talkMode = -1 ;
			if( deleteTime > 1.0f )
			{
				
				if( GameObject.Find("CanvasTalkMini(Clone)") != null ) GameObject.Destroy( GameObject.Find("CanvasTalkMini(Clone)") );
				if( GameObject.Find("CanvasTalkBig(Clone)")  != null ) GameObject.Destroy( GameObject.Find("CanvasTalkBig(Clone)") );
				deleteTime = 0.0f ;
			}

		}

	}

	public void endTalk()
	{

		playTalkNum = -1 ;
		
		flagsInStageManager.talkMode = -1 ;

		//audioSource.Stop();

		if( GameObject.Find("CanvasTalkMini(Clone)") != null ) GameObject.Destroy( GameObject.Find("CanvasTalkMini(Clone)") );
		if( GameObject.Find("CanvasTalkBig(Clone)")  != null ) GameObject.Destroy( GameObject.Find("CanvasTalkBig(Clone)") );
		deleteTime = 0.0f ;
		Debug.Log( "TalkEnd" );
	}

	//初期化しゅるぅうううう
	void talkProcessInit()
	{

		sTalkWindow = Resources.Load("CanvasTalkMini") as GameObject ;
		bTalkWindow = Resources.Load("CanvasTalkBig") as GameObject ;

		talksetting = GameObject.Find("GameControlObject").GetComponent<TalkSettings>();

	//	audioSource = gameObject.GetComponent<AudioSource>();

		for( int i = 0 ; i < 2000 ; i ++ )
		{
			talkClass[i] = new talk() ;
			talkClass[i].talkInit() ;
		}

		flagsInStageManager = GameObject.Find("GameControlObject").GetComponent<FlagsInStageManager>();

	}

}
