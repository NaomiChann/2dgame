using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    private Rigidbody2D body;
    private float velocity = 50f;
    void Awake() {
        body = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 0.25f);
    }

    void FixedUpdate() {
        body.velocity = new Vector2( 0, velocity );
    }
}
