using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OperatingManager : MonoBehaviour {
    
    Button ok;

    void Start()
    {
        GameManager.sceneNumber = 3;
        ok = GameObject.Find("/Canvas/Button/").GetComponent<Button>();

        ok.Select();
    }

    void Update()
    {
        if(Input.GetAxis("Attack") == 1)
        {
            SceneManager.LoadScene(2);
        }
    }

    public void OnClick()
    {
        SceneManager.LoadScene(2);
    }
}
