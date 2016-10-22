using UnityEngine;
using System.Collections;

public class CounterMagic1 : MonoBehaviour {
    public int damage = 20;
    PlayerMove playerMove;
    private int time;
    private Vector3 pos;
	// Use this for initialization
	void Start () {
        playerMove = GameObject.Find("player").GetComponent<PlayerMove>();
        pos = playerMove.targetEnemyPosition.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = pos;
        time++;
        if(time >= 90)
        {
            Destroy(gameObject);
        }
	}
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == ("Enemy") && time <= 30)
        {
            EnemyAI enemyAI = col.GetComponent<EnemyAI>();
            enemyAI.hp -= damage;
         //   Destroy(gameObject);
        }
    }
}
