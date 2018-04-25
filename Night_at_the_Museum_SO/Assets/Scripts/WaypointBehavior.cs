using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointBehavior : MonoBehaviour {

	public float dynamicScale = 1.2f;
	public float pulseIntensity = 0.5f;
	public float pulseFrequency = 4f;

	public WaypointBehavior[] neighborhood;

	bool isLookingAt = false;

	Vector3 baseScale;
	Color baseColor;
	Color pointerEnterColor = new Color(1f, 0f, 0f, 0.5f);  // Red with 0.5 alpha
	Material waypointMaterial;

	// Use this for initialization
	void Start () {
		waypointMaterial = gameObject.GetComponent<Renderer> ().material;
		baseScale = gameObject.transform.localScale;
		baseColor = gameObject.GetComponent<Renderer> ().material.GetColor ("_Color");
	}
	
	// Update is called once per frame
	void Update () {
		if (isLookingAt) {
			gameObject.transform.localScale = baseScale * (dynamicScale + Mathf.Abs (pulseIntensity * Mathf.Cos (Time.time * pulseFrequency)));
		}
	}

	public void PointerEnter () {
		isLookingAt = true;
		waypointMaterial.SetColor("_Color", pointerEnterColor);
	}

	public void PointerExit () {
		isLookingAt = false;
		gameObject.transform.localScale = baseScale;
		waypointMaterial.SetColor("_Color", baseColor);
	}

	public void Active () {
		gameObject.SetActive (true);
		// The code below is to reset the waypoints to their original state
		gameObject.GetComponent<Renderer> ().material.SetColor ("_Color", baseColor);
		gameObject.transform.localScale = baseScale;
		isLookingAt = false;
	}

	public void Activate(WaypointBehavior waypoints) {
		for (int i = 0; i < waypoints.neighborhood.Length; i++) {
			waypoints.neighborhood[i].Active();
		}
	}
}