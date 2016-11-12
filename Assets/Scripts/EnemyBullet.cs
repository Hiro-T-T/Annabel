using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {

    public float speed = 1.0f;
    public int lifeTime = 2;
    int second = 0;
    float counter = 0;
    public float damage = 2;
    private GameObject player;
    private PlayerMove playerMove;
    FlagsInStageManager flagsInStageManager;
    public Vector3 targetFoward;
    void Start()
    {
        second = 0;
        counter = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        playerMove = player.GetComponent<PlayerMove>();
        flagsInStageManager = GameObject.Find("GameControlObject").GetComponent<FlagsInStageManager>();
        
    }

    void FixedUpdate()
    {
        counter += Time.deltaTime;
        transform.position += targetFoward * Time.deltaTime * speed;
        transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        if(counter > lifeTime || flagsInStageManager.batleMode == false)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == ("Player"))
        {
            
            playerMove.hp -= damage;
            Destroy(gameObject);
        }
        if(col.gameObject.name == ("GuardObj") && playerMove.guardFlag == true)
        {
            Debug.Log("大根ガード");
            if(playerMove.guardTime < playerMove.guardCounterTime)
            {
                playerMove.guardCounter = true;
                playerMove.guardTime += 7;
            }
            else
            {
                playerMove.hp -= damage * 0.5f;
            }
           
            Destroy(gameObject);
        }
    }
}
