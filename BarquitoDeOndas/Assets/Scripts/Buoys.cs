using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoys : MonoBehaviour {

    public MoveInfo moveInfo;

    public float power;

    void OnCollisionEnter( Collision coll ) {
        if( coll.gameObject.tag == "OhShip" ) {
            moveInfo = new MoveInfo();

            moveInfo.speed = power;

            moveInfo.directionX = coll.transform.position.x - this.transform.position.x;
            moveInfo.directionY = coll.transform.position.y - this.transform.position.y;
            moveInfo.directionZ = coll.transform.position.z - this.transform.position.z;

            coll.gameObject.SendMessage( "ReactToWave", moveInfo );

            GameManager.instance.LoseLife();
        }
    }
}
