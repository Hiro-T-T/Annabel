using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EncountPopUp : MonoBehaviour {

    public bool encOn = false;



    // Use this for initialization
    void Start () {
        gameObject.GetComponent<Image>().enabled = false;

    }

    // Update is called once per frame
    void Update () {
        if (encOn == true)
        {
            Debug.Log("aaaaaa");
            gameObject.GetComponent<Image>().enabled = true;
            Invoke("Imageoff", 1.5f);
        }
	}

    public void Imageoff()
    {
        encOn = false;
        gameObject.GetComponent<Image>().enabled = false;
    }
}
