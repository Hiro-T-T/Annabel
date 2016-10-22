using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TargetControl : MonoBehaviour {

    //現在使用していませんar

    PlayerMove playerMove;
    RectTransform rectTransform = null;
    //[SerializeField] Transform 
    GameObject target;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Use this for initialization
    void Start () {
        playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();
	}
	
	// Update is called once per frame
	void Update () {
         target = playerMove.targetEnemyPosition;
        rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, target.transform.position);
        Debug.Log(rectTransform.position);
    }
}
