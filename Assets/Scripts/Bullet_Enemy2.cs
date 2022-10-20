using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Enemy2 : MonoBehaviour {
    private Rigidbody2D body;
    private float velocity = 5f;
    public float bulletTimer = 3f;
    Player target;
    Vector2 direction;
    void Awake() {
        body = GetComponent<Rigidbody2D>();
        Destroy(gameObject, bulletTimer);
        target = GameObject.FindObjectOfType< Player >();
        direction = ( target.transform.position - transform.position ).normalized * velocity;
        body.velocity = new Vector2( direction.x, direction.y );
    }

    void FixedUpdate() {
    }

    private void OnCollisionEnter2D( Collision2D other ) {
        var player = other.collider.GetComponent< Player >();
        if ( player != null ) {
            player.Die();
            Destroy( this.gameObject );
        }
    }
}
