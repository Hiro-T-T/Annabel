using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

    public Transform player;    //プレイヤーを代入
    public int hp = 3;
    public float speed = 3; //移動速度
    public float attackDir = 3.0f;
    public int attackCount = 120;
    float length;
    int count = 0;

    // Use this for initialization
    void Start()
    {
       
        player = GameObject.FindGameObjectWithTag("Player").transform;
       
        // count = GameObject.Find("GameManager").GetComponent<GameManager>().counter;
    }

    // Update is called once per frame
    void Update()
    { 

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
        if(hp <= 0)
        {
            Destroy(gameObject);
        }

    }
}
