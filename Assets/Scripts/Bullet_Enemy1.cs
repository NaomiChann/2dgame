using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Enemy1 : MonoBehaviour {
    private Rigidbody2D body;
    public float velocity = -10f;
    public float bulletTimer = 5f;
    void Awake() {
        body = GetComponent<Rigidbody2D>();
        Destroy(gameObject, bulletTimer);
    }

    void Update() {
        body.velocity = new Vector2( 0, velocity );
    }

    private void OnCollisionEnter2D( Collision2D other ) {
        var player = other.collider.GetComponent< Player >();
        if ( player != null ) {
            player.Die();
        }
    }
}
