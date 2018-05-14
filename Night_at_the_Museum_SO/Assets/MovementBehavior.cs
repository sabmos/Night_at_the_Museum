using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehavior : MonoBehaviour {

	public GameObject player;
	public float speed = 40f;

	private static Vector3 startPosition;
	private static Vector3 endPosition;

	private static bool wantToMove = false;
	private static float startTime;
	private static float distance;
	private static float journeyFraction;

	public void Move (Vector3 start, Vector3 end) {
		startPosition = start;
		endPosition = end;
		startTime = Time.time;

		distance = Vector3.Distance (startPosition, endPosition);
		wantToMove = true;

		// Debug.Log ("called MovementBehavior.Move... great job)");
		// Debug.Log (startPosition.ToString() + " " + endPosition.ToString() + " " + wantToMove.ToString() + " " + startTime.ToString());
	}
	
	// Update is called once per frame
	void Update () {
		
		//Debug.Log (wantToMove.ToString () + " " + startTime.ToString() + " " + endPosition.ToString());

		if (wantToMove) {
			
			//Debug.Log (journeyFraction.ToString() + " " + wantToMove.ToString());

			float currentDuration = (Time.time - startTime) * speed;
			journeyFraction = currentDuration / distance;
			player.transform.position = Vector3.Lerp (startPosition, endPosition, journeyFraction);
		}
			
		if (journeyFraction >= 1) {
			wantToMove = false;
		}
	}

		
}
