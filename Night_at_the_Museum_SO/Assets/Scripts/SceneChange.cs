using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{

    public static bool firstTimeLoad = true;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log(firstTimeLoad.ToString());
        }
    }

    public void GoToScene(string sceneName)
    {
        // Changes the firstTimeLoad variable to false. This will assist with which menus to show when switching scenes.
        if (firstTimeLoad)
        {
            firstTimeLoad = false;
        }
        SceneManager.LoadScene(sceneName);
    }
}


