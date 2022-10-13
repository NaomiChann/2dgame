using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    GameObject Cannon;
    public GameObject Bullet_Enemy1;
    private Rigidbody2D Body;
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
    }
}
