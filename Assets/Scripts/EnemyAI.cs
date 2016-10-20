using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

    public Transform player;    //プレイヤーを代入
    public float speed = 3; //移動速度
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.position;    //プレイヤーの位置
        Vector3 direction = playerPos - transform.position; //方向
        direction = direction.normalized;   //単位化（距離要素を取り除く）
        transform.position = transform.position + (direction * speed * Time.deltaTime);
        transform.LookAt(player);   //プレイヤーの方を向く
        this.transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        this.transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z,0);
    }
}
