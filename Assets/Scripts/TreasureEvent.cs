using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TreasureEvent : MonoBehaviour {

    public BlueImg blImg;
    private bool treOn = false;

    void Start()
    {
        treOn = false;
        blImg = GameObject.Find("BlueDoor").GetComponent<BlueImg>(); 
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player") && treOn == false)
        {
            blImg.imgEn = true;
            treOn = true;
            this.GetComponent<Animator>().SetTrigger("onTrigger");
        }
    }
}
