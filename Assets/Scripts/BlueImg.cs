using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BlueImg : MonoBehaviour {

    public bool imgEn = false;
    public bool blDst = false;

	// Use this for initialization
	void Start () {
            gameObject.GetComponent<Image>().enabled = false;

    }

    // Update is called once per frame
    void Update () {
	    if(imgEn == true)
        {
            blDst = true;
            imgEn = false;
            Invoke("blT", 1.5f);
        }
    }

    public void blT()
    {
        gameObject.GetComponent<Image>().enabled = true;
        Invoke("ImgOff", 1.5f);

    }

    public void ImgOff()
    {
        gameObject.GetComponent<Image>().enabled = false;

    }
}
