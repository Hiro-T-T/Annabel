using UnityEngine;
using System.Collections;

public class EnemyDamageSe : MonoBehaviour {
    private AudioSource se;
    public AudioClip sound;
    float count;
    // Use this for initialization
    void Start () {
        se = gameObject.GetComponent<AudioSource>();
        se.loop = false;
        se.PlayOneShot(sound);
    }
	
	// Update is called once per frame
	void Update () {
        count += Time.deltaTime;
        if(count > 3)
        {
            Destroy(gameObject);
        }
	}
}
