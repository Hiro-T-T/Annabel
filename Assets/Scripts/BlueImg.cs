using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BlueImg : MonoBehaviour {

    public bool imgEn = false;

	// Use this for initialization
	void Start () {
            gameObject.GetComponent<Image>().enabled = false;

    }

    // Update is called once per frame
    void Update () {
	    if(imgEn == true)
        {
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
        imgEn = false;
        gameObject.GetComponent<Image>().enabled = false;

    }
}
