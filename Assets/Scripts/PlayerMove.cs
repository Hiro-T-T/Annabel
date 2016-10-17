using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

    public float moveSpeed = 0.3f;                                              //地上時移動速度
    public float movePowInAir = 0.02f;                                          //空中での移動にかかる強さ
    public float moveMaxInAir = 0.3f;                                           //空中での感性限界値
    public float gravity = 0.02f;                                               //重力値
    public float jumppow = 1.0f;												//ジャンプ力
    public float moveFrictionPow = 0.3f;




    bool playerInAirFlag = true;

    private int jumpCount = 5;
    FlagsInStageManager flagsInStageManager;

    private Animator animator;
    private Vector3 moveDirectionMaxInAir = new Vector3(0.0f, 0.0f, 0.0f);  //空中慣性制限用
    public Vector3 moveDirection = new Vector3(0.0f, 0.0f, 0.0f);          //移動速度格納変数
    public float moveDirectioninAir = 0.0f;									//y軸移動用変数 
    private Vector3 moveDirection_y = new Vector3(0.0f, 0.0f, 0.0f);

    public CharacterController isPlayerController;                              //キャラクターコントローラー
    public Rigidbody rigidBodyInfo;                                         //リジッドボディ呼び出し用変数

    public float addDirectionMaxInAir = 0.0f;                                  //制限超過分考慮した空中慣性制限
    private float moveDirectionMagnitudeRe1fInAir = 0.0f;                       //1フレーム前のmoveDirection.magunitude
    private float addDirectionMax = 0.0f;                                       //制限超過分考慮した地上慣性制限
    private float moveDirectionMagnitudeRe1f = 0.0f;							//1フレーム前のmoveDirection.magunitude

    //カメラ正面方向取得
    private Vector3 CameraForward;
    //カメラ横方向取得
    private Vector3 CameraRight;
    //カメラダミーの情報取得
    private GameObject cameraDammyObj;
    void Start()
    {
        rigidBodyInfo = GetComponent<Rigidbody>();
        cameraDammyObj = GameObject.Find("CameraDammy");
        isPlayerController = gameObject.GetComponent<CharacterController>();
        flagsInStageManager = GameObject.Find("GameControlObject").GetComponent<FlagsInStageManager>();
        animator = GetComponent<Animator>();
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

                //地上時の操作
                //   moveControl(0);

            }
            else
            {


                moveDirectioninAir -= gravity;  //重力値を加算

                //空中時の操作

            }

            if (moveDirectioninAir > 0.0f)
            {
                playerInAirFlag = true;
            }

            Vector3 playerRemovePos = transform.position;

            moveDirection += (moveSpeed * Input.GetAxis("Horizontal")) * CameraRight + (moveSpeed * Input.GetAxis("Vertical")) * CameraForward;

            if (Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f)
            {
                pushKeyFlag = true;
            }
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


            //y軸に移動値が影響しないように
            moveDirection.y = 0.0f;
            addDirectionMaxInAir = moveDirection.magnitude;

            // transform.localPosition = transform.localPosition.normalized;
            moveDirection = Vector3.zero;
            if (moveDirectioninAir > jumppow) moveDirectioninAir = jumppow;

            airFlagProcess();
        }
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
