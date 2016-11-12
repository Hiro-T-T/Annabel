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

    GameObject mainCam;
    GameObject battleCam;

    GameObject canvasTargetControlObj;
    // Use this for initialization
    void Start () {
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

        
	}

    void OnTriggerEnter(Collider col)
    {
       if(col.gameObject.tag == ("Player"))
        {
            canvasTargetControlObj.SendMessage("canvasTargetAppear");
            flagsInStageManager.batleMode = true;
            encPop.encOn = true;
            EnemyEncounter();
            PlayerMove playerMove = col.GetComponent<PlayerMove>();
            playerMove.encountPos = transform.position;
            Destroy(this.gameObject);
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
