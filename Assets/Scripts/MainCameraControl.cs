using UnityEngine;
using System.Collections;

public class MainCameraControl : MonoBehaviour {


    CameraControl cameraDammyObj;

    FlagsInStageManager flagsInStageManager;
 
    //カメラダミー正面方向取得
    private Vector3 CameraForward = new Vector3(0.0f, 0.0f, 0.0f);
    //カメラダミー横方向取得
    private Vector3 CameraRight = new Vector3(0.0f, 0.0f, 0.0f);

    private float stageRiseY = 0.0f;        //地面のY座標
    public float stageRiseAddY = 1.5f;      //地面からの高さ
    public float stageRiseRotateY = 1.5f;   //傾きの値
    public float CameraDownDirection = 1.0f;    //プレイヤーからズラすカメラの距離

    private Vector3 cameraPosPrevious = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 battleCameraPosPrevious = new Vector3(0.0f, 0.0f, 0.0f);
    //プレイヤー情報取得
    PlayerMove player;

    private float cameraDistance = 30.0f;                           //カメラの距離
    [Range(0.0f, 50.0f)]
    public float moveCameraDistance = 30.0f;                        //通常時のカメラ距離
    [Range(0.0f, 20.0f)]
    public float battleCameraDistance = 10.0f;
    private Vector3 cameraMoveDirection = new Vector3(0.0f, 0.0f, 0.0f);

    public Vector3 cameraPos;

    public Vector3 CameraRotate;
    public Vector3 CameraMoveRotate = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 rayDirection =new Vector3(0.0f,-1.0f,0.0f);

    public Vector3 battleCameraX = new Vector3(0.0f, 0.0f, 0.0f);
    
    private int battleEndTime = 0;


    public float cameraDistanceMin = 0.2f;
    public float cameraDistanceMax = 5.0f;

    public float cameraBattleDistance;
    public float trueCameraDistance;
    float playerTargetDistance;

