using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMovement : MonoBehaviour {

	public GameObject player;
	public GameObject[] waypointSystem;

	public float moveTime = 0.5f;
	public float height = 1.85f;
	public bool teleport = false;

	WaypointBehavior instance;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Move (GameObject waypoint) {
		if (!teleport) {
			iTween.MoveTo (player,
				iTween.Hash (
					"position", new Vector3 (waypoint.transform.position.x, waypoint.transform.position.y + (height / 2), waypoint.transform.position.z),
					"time", moveTime,
					"easetype", "easeInOutQuad"
				)
			);
		} else {
			player.transform.position = new Vector3 (waypoint.transform.position.x,
				waypoint.transform.position.y + height / 2, waypoint.transform.position.z);
		}
		for (int i = 0; i < waypointSystem.Length; i++) {
			waypointSystem [i].SetActive (false);
		}
	}
}
