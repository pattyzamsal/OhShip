using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

    public float rippleMax;

    public SphereCollider sphere;

    public MoveInfo moveInfo;

    [Range( 0, 20 )]
    public float multiplier;

    void Start() {
        sphere = this.GetComponent<SphereCollider>();
    }

    void FixedUpdate() {
        sphere.radius += Time.fixedDeltaTime * multiplier;
        if( sphere.radius > rippleMax ) {
            GameObject.Destroy( this.gameObject );
        }
    }

    void OnTriggerEnter( Collider coll ) {
        if( coll.gameObject.tag == "OhShip" ) {
            moveInfo = new MoveInfo();

            moveInfo.speed = Mathf.InverseLerp( 0, rippleMax, rippleMax / sphere.radius ) * multiplier;

            moveInfo.directionX = coll.transform.position.x - this.transform.position.x;
            moveInfo.directionY = coll.transform.position.y - this.transform.position.y;
            moveInfo.directionZ = coll.transform.position.z - this.transform.position.z;

            coll.gameObject.SendMessage( "ReactToWave", moveInfo );
            GameObject.Destroy( this.gameObject );
        }
    }
}
