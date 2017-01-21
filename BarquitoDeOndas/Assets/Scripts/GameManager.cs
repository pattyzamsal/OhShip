using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public int lifes;

    public GameObject lifeContainer;

    public List<GameObject> lifeList;

    public GameObject lifeSprite;

    void Awake() {
        instance = this;
    }

    void Start() {
        for( int i = 0; i < lifes; i++ ) {
            lifeList.Add( GameObject.Instantiate( lifeSprite, lifeContainer.transform ) );
        }
    }

    public void LoseLife() {
        lifeList.RemoveAt(0);
    }

    public void AddLife() {
        lifeList.Add( GameObject.Instantiate( lifeSprite, lifeContainer.transform ) );
    }

}
