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
        rigid.AddForce( new Vector3( moveInfo.directionX, 0, moveInfo.directionZ ).normalized * (moveInfo.speed) * 25, ForceMode.Force );
    }

    public void ActivateAnimation(bool act) {
        if (act)
            ship.SetTrigger("tgrHundir");
    }
}
