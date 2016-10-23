using UnityEngine;
using System.Collections;
using System;

public class PopUpMessage : MonoBehaviour {

    public bool encOn = false;

    // enumクラス.ここでenumの種類を宣言.
    public enum ImageType
    {
        ENC,
        RED,
        BLUE
    }

    // enumを変数として宣言.
    // publicなのでInspectorで指定可能になる.
    public ImageType imgType;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    switch (imgType)
        {
            case ImageType.BLUE:
                gameObject.SetActive(false);
                break;
            case ImageType.RED:
                gameObject.SetActive(false);
                break;
        }
	}
}
