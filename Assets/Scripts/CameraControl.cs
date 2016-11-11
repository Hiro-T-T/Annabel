using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
    public Vector3 dammyCampos = new Vector3(0.0f, 0.0f, 0.0f);     //カメラ座標	
    public Vector3 dammyCamRotate = new Vector3(0.0f, 180.0f, 0.0f);  //カメラ回転値
    public float dammyCamRotateSpeed = 4.0f;                            //カメラ回転スピード
    public Vector3 trueDammyCamRotate = Vector3.zero;                   //こっちがゲームへ反映するカメラ回転値
    public float camRotateDig = 0.5f;
    FlagsInStageManager flagsInStageManager;
    public Vector3 targetCamPos = new Vector3(0.0f, 0.0f, 0.0f);    //カメラのターゲットの座標

    Vector3 dammyBattleCampos;

    public float rotateSpeed = 0.5f;                                //カメラの回転スピード
    public float moveSpeed = 4.0f;                                  //カメラの移動スピード（低いほど早くなる）
    public float xRotateMax = 70.0f;                                //Z軸回転限界角度

    Vector3 mousepos2 = Vector3.zero;                               //1f前のマウス座標
    Vector3 mousepos = Vector3.zero;                                //現在のマウス座標

    PlayerMove player;                                              //プレイヤーオブジェクト取得
    Vector3 CameraTargetPos;
    MainCameraControl MainCamera;
    public float lookAngle = 0.0f;
    public float angle = 0.0f;
    public float angle2 = 0.0f;

    float moveSpeedBattle = 0.05f;
    int battleCameraReadyTime = 0;
    bool returnFlag = false;
    Vector3 CameraAngle;
    public GameObject PlayerObj = GameObject.FindGameObjectWithTag("Player");
    public GameObject MainCameraObj = GameObject.FindGameObjectWithTag("MainCamera");

    // Use this for initialization
    void Start()
    {
        dammyCampos = transform.position;
        player = PlayerObj.transform.GetComponent<PlayerMove>();
        mousepos = Input.mousePosition;
        mousepos2 = mousepos;
        flagsInStageManager = GameObject.Find("GameControlObject").GetComponent<FlagsInStageManager>();
        MainCamera = MainCameraObj.transform.GetComponent<MainCameraControl>();
        CameraAngle = Vector3.zero;
        Application.targetFrameRate = 60;
       

    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
      

        if (flagsInStageManager.gameClear == false && flagsInStageManager.gameOver == false && flagsInStageManager.talkMode != 0)
        {
            

            if(flagsInStageManager.batleMode == false)
            {
                battleCameraReadyTime = 0;
                targetCamPos = player.transform.position;
                dammyCampos = Vector3.Lerp(dammyCampos, targetCamPos, moveSpeed);
                targetCamPos.x = player.transform.position.x;
                targetCamPos.y = player.transform.position.y;
                targetCamPos.z = player.transform.position.z;
                //  dammyCampos = Vector3.Lerp(dammyCampos, new Vector3(targetCamPos.x, targetCamPos.y + 0.2f, targetCamPos.z), moveSpeed);
                dammyCampos = Vector3.Lerp(dammyCampos, new Vector3(targetCamPos.x, targetCamPos.y + 0.2f, targetCamPos.z), moveSpeed);
            }
            else
            {
                battleCameraReadyTime ++;

                if (battleCameraReadyTime > 10)
                {
                    if (player.pushKeyFlag == true)
                    {
                        moveSpeedBattle = 0.010f;
                    }
                    else
                    {
                        moveSpeedBattle = 0.08f;
                    }




                    CameraTargetPos = player.targetEnemyPosition.transform.position;

                    dammyBattleCampos += (CameraTargetPos - dammyBattleCampos) * moveSpeedBattle;

                    transform.position = dammyBattleCampos;
                }
                if (battleCameraReadyTime < 10)
                {
                    transform.position = player.targetEnemyPosition.transform.position;
                    dammyBattleCampos = transform.position;
                }
                angle = Mathf.Atan2(player.transform.position.z - player.targetEnemyPosition.transform.position.z, player.transform.position.x - player.targetEnemyPosition.transform.position.x) - 1.57f;
                Debug.Log( battleCameraReadyTime );

                //回転値の調整
                returnFlag = false;
                if (Mathf.Abs(lookAngle - angle2) >= 3.14f)
                {

                    returnFlag = true;

                }

                if (lookAngle > -angle + 0.2f)
                {
                    angle2 = -angle + 0.2f;
                }

                if (lookAngle < -angle - 0.2f)
                {
                    angle2 = -angle - 0.2f;

                }

                float keepAngle2 = angle2;

                if (returnFlag == true)
                {

                    if (-angle < 0.0f)
                    {

                        angle2 = -angle + 6.28f - 0.2f;
                        //angle2 %= 6.28f ;
                        //lookAngle += ( angle2 - lookAngle )*moveSpeed ;
                        //lookAngle %= 6.28f ;
                        Debug.Log("消えかかっただいこん" + lookAngle + " " + angle2);

                    }
                    else if (-angle > 0.0f)
                    {

                        angle2 = -angle - 6.28f - 0.2f;
                        //angle2 %= 6.28f ;
                        //lookAngle += ( angle2 - lookAngle )*moveSpeed ;
                        //lookAngle %= 6.28f ;
                        Debug.Log("消えかかれなかっただいこん" + lookAngle + " " + angle2);
                    }

                }

                lookAngle += (angle2 - lookAngle) * moveSpeedBattle;
                lookAngle %= 6.28f;

                angle2 = keepAngle2;

                CameraAngle = new Vector3(-5.0f - MainCamera.trueCameraDistance * 0.4f, 360.0f / 6.28f * lookAngle, 0.0f);
                transform.eulerAngles = CameraAngle;

            }

            if (flagsInStageManager.batleMode == false)
            {




                //座標適用
                transform.position = dammyCampos;

                //カメラ（ダミー）の回転処理===============================================================

                //マウスの座標を適用
                //   mousepos2 = mousepos;
                //   mousepos = Input.mousePosition;


                //回転
                dammyCamRotate.y += (Input.GetAxis("Horizontal") * rotateSpeed) + (Input.GetAxis("Mouse X") * rotateSpeed * 2.0f);

                //dammyCamRotate.x -= Input.GetAxis("Horizontal") * rotateSpeed;

                //   if (Mathf.Abs(Input.GetAxis("Mouse XD")) > 0.5f) dammyCamRotate.y -= Input.GetAxis("Mouse XD") * rotateSpeed;
                //    if (Mathf.Abs(Input.GetAxis("Mouse YD")) > 0.5f) dammyCamRotate.x -= Input.GetAxis("Mouse YD") * rotateSpeed;



                //z軸回転限界処理
                if (dammyCamRotate.x < -xRotateMax) dammyCamRotate.x = -xRotateMax;
                if (dammyCamRotate.x > xRotateMax) dammyCamRotate.x = xRotateMax;

                trueDammyCamRotate = dammyCamRotate / camRotateDig; //+= ( dammyCamRotate - trueDammyCamRotate )/dammyCamRotateSpeed ;
                trueDammyCamRotate = Vector3.Lerp(trueDammyCamRotate, dammyCamRotate, dammyCamRotateSpeed);


                //回転値適用
                transform.eulerAngles = trueDammyCamRotate;

                /* if(flagsInStageManager.batleMode == true)
                 {
                     EnemyTargetPosMove enemyTargetPosMove = GameObject.Find("EnemyTargetPos").GetComponent<EnemyTargetPosMove>();
                     transform.LookAt(new Vector3(enemyTargetPosMove.transform.position.x, player.targetEnemyPosition.transform.position.y, enemyTargetPosMove.transform.position.z));
                 }
                 */
            }
        }
    }
}
