using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TargetControl : MonoBehaviour {

    PlayerMove playerMove;
    RectTransform rectTransform = null;
    [SerializeField] Transform target = null;

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
         target = playerMove.targetEnemy.transform;
        rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, target.position);
    }
}
