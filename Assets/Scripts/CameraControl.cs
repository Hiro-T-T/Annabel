using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
    public Vector3 dammyCampos = new Vector3(0.0f, 0.0f, 0.0f);     //カメラ座標	
    public Vector3 dammyCamRotate = new Vector3(0.0f, 0.0f, 0.0f);  //カメラ回転値
    public float dammyCamRotateSpeed = 4.0f;                            //カメラ回転スピード
    public Vector3 trueDammyCamRotate = Vector3.zero;                   //こっちがゲームへ反映するカメラ回転値
    public float camRotateDig = 0.5f;
    FlagsInStageManager flagsInStageManager;
    public Vector3 targetCamPos = new Vector3(0.0f, 0.0f, 0.0f);    //カメラのターゲットの座標

    public float rotateSpeed = 0.5f;                                //カメラの回転スピード
    public float moveSpeed = 4.0f;                                  //カメラの移動スピード（低いほど早くなる）
    public float xRotateMax = 70.0f;                                //Z軸回転限界角度

    Vector3 mousepos2 = Vector3.zero;                               //1f前のマウス座標
    Vector3 mousepos = Vector3.zero;                                //現在のマウス座標

    PlayerMove player;                                              //プレイヤーオブジェクト取得
                                                                    // Use this for initialization
    void Start()
    {
        dammyCampos = transform.position;
        player = GameObject.Find("player").GetComponent<PlayerMove>();
        mousepos = Input.mousePosition;
        mousepos2 = mousepos;
        flagsInStageManager = GameObject.Find("GameControlObject").GetComponent<FlagsInStageManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
         targetCamPos = player.transform.position;
         dammyCampos = Vector3.Lerp(dammyCampos, targetCamPos, moveSpeed);
     
        if (flagsInStageManager.gameClear == false && flagsInStageManager.gameOver == false && flagsInStageManager.talkMode != 0)
        {
            targetCamPos.x = player.transform.position.x;
            targetCamPos.y = player.transform.position.y;
            targetCamPos.z = player.transform.position.z;
            dammyCampos = Vector3.Lerp(dammyCampos, new Vector3(targetCamPos.x, targetCamPos.y + 0.2f, targetCamPos.z), moveSpeed);


            //座標適用
            transform.position = dammyCampos;

            //カメラ（ダミー）の回転処理===============================================================

            //マウスの座標を適用
            //   mousepos2 = mousepos;
            //   mousepos = Input.mousePosition;


            //マウスの移動値で回転させる
            dammyCamRotate.y += Input.GetAxis("Horizontal") * rotateSpeed;
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
        }
    }
}
