using UnityEngine;
using System.Collections;

public class TalkTriggerProcess : MonoBehaviour {
	TalkProcess talk ;

	// Use this for initialization
	void Start () {

		talk = GameObject.Find("GameControlObject").GetComponent<TalkProcess>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter( Collider col )
	{
		if( col.gameObject.tag == "TalkTrigger" )
		{
			talk.endTalk();
            talk.playTalk(col.GetComponent<TalkTriggerNumAndMode>().talkNum, col.GetComponent<TalkTriggerNumAndMode>().talkMode);
            if (col.GetComponent<TalkTriggerNumAndMode>().willDestroyAfterPlay)
            {
                Destroy(col.gameObject);
            }
		}
		Debug.Log( col.gameObject.name );
	}

}
