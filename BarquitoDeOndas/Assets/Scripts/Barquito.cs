using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barquito : MonoBehaviour {

    public static Barquito instance;

    public float maxSpeed = 100;

    public Rigidbody rigid;

    void Start() {
        instance = this;
        rigid = this.GetComponent<Rigidbody>();
    }

    public void FixedUpdate() {
        rigid.velocity = Vector3.ClampMagnitude( rigid.velocity, maxSpeed );
    }

    public void Move( MoveInfo moveInfo ) {
        Debug.Log( "speed: " + moveInfo.speed );
        //rigid.angularVelocity = Vector3.zero;
        //rigid.velocity = Vector3.zero;
        rigid.AddForce( new Vector3( moveInfo.directionX, 0, moveInfo.directionZ ).normalized * (moveInfo.speed), ForceMode.Impulse );
    }
}
