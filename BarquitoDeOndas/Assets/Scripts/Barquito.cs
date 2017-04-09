using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barquito : MonoBehaviour {

    public static Barquito instance;

    public Rigidbody rigid;

    public Collider col;

    public Animator ship;

    public Text textShadow;

    public Text textScore;

    private int score;

    void Start() {
        instance = this;
        if( rigid == null ) {
            rigid = this.GetComponentInChildren<Rigidbody>();
        }
        if(col == null) {
            col = rigid.GetComponent<Collider>();
        }
        score = 0;
    }

    public void ReactToWave( MoveInfo moveInfo ) {
        rigid.AddForce( new Vector3( moveInfo.directionX, 0, moveInfo.directionZ ).normalized * (moveInfo.speed) * 25, ForceMode.Force );
    }

    public void ActivateAnimation(bool act) {
        if( act ) {
            ship.SetTrigger( "tgrHundir" );
            AudioManager.instance.audioSource.PlayOneShot( AudioManager.instance.ohShip );
            rigid.velocity = Vector3.zero;
            col.enabled = false;
        }
    }

    public void CalculateScore() {
        score += 100;
        textScore.text = score.ToString();
        textShadow.text = score.ToString();
    }
}
