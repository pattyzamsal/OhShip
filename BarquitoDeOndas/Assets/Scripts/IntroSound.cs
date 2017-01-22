using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSound : MonoBehaviour {

	void Start () {
        AudioManager.instance.audioSource.PlayOneShot( AudioManager.instance.intro );
    }
}
