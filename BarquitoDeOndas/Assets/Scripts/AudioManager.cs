using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioClip intro;
    public AudioClip ohShip;
    public AudioClip pop;
    public AudioClip backgroundMusic;

    public AudioSource audioSource;

    public static AudioManager instance;

    // Use this for initialization
    void Awake () {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }
}
