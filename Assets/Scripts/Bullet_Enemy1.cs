using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Enemy1 : MonoBehaviour {
    private Rigidbody2D body;
    private float velocity = -5f;
    public float bulletTimer = 3f;
    void Awake() {
        body = GetComponent<Rigidbody2D>();
        Destroy(gameObject, bulletTimer);
    }

    void FixedUpdate() {
        body.velocity = new Vector2( 0, velocity );
    }

    private void OnCollisionEnter2D( Collision2D other ) {
        var player = other.collider.GetComponent< Player >();
        if ( player != null ) {
            player.Die();
            Destroy( this.gameObject );
        }
    }
}
