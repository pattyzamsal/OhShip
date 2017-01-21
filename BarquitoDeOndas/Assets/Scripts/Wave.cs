using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

    public float circleMax;

    [Range( 0, 10 )]
    public float multiplier;

    void FixedUpdate() {
        this.gameObject.transform.localScale += new Vector3 ( Time.fixedDeltaTime , Time.fixedDeltaTime , Time.fixedDeltaTime ) * multiplier;
        //circleCol.radius = Mathf.Clamp01( circleCol.radius );
        if( this.gameObject.transform.localScale.x >= circleMax ) {
            GameObject.Destroy(this.gameObject);
        }
    }
}
