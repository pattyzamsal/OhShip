using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : MonoBehaviour {

    void OnTriggerExit(Collider other) {
        if (other.tag == "Water" && this.tag == "Obstacle") {
            GameObject.Destroy(this.gameObject);
        }
    }
}
