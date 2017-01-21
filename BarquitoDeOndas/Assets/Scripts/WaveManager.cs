using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    public GameObject wave;

    void Update() {
        if( Input.GetMouseButtonDown( 0 ) ) {
            RaycastHit rayHit = new RaycastHit();
            if( Physics.Raycast( Camera.main.ScreenPointToRay( Input.mousePosition ), out rayHit ) ) {
                GameObject newWave = GameObject.Instantiate( wave, rayHit.transform );
                newWave.transform.position = new Vector3( rayHit.point.x, rayHit.point.y, rayHit.point.z );
            }
        }
    }

    public IEnumerator RandomWaveSpawner() {
        yield return null;
    }


}
