using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barquito : MonoBehaviour {

    public static Barquito instance;

    public Rigidbody rigid;

    public Animator ship;

    void Start() {
        instance = this;
        rigid = this.GetComponent<Rigidbody>();
    }

    public void ReactToWave( MoveInfo moveInfo ) {
        rigid.AddForce( new Vector3( moveInfo.directionX, 0, moveInfo.directionZ ).normalized * (moveInfo.speed), ForceMode.Impulse );
    }

    public void ActivateAnimation(bool act) {
        if (act)
            ship.SetTrigger("tgrHundir");
    }
}
