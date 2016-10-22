using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAction : MonoBehaviour {
    //基礎設定===========================================
    public int AttackTipe = 1;
    public int MoveTipe = 1;
    public Vector3 pos;
    private Vector3 startPos;
    FlagsInStageManager flagsInStageManager;
    //エンカウント=======================================
    public playerSearch playerSearch;
    public Material[] material;
    private int materialNum = 0;
    public float playerDistance = 5.0f;
    private bool attackMode = false;
    private int attackModeCount = 0;
    public int attackModeTime = 600;
    //MoveTipe1 ==========================================
    public int fuwa = 0;
    public int fuwaTime = 240;
    private int fuwaAdd = 1;
    //MoveTipe2 ==========================================




    //MoveTipe3 ===========================================

    


    //AttackTipe1 =========================================
    public Transform player;    //プレイヤーを代入
    public float speed = 3; //移動速度
    public float boundPow = 5.0f;
    private Vector3 playerPos;//プレイヤーの位置
    private Vector3 direction; //方向
    //AttackTipe2 =========================================
    private int shootPreviousTime;
    private int min = 60;
    private int Max = 120;
    private int shootTime;
    public GameObject bullet;


    //AttackTipe3 =========================================
    private bool hit_flag = false;
    private bool field_flag = false;
    private int hit_count = 0;
    private float hit_y;

    private AudioSource se;
    public AudioClip sound;


    void Start()
    {
        pos = gameObject.transform.position;
        player = GameObject.Find("PlayerTarget").transform;
        startPos = pos;
        playerSearch = gameObject.GetComponent<playerSearch>();
        flagsInStageManager = GameObject.Find("GameControlObject").GetComponent<FlagsInStageManager>();
        se = gameObject.GetComponent<AudioSource>();
        se.loop = false;
        shootTime = Random.Range(min, Max);
}

    void Update()
    {
        if (flagsInStageManager.gameClear == false && flagsInStageManager.gameOver == false && flagsInStageManager.talkMode != 0)
        {

            gameObject.transform.position = pos;

            //gameObject.GetComponent<Renderer>().material = material[materialNum];

            if (playerSearch.dist < playerDistance && attackMode == false)
            {
                attackMode = true;
            }
            else if (playerSearch.dist > playerDistance * 2 && attackMode == true)
            {
                attackMode = false;

            }


            if (attackMode == true)
            {
                materialNum = 1;
                if (AttackTipe == 2)
                {
                    Idle();
                }
                else if (AttackTipe != 2)
                {

                    fuwa = 0;
                }

                Attack();
                attackModeCount++;
                if (attackModeCount > attackModeTime)
                {
                    attackMode = false;

                }
            }
            else if (attackMode == false)
            {
                materialNum = 0;
                attackModeCount = 0;
                Idle();
            }
        }
    }
    void Idle()
    {
        if (MoveTipe == 1)
        {
            // materialNum = 0;
            fuwa += fuwaAdd;
            if (field_flag == true)
            {

                pos.y = startPos.y + (Mathf.Sin(Mathf.PI * 2 / fuwaTime * fuwa)) * 1;
            }
            else
            {

                pos.y = startPos.y + (Mathf.Sin(Mathf.PI * 2 / fuwaTime * fuwa)) * 1;
            }
            transform.LookAt(player);   //プレイヤーの方を向く

        }
        //=================================================================
        else if (MoveTipe == 2)
        {
            fuwa += fuwaAdd;

            playerPos.x = player.position.x;    //プレイヤーの位置
            direction.x = playerPos.x - pos.x; //方向
            playerPos.z = player.position.z;    //プレイヤーの位置
            direction.z = playerPos.z - pos.z; //方向
            direction = direction.normalized;   //単位化（距離要素を取り除く）
            if(playerSearch.dist > 3 && playerSearch.dist < playerDistance)
            {

                if (hit_flag == false)
                {
                    startPos.x += (direction.x * speed * Time.deltaTime);
                    startPos.z += (direction.z * speed * Time.deltaTime);
                   
                }
                else
                {
                    hit_count++;
                    startPos.x -= (direction.x * speed * Time.deltaTime);
                    startPos.z -= (direction.z * speed * Time.deltaTime);
                  
                    if (hit_count > 5)
                    {
                        hit_flag = false;
                        hit_count = 0;
                    }
                }

                
            }

            pos.x = startPos.x + (Mathf.Sin(Mathf.PI * 5 / fuwaTime * fuwa)) * 1;
            pos.z = startPos.z + (Mathf.Cos(Mathf.PI * 5 / fuwaTime * fuwa)) * 1;

            transform.LookAt(player);   //プレイヤーの方を向く

        }
        //=================================================================

        if(MoveTipe == 3)
        {

        }

    }


    void Attack()
    {
        if (AttackTipe == 1 || AttackTipe == 3)
        {
            
            playerPos = player.position;    //プレイヤーの位置
            direction = playerPos - pos; //方向
            direction = direction.normalized;   //単位化（距離要素を取り除く）
            if (field_flag == true)
            {
                pos.y = hit_y;
            }
            if (hit_flag == false)
            {
                pos += (direction * speed * Time.deltaTime);
            }
            else
            {
                hit_count++;
                pos -= (direction * speed * Time.deltaTime);
                if (hit_count > 5)
                {
                    hit_flag = false;
                    hit_count = 0;
                }
            }
         

            transform.LookAt(player);   //プレイヤーの方を向く
            startPos.y = pos.y;
        }
        //=====================================================================
        if(AttackTipe == 2 || AttackTipe == 3)
        {
            shootPreviousTime++;
            if(shootPreviousTime > shootTime)
            {
                Shoot();
            }
            transform.LookAt(player);   //プレイヤーの方を向く

        }

        //=====================================================================


    }
    void OnTriggerEnter(Collider hit)
    {
        if (hit.CompareTag("Stage") == true)
        {
            hit_flag = true;
            fuwaAdd *= -1;
            if (hit_count > 1)
            {
                hit_count = 0;
            }
            
        }
        if(hit.CompareTag("Field") == true)
        {
            field_flag = true;
            hit_y = pos.y;
            fuwaAdd *= -1;
        }
    }

    void OnTriggerExit(Collider hit)
    {
        if (hit.CompareTag("Field") == true)
        {
            field_flag = false;
        }
    }

    void Shoot()
    {
        GameObject obj = GameObject.Instantiate(bullet) as GameObject;
        se.PlayOneShot(sound);
        obj.transform.position = pos;
        shootTime = Random.Range(min, Max);
        shootPreviousTime = 0;
    }
}
