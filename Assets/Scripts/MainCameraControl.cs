using UnityEngine;
using System.Collections;

public class MainCameraControl : MonoBehaviour {


    CameraControl cameraDammyObj;

    //カメラダミー正面方向取得
    private Vector3 CameraForward = new Vector3(0.0f, 0.0f, 0.0f);
    //カメラダミー横方向取得
    private Vector3 CameraRight = new Vector3(0.0f, 0.0f, 0.0f);

    private float stageRiseY = 0.0f;        //地面のY座標
    public float stageRiseAddY = 1.5f;      //地面からの高さ
    public float stageRiseRotateY = 1.5f;   //傾きの値
    public float CameraDownDirection = 1.0f;    //プレイヤーからズラすカメラの距離

    private Vector3 cameraPosPrevious = new Vector3(0.0f, 0.0f, 0.0f);

    //プレイヤー情報取得
    PlayerMove player;

    private float cameraDistance = 30.0f;                           //カメラの距離
    public float moveCameraDistance = 30.0f;                        //通常時のカメラ距離

    private Vector3 cameraMoveDirection = new Vector3(0.0f, 0.0f, 0.0f);

    private Vector3 cameraPos;

    private Vector3 CameraRotate;

    private Vector3 rayDirection =new Vector3(0.0f,-1.0f,0.0f);

    // Use this for initialization
    void Start()
    {
        cameraDammyObj = GameObject.Find("CameraDammy").GetComponent<CameraControl>();

        player = GameObject.Find("player").GetComponent<PlayerMove>();
        cameraPos = Camera.main.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        //カメラをダミーの位置へ持っていく
        cameraPos = cameraDammyObj.transform.position;

        //カメラダミー正面方向取得
        CameraForward = cameraDammyObj.transform.TransformDirection(Vector3.forward);
        //カメラダミー横方向取得
        CameraRight = cameraDammyObj.transform.TransformDirection(Vector3.right);

        cameraDistance = moveCameraDistance;
        //カメラの距離を設定
        cameraPos -= cameraDistance / 5.0f * CameraForward;
        //cameraPos.y -= swingingCameraYposDown ;

        //カメラの傾き調整
        CameraRotate = cameraDammyObj.transform.localEulerAngles;

        float rad = cameraDammyObj.transform.position.y - transform.position.y;
        Debug.Log(rad);
        CameraRotate.x -= Mathf.RoundToInt(rad * stageRiseRotateY);

        transform.localEulerAngles = CameraRotate;


        //カメラの高さ調整
        RaycastHit floorHit;
        // float overDistance = 0.0f;
        if (Physics.Raycast(cameraPos, rayDirection, out floorHit, LayerMask.GetMask("Stage")))
        {

            stageRiseY = floorHit.point.y + 1.5f;
            if (player.transform.position.y - CameraDownDirection > stageRiseY) stageRiseY = player.transform.position.y - CameraDownDirection;
        }
        else
        {
            stageRiseY = player.transform.position.y - CameraDownDirection;
        }
           

         cameraPos.y = stageRiseY;

        transform.position = cameraPos;

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
}
