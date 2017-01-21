using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

    public float rippleMax;

    public CircleCollider2D circle;

    public MoveInfo moveInfo;

    [Range( 0, 10 )]
    public float multiplier;

    void Start() {
        circle = this.GetComponent<CircleCollider2D>();
    }

    void FixedUpdate() {
        circle.radius += Time.fixedDeltaTime * multiplier;
        if( this.gameObject.transform.localScale.x > rippleMax ) {
            GameObject.Destroy( this.gameObject );
        }
    }

    void OnCollisionEnter2D( Collision2D coll ) {
        if( coll.gameObject.tag == "OhShip" ) {
            moveInfo = new MoveInfo();
            moveInfo.speed = 1/circle.radius * rippleMax;
            moveInfo.direction = coll.transform.position - this.transform.position;
            coll.gameObject.SendMessage( "Move", moveInfo );
            GameObject.Destroy( this.gameObject );
        }
    }
}
