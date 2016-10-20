using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMove : MonoBehaviour {

    public float moveSpeed = 0.3f;                                              //地上時移動速度
    public float movePowInAir = 0.02f;                                          //空中での移動にかかる強さ
    public float moveMaxInAir = 0.3f;                                           //空中での感性限界値
    public float gravity = 0.02f;                                               //重力値
    public float jumppow = 1.0f;												//ジャンプ力
    public float moveFrictionPow = 0.3f;

    bool playerInAirFlag = true;                                                //空中かどうか

    private int jumpCount = 5;
    FlagsInStageManager flagsInStageManager;

    private Animator animator;                                              //アニメーション
    private Vector3 moveDirectionMaxInAir = new Vector3(0.0f, 0.0f, 0.0f);  //空中慣性制限用
    public Vector3 moveDirection = new Vector3(0.0f, 0.0f, 0.0f);           //移動速度格納変数
    public float moveDirectioninAir = 0.0f;									//y軸移動用変数 
    private Vector3 moveDirection_y = new Vector3(0.0f, 0.0f, 0.0f);

    public CharacterController isPlayerController;                          //キャラクターコントローラー
    public Rigidbody rigidBodyInfo;                                         //リジッドボディ呼び出し用変数

    public float addDirectionMaxInAir = 0.0f;                                   //制限超過分考慮した空中慣性制限
    private float moveDirectionMagnitudeRe1fInAir = 0.0f;                       //1フレーム前のmoveDirection.magunitude
    private float addDirectionMax = 0.0f;                                       //制限超過分考慮した地上慣性制限
    private float moveDirectionMagnitudeRe1f = 0.0f;							//1フレーム前のmoveDirection.magunitude

    public GameObject targetEnemy;                     //ターゲットエネミー
    private RectTransform targetMaker;
    private GameObject targetMakerObj;
    //カメラ正面方向取得
    private Vector3 CameraForward;
    //カメラ横方向取得
    private Vector3 CameraRight;
    //カメラダミーの情報取得
    private GameObject cameraDammyObj;
    void Start()
    {
        //色々代入
        rigidBodyInfo = GetComponent<Rigidbody>();
        cameraDammyObj = GameObject.Find("CameraDammy");
        isPlayerController = gameObject.GetComponent<CharacterController>();
        flagsInStageManager = GameObject.Find("GameControlObject").GetComponent<FlagsInStageManager>();
        animator = GetComponent<Animator>();
        targetMakerObj = Resources.Load("Prefab/CanvasTarget/Target") as GameObject;
        
   
     

    }

    void Update()
    {
        moveDirection.y = 0.0f;
        if (playerInAirFlag == false)
        {
            if (Input.GetButtonDown("Jump"))
            {

                playerInAirFlag = true;
                moveDirectioninAir = jumppow;

            }



        }
        //一番近い敵
        if(flagsInStageManager.batleMode == true)
        {
            targetMaker = GameObject.Find("Target").GetComponent<RectTransform>();
            targetEnemy = nearEnemySearch();
            targetEnemy.GetComponent<EnemyMove>().colNum = 1;
            targetMaker.transform.position = Camera.main.WorldToScreenPoint(targetEnemy.transform.position);
        }
  
    }
    void FixedUpdate()
    {
        bool pushKeyFlag = false;
        //カメラ正面方向取得

            CameraForward = Camera.main.transform.TransformDirection(Vector3.forward);
            //カメラ横方向取得
            CameraRight = Camera.main.transform.TransformDirection(Vector3.right);
        



        if (flagsInStageManager.gameClear == false && flagsInStageManager.gameOver == false && flagsInStageManager.talkMode != 0)
        {
            if (playerInAirFlag == false)
            {
                jumpCount = 0;
                moveDirectioninAir = 0.0f;      //重力値を初期化

            }
            else
            {

                moveDirectioninAir -= gravity;  //重力値を加算

            }

            if (moveDirectioninAir > 0.0f)
            {
                playerInAirFlag = true;
            }

            Vector3 playerRemovePos = transform.position;
            //移動
            //     moveDirection += (moveSpeed * Input.GetAxis("Horizontal")) * CameraRight + (moveSpeed * Input.GetAxis("Vertical")) * CameraForward;

            moveDirection += (moveSpeed * Input.GetAxis("Horizontal")) * CameraRight + (moveSpeed * Input.GetAxis("Vertical")) * CameraForward;

            if (Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f)
            {
                pushKeyFlag = true;
            }

            //Debug.Log(Input.GetAxis("Horizontal"));

            //y軸に移動値が影響しないように
            moveDirection.y = 0.0f;
            if (pushKeyFlag == true)
            {

                //   moveDirection += (moveDirection.normalized * moveSpeed) / 50;
              //  animator.SetBool("isRunning", true); //走るアニメーションオン
            }
            else
            {
              //  animator.SetBool("isRunning", false); //走るアニメーションオフ
            }

            //慣性制限
            if (moveDirection.magnitude > moveMaxInAir)
            {
                if (moveDirection.magnitude < moveDirectionMagnitudeRe1fInAir) addDirectionMaxInAir = moveDirection.magnitude;
                moveDirection = moveDirection.normalized * addDirectionMaxInAir;

            }
            else
            {
                addDirectionMaxInAir = moveDirection.magnitude;
                moveDirection = moveDirection.normalized * addDirectionMaxInAir;
            }

            moveDirectionMagnitudeRe1fInAir = moveDirection.magnitude;

            //addDirectionMaxInAir = moveDirection.magnitude;

            if (pushKeyFlag == true)
            {
                Quaternion q = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 20.0f);
            }



            moveDirection.y = moveDirectioninAir;
            isPlayerController.Move(moveDirection);

            //y軸に移動値が影響しないように
            moveDirection.y = 0.0f;
            addDirectionMaxInAir = moveDirection.magnitude;

            // transform.localPosition = transform.localPosition.normalized;
            moveDirection = Vector3.zero;
            if (moveDirectioninAir > jumppow) moveDirectioninAir = jumppow;

            airFlagProcess();
        }
    }

    GameObject nearEnemySearch()
    {
        float targetDistance = 0.0f;        //ターゲット距離
        float nearDistance = 0.0f;          //一番近いターゲット距離

        GameObject targetEnemyObj = null;                                      //一番近い敵オブジェクト
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");   //マップ上の敵を探す

        foreach(GameObject targetOnceEnemy in enemys)//targetOnceEnemyに敵IDを順に代入
        {
            //距離調べるよ
            targetDistance = Vector3.Distance(targetOnceEnemy.transform.position, transform.position);
            //一番近い距離をnearDistanceに入れるよ
            if(nearDistance == 0 || nearDistance > targetDistance)
            {
                nearDistance = targetDistance;
                targetEnemyObj = targetOnceEnemy;
            }

            targetOnceEnemy.GetComponent<EnemyMove>().colNum = 0;
        }
        Debug.Log("一番近い敵" + targetEnemyObj);//ほげぇ
        return targetEnemyObj;

    }


    void airFlagProcess()
    {

        RaycastHit underHit;

        if (moveDirectioninAir < 0.0f && Physics.Raycast(transform.position, new Vector3(0.0f, -1.0f, 0.0f), out underHit, 0.7f, LayerMask.GetMask("Stage")))
        {
            isPlayerController.Move(new Vector3(0.0f, underHit.point.y - transform.position.y, 0.0f));
            playerInAirFlag = false;

        }

    }
}
