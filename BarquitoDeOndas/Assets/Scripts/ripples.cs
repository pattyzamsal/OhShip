using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ripples : MonoBehaviour {

	// Use this for initialization
	public Camera cam;

    private Material _mat;

    public bool turnOff;

	float offWave;

	//public GameObject goPlane;
	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera>();
        _mat = this.GetComponent<Renderer>().material;
        offWave = 1.0f;
	}

	// Update is called once per frame
	void Update () {

        if( turnOff ) return;

		if (Input.GetMouseButtonDown(0))
		{
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast( Camera.main.ScreenPointToRay( Input.mousePosition ), out hitInfo );
            if( hit ) {
                _mat.SetInt( "_Click", 1 );
                offWave = 1.0f;
                _mat.SetFloat( "_DirectionX", hitInfo.point.x );
                _mat.SetFloat( "_DirectionZ", hitInfo.point.z );
                _mat.SetFloat( "_OffWave", offWave );
            }
		} 

		offWave = offWave - (Time.deltaTime * 1.0f);

		if (offWave < 0.0f)
			offWave = 0.0f;

        _mat.SetFloat ("_OffWave",offWave);
	}

}