    // Use this for initialization
    void Start()
    {
        cameraDammyObj = GameObject.Find("CameraDammy").GetComponent<CameraControl>();
        flagsInStageManager = GameObject.Find("GameControlObject").GetComponent<FlagsInStageManager>();
        player = GameObject.Find("player").transform.GetComponent<PlayerMove>();
        cameraPos = Camera.main.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

 

        if (flagsInStageManager.batleMode == false)
        {
            //カメラダミー正面方向取得
            CameraForward = cameraDammyObj.transform.TransformDirection(Vector3.forward);
            //カメラダミー横方向取得
            CameraRight = cameraDammyObj.transform.TransformDirection(Vector3.right);


            //カメラをダミーの位置へ持っていく
            Vector3 cameraMovePos = cameraDammyObj.transform.position;

            cameraDistance = moveCameraDistance;



            cameraDistance = moveCameraDistance;
            //カメラの距離を設定
            cameraMovePos -= cameraDistance / 5.0f * CameraForward;
            //cameraPos.y -= swingingCameraYposDown ;


            //カメラの傾き調整
           // CameraMoveRotate = cameraDammyObj.transform.localEulerAngles;
            // CameraRotate = cameraDammyObj.transform.localEulerAngles;
            float rad = cameraDammyObj.transform.position.y - transform.position.y;
            // Debug.Log(rad);
            CameraMoveRotate.x -= Mathf.RoundToInt(rad * stageRiseRotateY);
            // CameraRotate.x -= Mathf.RoundToInt(rad * stageRiseRotateY);

         //   CameraRotate = CameraMoveRotate;


            transform.localEulerAngles = CameraRotate;


            //カメラの高さ調整
            RaycastHit floorHit;
            // float overDistance = 0.0f;
            if (Physics.Raycast(cameraPos, rayDirection, out floorHit, LayerMask.GetMask("Stage")))
            {

                stageRiseY = floorHit.point.y + stageRiseAddY;
                if (player.transform.position.y - CameraDownDirection > stageRiseY) stageRiseY = player.transform.position.y - CameraDownDirection;
            }
            else
            {
                stageRiseY = player.transform.position.y - CameraDownDirection;
            }


            cameraMovePos.y = stageRiseY;

            cameraPos += (cameraMovePos - cameraPos) * 0.05f;
            transform.position = cameraPos;
        }

        
        if (flagsInStageManager.batleMode == false)
        {
            //ダミーからカメラの方向を取得
            Vector3 heading = transform.position - cameraDammyObj.transform.position;
        Vector3 direction = heading / heading.magnitude;

      
            RaycastHit camHit;


            if (Physics.Raycast(cameraDammyObj.transform.position, direction, out camHit, cameraDistance / 5.0f, LayerMask.GetMask("Stage")))
            {

                transform.position = camHit.point;

            }

            cameraPos = transform.position;
            /*
                    if (transform.position.y - player.transform.position.y > Mathf.Abs(0.8f))
                    {
                        cameraPos.y = cameraPosPrevious.y;
                    } 
                    */
            transform.position = cameraPos;
            cameraPosPrevious = transform.position;
        }
      //Debug.Log(CameraForward);

        //バトルカメラワーク設定。後できれいにします

        if (flagsInStageManager.batleMode == true)
        {
            transform.localRotation = new Quaternion(0.0f, -180.0f, 0.0f,0.0f);

            playerTargetDistance = Vector3.Distance(player.transform.position, player.targetEnemyPosition.transform.position);

            cameraDistance = playerTargetDistance * 1.8f;
            if (cameraDistance < cameraDistanceMin) cameraDistance = cameraDistanceMin;
            if (cameraDistance > cameraDistanceMax) cameraDistance = cameraDistanceMax;

            trueCameraDistance += (cameraDistance - trueCameraDistance) * 0.1f;

            Vector3 cameraBattlePos = new Vector3(0.0f, 0.0f, trueCameraDistance);

            transform.localPosition = cameraBattlePos;

          //  transform.position = cameraPos;

            /*
            battleEndTime = 0;

            Vector2 playerScreenPos = Camera.main.WorldToScreenPoint(player.transform.position);
            Vector2 enemyScreenPos = Camera.main.WorldToScreenPoint(player.targetEnemyPosition.transform.position);
            float playerEnemyDistance = Vector3.Distance(playerScreenPos, enemyScreenPos) * 0.15f;
            // if (playerEnemyDistance >= 10) playerEnemyDistance = 10;
            // if (playerEnemyDistance <= 5) playerEnemyDistance = 5;
            // playerEnemyDistance = 10.0f;
            Vector3 cameraMoveEndPos = new Vector3(0.0f, 0.0f, 0.0f);
            cameraMoveEndPos = cameraPos;
            float Distance = Vector3.Distance(player.targetEnemyPosition.transform.position, player.transform.position);
            //Debug.Log(playerEnemyDistance);
            if (Distance > 0)
            {
                cameraMoveEndPos = player.transform.position;
            }
            cameraMoveEndPos -= playerEnemyDistance * (CameraForward);
            cameraMoveEndPos.y = cameraPosPrevious.y;

            Vector3 cameraMovePos = (cameraMoveEndPos - cameraPos) * 0.05f;

            float cameraMoveEndY = cameraPosPrevious.y + 5.0f;
            float cameraMoveY = (cameraMoveEndY - cameraPos.y) * 0.1f;

            //バトルカメラの距離を設定
            //cameraPos += cameraMovePos * 0.5f;
            //cameraPos.y += cameraMoveY;

           // cameraPos.z = -cameraDistance / 5.0f;

            battleCameraPosPrevious = transform.position;

            Vector3 bubo = Vector3.zero;


            bubo.z = cameraDistance / 2.0f;

            transform.localPosition = bubo; 


            //transform.LookAt(cameraBattleRotate);
            //Quaternion.Slerp(transform.rotation, player.targetEnemyPosition.transform.rotation, 1.0f);
            // transform.localEulerAngles = cameraBattleRotate;
            if (player.guardFlag == false)
            {
                
                  
                //CameraRotate.y = transform.localEulerAngles.y - playerEnemyDistance2;
                
            }
          */
        }




    }
}
