using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private Rigidbody2D body;
    void Awake() {
        body = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5f);
    }
    private void OnCollisionEnter2D( Collision2D other ) {
        var player = other.collider.GetComponent< Player >();
        if ( player != null ) {
            player.Increase( "power" );
            Destroy( this.gameObject );
        }
    }
}
