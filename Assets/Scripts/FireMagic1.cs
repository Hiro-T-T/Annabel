using UnityEngine;
using System.Collections;

public class FireMagic1 : MonoBehaviour {

   private PlayerMove playerMove;
   private Vector3 pos;
   private GameObject targetObj;

   Vector3 playerTransform;
   Vector3 enemyTransform;
   private Vector3 enemyDistance;
   private Vector3 enemyDistanceHalf;
   Vector3 bezierHalfPoint;
    public AudioClip audioClip;
    AudioSource audioSource;
    public int damage = 2;
    public float tAdd = 0.03f;

 //   float r = 
    
    float t = 0.0f;
	// Use this for initialization
	void Start () {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        playerMove = GameObject.Find("player").GetComponent<PlayerMove>();
        pos = gameObject.transform.position;
        targetObj = playerMove.targetEnemyPosition;

        playerTransform = playerMove.transform.position;
        enemyTransform = targetObj.transform.position;
        enemyDistance = (enemyTransform + playerTransform) * 0.5f;
        //enemyDistanceHalf = new Vector3(playerTransform.x + (Mathf.Abs(enemyDistance.x) * 0.5f), playerTransform.y + (Mathf.Abs(enemyDistance.y) * 0.5f), playerTransform.z + (Mathf.Abs(enemyDistance.z) * 0.5f));

        float tamaPointDirection = Random.Range(-10.0f, 10.0f);

        bezierHalfPoint.x = Random.Range(playerTransform.x, enemyTransform.x) + tamaPointDirection;

        tamaPointDirection = Random.Range(-3.0f, 3.0f);

        bezierHalfPoint.y = Random.Range(playerTransform.y, enemyTransform.y) + tamaPointDirection;

        tamaPointDirection = Random.Range(-10.0f, 10.0f);

        bezierHalfPoint.z = Random.Range(playerTransform.z, enemyTransform.z) + tamaPointDirection;

       
    }
	// Update is called once per frame
	void FixedUpdate () {

        t += Time.deltaTime * 2.5f;
	if(targetObj != null)
        {
            
            enemyTransform = targetObj.transform.position;
            pos = (1 - t) * (1 - t) * playerTransform + 2 * (1 - t) * t * bezierHalfPoint + t * t * enemyTransform;
            
            // pos = enemyDistance;
            transform.position = pos;

        }else
        {
            Destroy(gameObject);
        }
	}

    void OnTriggerStay(Collider col)
    {
        if(col.gameObject.tag == ("Enemy"))
        {
            EnemyAI enemyAI = col.GetComponent<EnemyAI>();
            enemyAI.hp -= damage;
            GameObject SE = Resources.Load("EnemyDamageSe") as GameObject;
            GameObject obj = GameObject.Instantiate(SE) as GameObject;
            EnemyDamageSe enemyDamageSe = obj.GetComponent<EnemyDamageSe>();
            enemyDamageSe.sound = audioClip;
            
            //audioSource.Play();
            Destroy(gameObject);
        }
    }
}
