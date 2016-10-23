using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RedImg : MonoBehaviour {

    public bool ImgOn = false;

	// Use this for initialization
	void Start () {
            gameObject.GetComponent<Image>().enabled = false;

    }

    // Update is called once per frame
    void Update () {
	    if(ImgOn == true)
        {
            gameObject.GetComponent<Image>().enabled = true;
            Invoke("ImgOff", 1.5f);
        }
	}

    public void ImgOff()
    {
        ImgOn = false;
        gameObject.GetComponent<Image>().enabled = false;

    }
}
