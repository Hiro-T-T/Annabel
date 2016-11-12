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

    public void OnClick()
    {
        SceneManager.LoadScene(2);
    }
}
