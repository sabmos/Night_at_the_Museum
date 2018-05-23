using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneMenu : MonoBehaviour {

    public GameObject mainCamera;                   // Main Camera in Unity
    public GameObject changeScenesMenuPivot;        // Parent of the changeSceneMenu, allows us to "revolve" around the player
    public Animator changeScenesMenuAnimator;       // Animator that controlls animations of the menu
    public AnimationClip closeMenuAnimation;        // Animation clip of closing the menu, we need to use the time length of this clip
    public float degreesToViewMenu = 65f;           // Degrees of how far the user can look down before the menu pops up
    private float maxDegreesUp = 274;               /* Degrees while looking straight up minus 1 for buffer.
                                                       From the horizon to directly over the user,the degrees go from 360 to 275 */

    public GvrAudioSource soundSource;              // Sound source for the popup menu
    public AudioClip[] soundFile;

    private Quaternion currentRotation;             // Current rotation of the mainCamera
    private float rotationXEuler;                   // Euler coordinate of x rotation (degrees)
    private float rotationYEuler;                   // Euler coordinate of x rotation (degrees)
    private float rotationZEuler;                   // Euler coordinate of x rotation (degrees)

    private bool playedSound = false;             // Used to limit our CloseMenu coroutine

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
        currentRotation = mainCamera.transform.rotation;
        // Debug.Log("current rotation = " + currentRotation.eulerAngles);

        if (currentRotation.eulerAngles.x >= degreesToViewMenu &&
            currentRotation.eulerAngles.x < maxDegreesUp &&
            changeScenesMenuPivot.activeSelf == false) {

            // Identify the correct Euler values of where I want the menu to be
            rotationXEuler = changeScenesMenuPivot.transform.eulerAngles.x;
            rotationYEuler = mainCamera.transform.eulerAngles.y;
            rotationZEuler = changeScenesMenuPivot.transform.eulerAngles.z;

            // Create newRotation Quaternion and assign it to the newRotation variable,
            // and then assign it to our actual changeScenesMenu trasform.rotation
            Quaternion newRotation = Quaternion.Euler(rotationXEuler, rotationYEuler, rotationZEuler);
            // Debug.Log("all rotation " + newRotation.ToString());
            changeScenesMenuPivot.transform.rotation = newRotation;

            // Activate menu and run opening animation
            OpenMenu();

        // Close the menu, play animation and sound, deactivate menu
        } else if (currentRotation.eulerAngles.x < degreesToViewMenu && 
                   changeScenesMenuPivot.activeSelf == true) {
            StartCoroutine(CloseMenu());

        }
	}

    // Runs the animation to open the "change scenes" menu
    public void OpenMenu () {
        soundSource.clip = soundFile[0];
        soundSource.Play();
        changeScenesMenuPivot.SetActive(true);
        changeScenesMenuAnimator.SetTrigger("OpenMenu");

    }

    // Runs the animation to close the "change scenes" menu
    public IEnumerator CloseMenu () {
        
        changeScenesMenuAnimator.SetTrigger("CloseMenu");
        if (!playedSound) {
            soundSource.clip = soundFile[1];
            soundSource.Play();
            playedSound = true;
        }
        yield return new WaitForSeconds(closeMenuAnimation.length);
        changeScenesMenuPivot.SetActive(false);
        playedSound = false;        // Switch playedSound to false after our WaitForSeconds is complete, so we are ready for the next play
    }
}
