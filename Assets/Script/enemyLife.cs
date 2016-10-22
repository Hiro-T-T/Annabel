using UnityEngine;
using System.Collections;

public class enemyLife : MonoBehaviour {

    public float life = 5;    //現在体力
    public bool hitFlag = false;
    public GameObject effect;
   // private Renderer rend;
    private int frame = 0;

    

    void Start()
    {

     //   rend = GetComponent<Renderer>();
       
    }
  

    void Update()
    {
        if (life <= 0)
        {
            //体力が0になったら
            Dead();
        }

        if(hitFlag == true)
        {
            if(frame / 30 % 2 == 0)
            {
              //  rend.enabled = false;
            }
            else
            {
              //  rend.enabled = true;
            }
        }

      
    }

    //死亡処理（死亡時の演出）
    public void Dead()
    {

        GameObject obj = GameObject.Instantiate(effect) as GameObject;
        obj.transform.position = gameObject.transform.position;
        Destroy(gameObject);
    }

    //ゲームオーバー処理
  
    //体力を表示


    
}