using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

    GameObject player;    //プレイヤーを代入
    public int hp = 3;
    public float speed = 3; //移動速度
    public float attackDir = 3.0f;
    public int attackCount = 120;
    float length;

    int moveMode = 0;
    float count = 0;
    int attackTime;
    Vector3 playerTransformPrevious = new Vector3(0.0f, 0.0f, 0.0f);
    Vector3 playerTransform = new Vector3(0.0f, 0.0f, 0.0f);
    Vector3 targetTransform = new Vector3(0.0f, 0.0f, 0.0f);


    // Use this for initialization
    void Start()
    {
        this.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        this.transform.rotation = new Quaternion(0, transform.rotation.y, 0, 0);
        player = GameObject.FindGameObjectWithTag("Player");
        transform.LookAt(new Vector3(player.transform.position.x, 0.0f, player.transform.position.z));   //プレイヤーの方を向く
        targetTransform = player.transform.position;
        playerTransformPrevious = targetTransform;


        // count = GameObject.Find("GameManager").GetComponent<GameManager>().counter;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        count += Time.deltaTime;
  //      Debug.Log(count);
        if(moveMode == 0)
        {
            attackTime = Random.Range(1, 3);
        
            moveMode = 1;
        }
        if(moveMode == 1)
        {
            if(count > attackTime)
            {
                playerTransform = player.transform.position;
                moveMode = 2;
            }
        }
        if(moveMode == 2)
        {
            targetTransform += (playerTransform - targetTransform) * 0.1f;
            transform.LookAt(new Vector3(targetTransform.x, 0.0f, targetTransform.z));

            Debug.Log("目標位置" + targetTransform);
            if((attackTime + 1) < count)
            {
               GameObject obj = Instantiate(Resources.Load("E_bullet"),transform.position, Quaternion.identity) as GameObject;
                EnemyBullet enemyBullet = obj.GetComponent<EnemyBullet>();
                enemyBullet.targetFoward = transform.forward;
                Debug.Log("新宿");
                count = 0;
                moveMode = 0;
            }
            /*if((attackTime + 3) < count)
            {
                
               
            }
            */
        }
          Debug.Log("攻撃" + attackTime);
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
}
