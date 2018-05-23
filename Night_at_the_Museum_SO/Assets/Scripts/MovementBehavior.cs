using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehavior : MonoBehaviour {

	public GameObject player;                       // the player object, GvrEditorEmulator
	public float speed = 40f;                       // speed constant which affects currentDuration

	private static Vector3 startPosition;           // Vector3 coordinates of starting position
    private static Vector3 endPosition;             // Vector3 coordinates of destination position

	private static bool wantToMove = false;         // Indicates when we should be moving with LERP
	private static float startTime;                 // Time when the Move fuction is first called
	private static float distance;                  // Distance to be traveled
    private static float journeyFraction;           // Seconds elapsed divided by the distance to be traveled: currentDuration/distance

    // Moves the player through 3D space to a destination via Lerp, which linearly interpolates bewteen two Vector3s
    // This function is called from RaycastMovement.cs, and passes two Vector3 parameters
	public void Move (Vector3 start, Vector3 end) {
        RaycastMovement.moving = true;      // In our RaycastMovement class, we set the bool moving as true
		startPosition = start;
		endPosition = end;

		startTime = Time.time;
        distance = Vector3.Distance (startPosition, endPosition);

		wantToMove = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (wantToMove) {
			
			//Debug.Log (journeyFraction.ToString() + " " + wantToMove.ToString());

			float currentDuration = (Time.time - startTime) * speed;        // The time elapsed between starting and the current time, multiplied by a speed constant
			journeyFraction = currentDuration / distance;
			player.transform.position = Vector3.Lerp (startPosition, endPosition, journeyFraction);
		}
		
        // journeyFraction is only >= 1 when currentDuration = distance, in other words, when the player has arrived at the destination
		if (journeyFraction >= 1) {
			wantToMove = false;
            RaycastMovement.moving = false;     // We are finished moving, so we can allow the player to move the raycast indicator
		}
	}	
}
