using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReviewCollision : MonoBehaviour {

    private Barquito ship;

    void Start() {
        ship = GameObject.FindGameObjectWithTag("OhShip").GetComponent<Barquito>();
    }

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
            if(!GameManager.instance.gameOver) ship.CalculateScore();
        }
    }

    public IEnumerator SpawnNewShip() {
        yield return new WaitForSeconds( 2f );
        if( !GameManager.instance.gameOver ) {
            Barquito.instance.ship.SetTrigger( "tgrBarquito" );
            GameManager.instance.ResetShipPosition();
        }
    }

}
