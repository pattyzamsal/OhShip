using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

    public CircleCollider2D circleCol;
    public float circleMax;

    [Range( 0, 10 )]
    public float multiplier;

    // Use this for initialization
    void Start() {
        circleCol = this.GetComponent<CircleCollider2D>();
    }

    void FixedUpdate() {
        circleCol.radius += Time.fixedDeltaTime * multiplier;
        //circleCol.radius = Mathf.Clamp01( circleCol.radius );
        if( circleCol.radius >= circleMax ) {
            GameObject.Destroy(this.gameObject);
        }
    }
}
