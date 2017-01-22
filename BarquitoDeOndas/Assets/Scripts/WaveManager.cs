using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    public GameObject wave;

    private bool _isTouching;

    void Start() {
        _isTouching = false;
    }

    void OnMouseDown() {
        _isTouching = true;
        StartCoroutine(checkTouching());
    }

    void OnMouseUp() {
        _isTouching = false;
    }

    IEnumerator checkTouching() {
        while (_isTouching) {
            if (Input.GetMouseButtonDown(0)) {
                RaycastHit rayHit = new RaycastHit();
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit)) {
                    AudioManager.instance.audioSource.PlayOneShot( AudioManager.instance.pop );
                    GameObject newWave = GameObject.Instantiate(wave, rayHit.transform);
                    newWave.transform.position = new Vector3(rayHit.point.x, rayHit.point.y, rayHit.point.z);
                }
            }
            yield return null;
        }
    }
}

