using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {

    public float speed = 1.0f;
    public int lifeTime = 2;
    int second = 0;
    int counter = 0;

    void Start()
    {
        second = 0;
        counter = 0;
    }

    void Update()
    {
        second++;
        float step = Time.deltaTime * speed;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
        if(second > 60)
        {
            second = 0;
            counter++;
        }
        if(counter > lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
