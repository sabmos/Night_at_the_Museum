using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneMenu : MonoBehaviour {

    public GameObject mainCamera;
    public GameObject changeScenesMenu;
    public float degreesToViewMenu = 50f;

    private Quaternion currentRotation;
    private float rotationXEuler;
    private float rotationYEuler;
    private float rotationZEuler;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        currentRotation = mainCamera.transform.rotation;
        // Debug.Log("current rotation = " + currentRotation.eulerAngles);

        if (currentRotation.eulerAngles.x >= degreesToViewMenu && 
           changeScenesMenu.activeSelf == false) {

            // Identify the correct Euler values of where I want the menu to be
            rotationXEuler = changeScenesMenu.transform.eulerAngles.x;
            rotationYEuler = mainCamera.transform.eulerAngles.y;
            rotationZEuler = changeScenesMenu.transform.eulerAngles.z;

            // Create newRotation Quaternion and assign it to 
            Quaternion newRotation = Quaternion.Euler(rotationXEuler, rotationYEuler, rotationZEuler);
            // Debug.Log("all rotation " + newRotation.ToString());
            changeScenesMenu.transform.rotation = newRotation;

            // Activate menu
            //OpenMenu();
            changeScenesMenu.SetActive(true);

        // else close the menu
        } else if (currentRotation.eulerAngles.x < degreesToViewMenu && 
                   changeScenesMenu.activeSelf == true) {
            //CloseMenu();
            changeScenesMenu.SetActive(false);
        }
	}

    // Runs the animation to open the "change scenes" menu
    public void OpenMenu () {
        
    }

    // Runs the animation to close the "change scenes" menu
    public void CloseMenu () {
        
    }
}
