using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastMovement : MonoBehaviour {

	public GameObject player;
	public GameObject raycastHolder;
	public GameObject raycastIndicator;
	public GvrAudioSource movementSound;

	public float height = 1.9f;
	public bool teleport = false;

	public float maxMoveDistance = 10;
	public float moveTime = 5f;

	private bool moving = false;

	RaycastHit hit;
	float raycastIndicatorHeight = 0.2f;

	MovementBehavior movementBehavior = new MovementBehavior ();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 forwardDir = raycastHolder.transform.TransformDirection (Vector3.forward) * 100;

		// Debug.Log (forwardDir.ToString());
		// Debug.DrawRay (raycastHolder.transform.position, forwardDir, Color.cyan);

		if (Physics.Raycast (raycastHolder.transform.position, (forwardDir), out hit)) {

			// Debug.Log (hit.collider.gameObject.tag);

			if (hit.collider.gameObject.tag == "movementCapable") {

				ManageIndicator (hit.point);
				if (hit.distance <= maxMoveDistance) {
					
					if (raycastIndicator.activeSelf == false) {
						raycastIndicator.SetActive (true);
					}

					if (Input.GetMouseButtonDown (0)) {
						movementSound.Play ();
						if (teleport) {
							teleportMove (hit.point);
						} else {
							Vector3 moveTo = hit.point;
							moveTo = new Vector3 (moveTo.x, moveTo.y + height, moveTo.z);
							movementBehavior.Move (player.transform.position, moveTo);
						}
					}
				} else {
					if (raycastIndicator.activeSelf == true) {
						raycastIndicator.SetActive (false);
					}
				}
			}
		}

	}

	public void ManageIndicator (Vector3 raycastIndicatorPosition) {
		
		if (!teleport) {
			if (!moving) {
				raycastIndicator.transform.position = new Vector3 (
					raycastIndicatorPosition.x, raycastIndicatorPosition.y + raycastIndicatorHeight, raycastIndicatorPosition.z);
			}
			if (Vector3.Distance (raycastIndicator.transform.position, player.transform.position) <= 2) {
				moving = false;
			}

		} else {
			
			raycastIndicator.transform.position = new Vector3 (
				raycastIndicatorPosition.x, raycastIndicatorPosition.y + raycastIndicatorHeight, raycastIndicatorPosition.z);
		}
	}

	/*
	public void DashMove (Vector3 location) {
		location = new Vector3 (location.x, location.y + height, location.z);
		player.transform.position = Vector3.Lerp (player.transform.position, location, moveTime * 100);
	}
	*/

	public void teleportMove (Vector3 location) {
		player.transform.position = new Vector3 (location.x, location.y + height, location.z);
	}
}
