using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    private GameObject Cannon;
    public GameObject Bullet_Enemy1;
    public GameObject PowerUp;
    private Rigidbody2D Body;
    private Vector2 deathPoint;
    private int delay = 0;
    public int delayLimit = 50;

    private void Awake() {
        Body = GetComponent< Rigidbody2D >();
        Cannon = transform.Find( "Cannon" ).gameObject;
    }
    private void Update() {
        if ( delay >= delayLimit ) {
            Shoot();
        }
        SetPos( transform.position );
        delay++;
    }
    void Shoot() {
        delay = 0;
        Instantiate( Bullet_Enemy1, Cannon.transform.position, Quaternion.identity );
    }

    private void OnCollisionEnter2D( Collision2D other ) {
        var player = other.collider.GetComponent< Player >();
        if ( player != null ) {
            player.Die();
        }
        if ( other.gameObject.tag.Equals( "Bullet" ) ) {
            Instantiate( PowerUp, deathPoint, Quaternion.identity );
            Destroy( other.gameObject );
            Destroy( gameObject );
        }
    }
    private void SetPos( Vector2 point ) {
        deathPoint = point;
    }
}