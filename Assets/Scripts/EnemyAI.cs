using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

    GameObject player;    //プレイヤーを代入
    PlayerMove playerMove;
    public int hp = 3;
    public float speed = 3; //移動速度
    public float attackDir = 3.0f;
    public int attackCount = 120;
    float length;
    int damage;
    int moveMode = 0;
    float count = 0;
    float attackTime;
    Vector3 playerTransformPrevious = new Vector3(0.0f, 0.0f, 0.0f);
    Vector3 playerTransform = new Vector3(0.0f, 0.0f, 0.0f);
    Vector3 targetTransform = new Vector3(0.0f, 0.0f, 0.0f);
    float countTime = 0.0f;
    public int enemyMode = 0;
    /*-------------------------------------------------
    0 = ドール
    1 = どくろ

    *///-----------------------------------------------

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMove = player.GetComponent<PlayerMove>();
        if(enemyMode == 0)
        {
            this.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            this.transform.rotation = new Quaternion(0, transform.rotation.y, 0, 0);
            transform.LookAt(new Vector3(player.transform.position.x, 0.0f, player.transform.position.z));   //プレイヤーの方を向く
            targetTransform = player.transform.position;
            playerTransformPrevious = targetTransform;
        }



        // count = GameObject.Find("GameManager").GetComponent<GameManager>().counter;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        enemyAction(enemyMode);
       
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
        //   transform.rotation = new Quaternion(0, transform.rotation.y, 0, 0);
        /*
        count++;
        Vector3 playerPos = player.position;    //プレイヤーの位置
        Vector3 direction = playerPos - transform.position; //方向
        length = direction.magnitude; // 距離を長さに変換
        direction = direction.normalized;   //単位化（距離要素を取り除く）
        transform.position = transform.position + (direction * speed * Time.deltaTime);
        transform.LookAt(player);   //プレイヤーの方を向く
        this.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        this.transform.rotation = new Quaternion(0, transform.rotation.y, 0,0);
        if (count - attackCount > 0)
        {
                count = 0;
                Instantiate(Resources.Load("E_bullet"), this.transform.position, Quaternion.identity);
        }
 
        */
    }
    float Ywidth;
    int hitMove = 1;


    void enemyAction(int enemyModeNum)
    {
        //ドール
        if(enemyModeNum == 0)
        {
            count += Time.deltaTime;
            //      Debug.Log(count);
            if (moveMode == 0)
            {
                attackTime = Random.Range(1, 3);

                moveMode = 1;
            }
            if (moveMode == 1)
            {
                if (count > attackTime)
                {
                    playerTransform = player.transform.position;
                    moveMode = 2;
                }
            }
            if (moveMode == 2)
            {
                targetTransform += (playerTransform - targetTransform) * 0.1f;
                transform.LookAt(new Vector3(targetTransform.x, 0.0f, targetTransform.z));

             //   Debug.Log("目標位置" + targetTransform);
                if ((attackTime + 1) < count)
                {
                    GameObject obj = Instantiate(Resources.Load("E_bullet"), transform.position, Quaternion.identity) as GameObject;
                    EnemyBullet enemyBullet = obj.GetComponent<EnemyBullet>();
                    enemyBullet.targetFoward = transform.forward;
              //      Debug.Log("新宿");
                    count = 0;
                    moveMode = 0;
                }
                /*if((attackTime + 3) < count)
                {


                }
                */
            }
        //    Debug.Log("攻撃" + attackTime);
        }

        //ドクロ
        if (enemyMode == 1)
        {
            Vector3 playerTransform = new Vector3(player.transform.position.x, 0.0f, player.transform.position.z);
            Vector3 myTransform = new Vector3(transform.position.x, 0.0f, transform.position.z);

            count += Time.deltaTime;
            
            if(moveMode == 0)
            {
                attackTime = Random.Range(0.5f, 1.5f);
                moveMode = 1;
            }
            if(moveMode == 1)
            {
                if (count > 1)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerTransform - myTransform), 0.1f);
                    transform.position = new Vector3(transform.position.x, (Mathf.Abs(Mathf.Cos(count * 1.5f)) * 2.5f) + 0.5f, transform.position.z);
                    //  transform.position += new Vector3(0.0f, 2.0f, 0.0f);
                    //Ywidth -= 0.1f;
                    //   transform.position += new Vector3(0.0f, Ywidth, 0.0f);
                    transform.position += transform.forward * 0.1f * hitMove;
                }
                Vector3 rayDirection = new Vector3(0.0f, -1.0f, 0.0f);
                RaycastHit hit;
                Ray ray = new Ray(transform.position, rayDirection);
                if (Physics.Raycast(ray, out hit, 0.6f, LayerMask.GetMask("Stage")))
                {
                    if (count > attackTime + 0.1f)
                    {
                        hitMove = 1;
                        moveMode = 0;
                        count = 0;


                    }

                }
            }
    
        }
    }

    void OnTriggerStay(Collider col)
    {
        if(col.gameObject.tag == ("Player"))
        {
            if(moveMode == 1)
            {
                if(count > 1)
                {
                    hitMove = -1;
                   

                    if (playerMove.damageCount == 0)
                    {
                        playerMove.hp -= damage;
                        playerMove.damageCount = 60;
                    }
                
                    Debug.Log("池袋にあたる");
                }
            }
 
        }

        if (col.gameObject.name == ("GuardObj") && playerMove.guardFlag == true)
        {
            damage = 2;
            Debug.Log("大根ガード");
            if (playerMove.guardTime < playerMove.guardCounterTime)
            {
                playerMove.guardCounter = true;
                playerMove.guardTime += 7;
            }
            else
            {
                playerMove.hp -= damage * 0.5f;
            }

            hitMove = -1;
        }
    }


}
