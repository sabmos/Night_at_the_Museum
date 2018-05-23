using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastMovement : MonoBehaviour {

    public GameObject player;                   // The player object, GvrEditorEmulator
	public GameObject raycastHolder;            // Empty GameObject that serves as starting place for our raycaster
    public GameObject raycastIndicator;         // Holds an object that indicates where our raycaster is hitting; particle system
	public GvrAudioSource movementSound;        // The audio source that plays an AudioClip when the user moves

	public float height = 1.9f;                 // The player's height above the floor
    public float maxMoveDistance = 10;          // The max distance that the player is allowed to move in one click
    public bool teleport = false;               // Switches between smooth movement and instant teleportation

	public static bool moving = false;          // Indicates whether or not the player is currently moving

	RaycastHit hit;                             // Structure to get information back from a raycast
	float raycastIndicatorHeight = 0.2f;        // raycastIndicator height above the floor

    MovementBehavior movementBehavior = new MovementBehavior ();        // Instance of the MovementBehavior class, we will use this to call the Move() function

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Create a Vector3 variable that transforms local space to world space, and they extend it by a factor of 100
        // Vector3.foward with a world space unit vector of 0,0,1
        Vector3 forwardDir = raycastHolder.transform.TransformDirection (Vector3.forward) * 100;

        // Debug.Log (forwardDir.ToString());transform.TransformDirection(Vector3.forward)
		// Debug.DrawRay (raycastHolder.transform.position, forwardDir, Color.cyan);

        // Bool that uses Physics.Raycast with origin, direction and hitInfo parameters. Returns true if Raycast hits an object with a collider
        if (Physics.Raycast (raycastHolder.transform.position, (forwardDir), out hit) && !moving) {

			// Debug.Log (hit.collider.gameObject.tag);

            // If the object that our Raycast hit has been tagged "movementCapable"
			if (hit.collider.gameObject.tag == "movementCapable") {
				ManageIndicator (hit.point);        // Calls the ManageIndicator function, and passes a RaycastHit.point as a Vector3

				if (hit.distance <= maxMoveDistance) {
					
					if (raycastIndicator.activeSelf == false) {
						raycastIndicator.SetActive (true);
					}

					if (Input.GetMouseButtonDown (0)) {
						movementSound.Play ();

						if (teleport) {     // if we want to use instant teleport locomotion
							teleportMove (hit.point);
						} else {
							Vector3 moveTo = hit.point;
							moveTo = new Vector3 (moveTo.x, moveTo.y + height, moveTo.z);       // account for player height in position.y
                            movementBehavior.Move (player.transform.position, moveTo);          // Run MovementBehavior class Move() function
						}
					}
				} else {        // if hit.distance is NOT <= maxMoveDistance
					if (raycastIndicator.activeSelf == true) {
						raycastIndicator.SetActive (false);
					}
				}
			}
		}
	}

    // Activate, deactive or move the raycast indicator GameObject based on where the player is looking.
    // Accepts one Vector3 parameter. It is being passed a Vector3 from RaycastHit.point.
	public void ManageIndicator (Vector3 raycastIndicatorPosition) {
		
		if (!teleport) {
			if (!moving) {
                // Move our raycast indicator to where the player is looking
				raycastIndicator.transform.position = new Vector3 (
					raycastIndicatorPosition.x, raycastIndicatorPosition.y + raycastIndicatorHeight, raycastIndicatorPosition.z);
			}
		} else {        // if teleport is enabled, move the raycast indicator position
			raycastIndicator.transform.position = new Vector3 (
				raycastIndicatorPosition.x, raycastIndicatorPosition.y + raycastIndicatorHeight, raycastIndicatorPosition.z);
		}
	}

    // Instantly teleports the player to the a Vector3 location from RaycastHit.point.
    // This option can be selected for users susceptible to simulator sickness
	public void teleportMove (Vector3 location) {
		player.transform.position = new Vector3 (location.x, location.y + height, location.z);
	}
}
