using UnityEngine;
using System.Collections;

public class EncountZone : MonoBehaviour {
    private AudioSource se;
    public AudioClip sound;
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
    public int encountNumber = 0;
    public int stageNumber = 1;

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
        se = gameObject.GetComponent<AudioSource>();
        se.loop = false;
    
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
            if(gm.battleCount >= 2)
            {
                gm.stateCount = 4;
            }
            if(stageNumber == 1)
            {
                gm.battleCount = encountNumber;
            }
            Destroy(this.gameObject);
        }
        
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == ("Player") && EncFlag == true && encountNumber - gm.stateCount == 1)
        {
            Debug.Log("hit");
            EncFlag = false;
            canvasTargetControlObj.SendMessage("canvasTargetAppear");
            flagsInStageManager.batleMode = true;
            encPop.encOn = true;
            EnemyEncounter();
            se.PlayOneShot(sound);
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
            GameObject.Instantiate(appearEnemy, new Vector3(apeearX, appearEnemy.transform.position.y, apeearZ), Quaternion.identity);
            i++;
        }
        
    }
}
