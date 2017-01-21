using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ripples : MonoBehaviour {

	// Use this for initialization
	public Camera cam;

	float offWave;

	//public GameObject goPlane;
	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera>();
		offWave = 1.0f;
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hitInfo = new RaycastHit();
			bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
			if (hit) 
			{
				Debug.Log ("Hit");
				this.GetComponent<Renderer> ().material.SetInt("_Click",1);
				offWave = 1.0f;
				this.GetComponent<Renderer> ().material.SetFloat ("_DirectionX",hitInfo.point.x);
				this.GetComponent<Renderer> ().material.SetFloat ("_DirectionZ",hitInfo.point.z);
				this.GetComponent<Renderer> ().material.SetFloat ("_OffWave",offWave);

			} 

		} 

		offWave = offWave - (Time.fixedDeltaTime * 1.0f);

		if (offWave < 0.0f)
			offWave = 0.0f;

		this.GetComponent<Renderer> ().material.SetFloat ("_OffWave",offWave);

	}

}
