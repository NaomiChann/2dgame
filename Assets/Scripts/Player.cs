using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private GameObject Cannon_1;
    private GameObject Cannon_2_1;
    private GameObject Cannon_2_2;
    private GameObject Cannon_3_1;
    private GameObject Cannon_3_2;
    private GameObject Cannon_3_3;
    private GameObject Cannon_3_4; // probably can do this with a single object
    private SpriteRenderer pRender;
    public GameObject Bullet;
    private Rigidbody2D body;
    private Collider2D pCollider;
    private Vector2 respawnPoint;
    private float speed = 10f;
    private float characterSpeed = 2f;
    private int level = 1;
    private int delay = 0;
    private float dirX = 0f;
    private float dirY = 0f;

    private void Awake() {
        body = GetComponent< Rigidbody2D >();
        pCollider = GetComponent< Collider2D >();
        pRender = GetComponent< SpriteRenderer >();
        Cannon_1 = transform.Find( "Cannon_1" ).gameObject;
        Cannon_2_1 = transform.Find( "Cannon_2_1" ).gameObject;
        Cannon_2_2 = transform.Find( "Cannon_2_2" ).gameObject;
        Cannon_3_1 = transform.Find( "Cannon_3_1" ).gameObject;
        Cannon_3_2 = transform.Find( "Cannon_3_2" ).gameObject;
        Cannon_3_3 = transform.Find( "Cannon_3_3" ).gameObject;
        Cannon_3_4 = transform.Find( "Cannon_3_4" ).gameObject;

        SetSpawn( transform.position );
    }

    private void FixedUpdate() {
        Vector2 view = transform.position;
        
        dirX = Input.GetAxisRaw( "Horizontal" );
        dirY = Input.GetAxisRaw( "Vertical" ); 

        view.x = Mathf.Clamp( view.x, -2.5f, 2.5f );
        view.y = Mathf.Clamp( view.y, -4.5f, 4.5f );
        transform.position = view;
        
        if ( pRender.enabled == true ) {
            if ( Input.GetKey( KeyCode.LeftShift ) ) {
                body.velocity = new Vector2( dirX * ( ( speed / 2 ) / characterSpeed ), dirY * ( ( speed / 2 ) / characterSpeed ) );
            } else {
                body.velocity = new Vector2( dirX * ( speed / characterSpeed ), dirY * ( speed / characterSpeed ) );
            }

            if ( Input.GetKey( KeyCode.Z ) && delay >= 2 ) {
                Shoot();
            }
        }

        delay++;
    }

    void Shoot() {
        delay = 0;
        switch ( level ) {
            case 1:
                Instantiate( Bullet, Cannon_1.transform.position, Quaternion.identity );
                break;
            case 2:
                Instantiate( Bullet, Cannon_2_1.transform.position, Quaternion.identity );
                Instantiate( Bullet, Cannon_2_2.transform.position, Quaternion.identity );
                break;
            case 3:
                Instantiate( Bullet, Cannon_3_1.transform.position, Quaternion.identity );
                Instantiate( Bullet, Cannon_3_2.transform.position, Quaternion.identity );
                Instantiate( Bullet, Cannon_3_3.transform.position, Quaternion.identity );
                Instantiate( Bullet, Cannon_3_4.transform.position, Quaternion.identity );
                break;
        }
    }

    public void Die() {
        pRender.enabled = false;
        pCollider.enabled = false;
        level = 1;
        StartCoroutine( Respawn() );
    }

    private IEnumerator Respawn() {
        yield return new WaitForSeconds( 1f );
        transform.position = respawnPoint;
        pRender.enabled = true;
        pCollider.enabled = true;
    }

    private void SetSpawn( Vector2 point ) {
        respawnPoint = point;
    }

    public void Increase( string attribute ) {
        switch ( attribute ) {
            case "score":
                break;
            case "power":
                if ( level < 3 ) {
                    level++;
                }
                break;
            case "bomb":
                break;
            case "life":
                break;
        }
    }
}
