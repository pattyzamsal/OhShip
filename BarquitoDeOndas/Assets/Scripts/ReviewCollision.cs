using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviewCollision : MonoBehaviour {

    void OnTriggerEnter(Collider coll) {
        if (coll.tag == "OhShip" && this.tag == "Obstacle") {
            coll.gameObject.SendMessage("ActivateAnimation", true);

            GameManager.instance.LoseLife();
            StartCoroutine( SpawnNewShip ());
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Water" && this.tag == "Obstacle") {
            GameObject.Destroy(this.gameObject);
        }
    }

    public IEnumerator SpawnNewShip() {
        yield return new WaitForSeconds( 2f );
        Barquito.instance.ship.SetTrigger( "tgrBarquito" );
        GameManager.instance.ResetShipPosition();
    }

}
