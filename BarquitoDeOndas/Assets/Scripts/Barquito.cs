using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barquito : MonoBehaviour {

    public Rigidbody2D rigid;

    void Start() {
        rigid = this.GetComponent<Rigidbody2D>();
    }

    public void Move( MoveInfo moveInfo ) {
        rigid.AddForce( moveInfo.direction * (moveInfo.speed), ForceMode2D.Force );
    }
}
