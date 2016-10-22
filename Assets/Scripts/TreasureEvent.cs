using UnityEngine;
using System.Collections;

public class TreasureEvent : MonoBehaviour {
    

    void Start()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            this.GetComponent<Animator>().SetTrigger("onTrigger");
        }
    }
}
