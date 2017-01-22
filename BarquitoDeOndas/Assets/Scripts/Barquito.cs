using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barquito : MonoBehaviour {

    public static Barquito instance;

    public Rigidbody rigid;

    public Animator ship;

    public Text textShadow;

    public Text textScore;

    private int score;

    void Start() {
        instance = this;
        rigid = this.GetComponent<Rigidbody>();
        score = 0;
    }

    public void ReactToWave( MoveInfo moveInfo ) {
        rigid.AddForce( new Vector3( moveInfo.directionX, 0, moveInfo.directionZ ).normalized * (moveInfo.speed) * 25, ForceMode.Force );
    }

    public void ActivateAnimation(bool act) {
        if (act)
            ship.SetTrigger("tgrHundir");
    }

    public void CalculateScore() {
        score += 100;
        textScore.text = score.ToString();
        textShadow.text = score.ToString();
    }
}
