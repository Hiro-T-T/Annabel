using UnityEngine;
using System.Collections;

public class TalkAnimation : MonoBehaviour {

	Animator animator ;
	FlagsInStageManager flagsInStageManager ;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		flagsInStageManager = GameObject.Find("GameControlObject").GetComponent<FlagsInStageManager>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		animator.SetBool( "talkflag" , false );
		
		if( flagsInStageManager.talkMode != -1 )
		{

			animator.SetBool( "talkflag" , true );

		}

	}
}
