using UnityEngine;
using System.Collections;

public class tamaMove : MonoBehaviour {

    public float timer = 3;
    public float attack = 10;   //攻撃力

    void Update()
    {
        Destroy(gameObject, timer);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            //Enemyとぶつかった時実行
            //   col.gameObject.SendMessage("Damage", attack);
            Destroy(gameObject);
        }
        
    }
}
