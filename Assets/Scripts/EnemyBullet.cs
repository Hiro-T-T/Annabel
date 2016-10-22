using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {

    public float speed = 1.0f;
    public int lifeTime = 2;
    int second = 0;
    int counter = 0;
    public float damage = 2;
    private GameObject player;
    private PlayerMove playerMove;
    FlagsInStageManager flagsInStageManager;
    void Start()
    {
        second = 0;
        counter = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        playerMove = player.GetComponent<PlayerMove>();
        flagsInStageManager = GameObject.Find("GameControlObject").GetComponent<FlagsInStageManager>();
    }

    void Update()
    {
        second++;
        float step = Time.deltaTime * speed;
        
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
        if(second > 60)
        {
            second = 0;
            counter++;
        }
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
            if(playerMove.guardTime < 20)
            {
                playerMove.guardCounter = true;
            }
            else
            {
                playerMove.hp -= damage * 0.5f;
            }
           
            Destroy(gameObject);
        }
    }
}
