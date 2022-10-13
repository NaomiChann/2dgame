using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    GameObject Cannon_1;
    GameObject Cannon_2_1;
    GameObject Cannon_2_2;
    GameObject Cannon_3_1;
    GameObject Cannon_3_2;
    GameObject Cannon_3_3;
    GameObject Cannon_3_4;
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
        Cannon_1 = transform.Find( "Cannon_1" ).gameObject;
        Cannon_2_1 = transform.Find( "Cannon_2_1" ).gameObject;
        Cannon_2_2 = transform.Find( "Cannon_2_2" ).gameObject;
        Cannon_3_1 = transform.Find( "Cannon_3_1" ).gameObject;
        Cannon_3_2 = transform.Find( "Cannon_3_2" ).gameObject;
        Cannon_3_3 = transform.Find( "Cannon_3_3" ).gameObject;
        Cannon_3_4 = transform.Find( "Cannon_3_4" ).gameObject;

        SetSpawn( transform.position );
    }

    private void Update() {
        Vector2 view = transform.position;
        
        dirX = Input.GetAxisRaw( "Horizontal" );
        dirY = Input.GetAxisRaw( "Vertical" ); 

        view.x = Mathf.Clamp( view.x, -2.5f, 2.5f );
        view.y = Mathf.Clamp( view.y, -4.5f, 4.5f );
        transform.position = view;
        
        if ( Input.GetKey( KeyCode.LeftShift ) ) {
            body.velocity = new Vector2( dirX * ( ( speed / 2 ) / characterSpeed ), dirY * ( ( speed / 2 ) / characterSpeed ) );
        } else {
            body.velocity = new Vector2( dirX * ( speed / characterSpeed ), dirY * ( speed / characterSpeed ) );
        }

        if ( Input.GetKey( KeyCode.Z ) && delay >= 10 ) {
            Shoot();
        }

        if ( Input.GetKeyDown( KeyCode.X ) && level < 3 ) {
            level++;
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
        Destroy( this.gameObject );
        StartCoroutine( Respawn() );
    }

    private IEnumerator Respawn() {
        yield return new WaitForSeconds( 1f );
        Instantiate( this, respawnPoint, Quaternion.identity );
    }

    private void SetSpawn( Vector2 point ) {
        respawnPoint = point;
    }
}
