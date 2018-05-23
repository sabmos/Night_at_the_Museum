using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuLogic : MonoBehaviour {

    public GameObject buttonsCanvas;
    public GameObject howToCanvas;

	// Use this for initialization
	void Start () {
        if (!SceneChange.firstTimeLoad) {
            buttonsCanvas.SetActive(false);
            howToCanvas.SetActive(true);
        }
	}
}
