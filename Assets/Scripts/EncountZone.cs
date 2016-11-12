using UnityEngine;
using System.Collections;

public class EncountZone : MonoBehaviour {

    public GameObject[] enemy;
    FlagsInStageManager flagsInStageManager;
    private Vector3 pos = new Vector3(0.0f,0.0f,0.0f);
    private GameObject childObject;
    private Vector3 childPos = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 distancePos;
    private Vector3 distancePosQuarter;
    private float apeearX = 0.0f;
    private float apeearZ = 0.0f;
    public EncountPopUp encPop;
    bool EncFlag = true;
    bool BattleCount = false;
    GameManager gm;
    public int EncountNumber = 0;

    GameObject mainCam;
    GameObject battleCam;

    GameObject canvasTargetControlObj;
    // Use this for initialization
    void Start () {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        encPop = GameObject.Find("Encount").GetComponent<EncountPopUp>();
        pos = transform.position;
        flagsInStageManager = GameObject.Find("GameControlObject").GetComponent<FlagsInStageManager>();
        canvasTargetControlObj = GameObject.Find("GameControlObject");
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        battleCam = GameObject.Find("BattleCamera");


        childObject = gameObject.transform.FindChild("DistanceCheck").gameObject;
        childPos = childObject.transform.position;
        distancePos = (childPos - pos) * 2;
        distancePosQuarter = (childPos + (distancePos / 2));
        EncFlag = true;
	}
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.8f, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, distancePos);
    }
    // Update is called once per frame
    void Update () {

        Collider[] hit = Physics.OverlapBox(transform.position, distancePos);
      //  Debug.Log(distancePos.x);
      if(EncFlag == false && flagsInStageManager.batleMode == false && BattleCount == false)
        {
            BattleCount = true;
            gm.stateCount = EncountNumber;
            Destroy(this.gameObject);
        }
        
	}

    void OnTriggerEnter(Collider col)
    {
       if(col.gameObject.tag == ("Player") && EncFlag == true && EncountNumber - gm.stateCount == 1)
        {
            Debug.Log("hit");
            EncFlag = false;
            canvasTargetControlObj.SendMessage("canvasTargetAppear");
            flagsInStageManager.batleMode = true;
            encPop.encOn = true;
            EnemyEncounter();
            PlayerMove playerMove = col.GetComponent<PlayerMove>();
            playerMove.encountPos = transform.position;
        }
      //  Debug.Log("これはいい大根");
    }


    void EnemyEncounter()
    {
        int i = 0;

        float MaxX = childPos.x - distancePos.x;
        float MaxZ = childPos.z - distancePos.z;
        Debug.Log(MaxX);
        foreach(GameObject appearEnemy in enemy)
        {

            apeearX = Random.Range(childPos.x,MaxX);
            apeearZ = Random.Range(childPos.z,MaxZ);
            GameObject.Instantiate(appearEnemy, new Vector3(apeearX, pos.y + 0.05f, apeearZ), Quaternion.identity);
            i++;
        }
        
    }
}
