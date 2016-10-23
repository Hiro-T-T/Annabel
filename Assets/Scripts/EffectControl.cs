using UnityEngine;
using System.Collections;

public class EffectControl : MonoBehaviour {
    public int damage = 0;
    public int damageCount = 0;
    PlayerMove playerMove;
    private float time;
    public float timeEnd;
    public Vector3 pos;

    ParticleSystem myParticleSystem;
    // Use this for initialization
   public void Awake()
    {
        // playerMove = GameObject.Find("player").GetComponent<PlayerMove>();
        // pos = playerMove.targetEnemyPosition.transform.position;
        myParticleSystem = this.GetComponent<ParticleSystem>();

    }

    // Update is called once per frame
   public void Update()
    {

        time++;
        if (time >= timeEnd)
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
