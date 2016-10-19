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
    // Use this for initialization
    void Start () {
        pos = transform.position;
        flagsInStageManager = GameObject.Find("GameControlObject").GetComponent<FlagsInStageManager>();
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
        Debug.Log(distancePos.x);

        
	}

    void OnTriggerEnter(Collider col)
    {
       if(col.gameObject.tag == ("Player"))
        {
            flagsInStageManager.batleMode = true;
            EnemyEncounter();
            
            Destroy(this.gameObject);
        }
      //  Debug.Log("これはいい大根");
    }


    void EnemyEncounter()
    {
        int i = 0;

        float MaxX = childPos.x + Mathf.Abs(distancePos.x);
        float MaxZ = childPos.z + Mathf.Abs(distancePos.z);

        foreach(GameObject appearEnemy in enemy)
        {

            apeearX = Random.Range(childPos.x,MaxX);
            apeearZ = Random.Range(childPos.z,MaxZ);
            GameObject.Instantiate(appearEnemy, new Vector3(apeearX, pos.y + 0.05f, apeearZ), Quaternion.identity);
            i++;
        }
        
    }
}
