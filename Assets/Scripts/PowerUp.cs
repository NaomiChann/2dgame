using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private Rigidbody2D body;
    private float gravity = 2.5f;
    void Awake() {
        body = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5f);
    }

    void FixedUpdate() {
        if ( body.velocity.y > -0.1 ) {
            gravity -= 0.15f;
        } else {
            gravity = gravity * 1.1f;
        }
        gravity = Mathf.Clamp( gravity, -2.5f, 2.5f );
        body.velocity = new Vector2( 0, gravity );
    }
    
    private void OnCollisionEnter2D( Collision2D other ) {
        var player = other.collider.GetComponent< Player >();
        if ( player != null ) {
            player.Increase( "power" );
            Destroy( this.gameObject );
        }
    }
}
