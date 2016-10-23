using UnityEngine;
using System.Collections;

public class CounterMagic1 : MonoBehaviour {
    public int damage = 0;
    public int damageCount = 0;
    PlayerMove playerMove;
    private int time;
    public int timeEnd;
    public Vector3 pos;
	// Use this for initialization
	void Start () {
       // playerMove = GameObject.Find("player").GetComponent<PlayerMove>();
       // pos = playerMove.targetEnemyPosition.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = pos;
        time++;
        if(time >= timeEnd)
        {
            Destroy(gameObject);
        }
	}
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == ("Enemy") && time <= damageCount)
        {
            EnemyAI enemyAI = col.GetComponent<EnemyAI>();
            enemyAI.hp -= damage;
         //   Destroy(gameObject);
        }
    }
}
