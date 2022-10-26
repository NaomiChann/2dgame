using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    private GameObject Cannon;
    public GameObject Bullet_Enemy1;
    public GameObject Bullet_Enemy2;
    public GameObject PowerUp;
    public GameObject PlayerO;
    private Rigidbody2D body;
    private Vector2 deathPoint;
    private int delay = 0;
    public float vspeed = 0.5f;
    public float hspeed = 0.1f;
    public int type;
    private int counter = 0;
    public int delayLimit = 250;

    private void Awake() {
        body = GetComponent< Rigidbody2D >();
        PlayerO = GameObject.FindGameObjectWithTag( "Player" );
        Cannon = transform.Find( "Cannon" ).gameObject;
    }
    private void Update() {
        delay++;

        if ( delay >= delayLimit ) {
            Shoot();
        }
    }

    private void FixedUpdate() {
        switch ( type ) {
            case 1:
                if ( transform.position.y < 1f ) {
                    hspeed += 0.05f;
                    vspeed += 0.05f;
                }
                break;
            case 2:
                if ( transform.position.y < -1.5f ) {
                    vspeed = -vspeed * 3f;
                }
                break;
            case 3:
                break;
            default:
                break;
        }
        if ( transform.position.x < -3f || transform.position.x > 3f || transform.position.y < -5f || transform.position.y > 6f ) {
            Destroy( gameObject );
        }
        
        body.velocity = new Vector2( hspeed, -vspeed );

        SetPos( transform.position );
    }

    void Shoot() {
        delay = 0;
        switch ( type ) {
            case 1:
                Instantiate( Bullet_Enemy1, Cannon.transform.position, Quaternion.identity );
                break;
            case 2:
                StartCoroutine( Burst() );
                break;
            case 3:
                Instantiate( Bullet_Enemy2, Cannon.transform.position, Quaternion.identity );
                break;
            default:
                break;
        }
        
        counter++;
    }

    private void OnCollisionEnter2D( Collision2D other ) {
        var player = other.collider.GetComponent< Player >();
        if ( player != null ) {
            player.Die();
        }
        if ( other.gameObject.tag.Equals( "Bullet" ) ) {
            Instantiate( PowerUp, deathPoint, Quaternion.identity );
            Destroy( other.gameObject );
            PlayerO.SendMessage( "ScoreUp", 100 );
            Destroy( gameObject );
        }
    }
    private void SetPos( Vector2 point ) {
        deathPoint = point;
    }
    private IEnumerator Burst() {
        yield return new WaitForSeconds( 0.2f );
        Instantiate( Bullet_Enemy2, Cannon.transform.position, Quaternion.identity );
        yield return new WaitForSeconds( 0.2f );
        Instantiate( Bullet_Enemy2, Cannon.transform.position, Quaternion.identity );
        yield return new WaitForSeconds( 0.2f );
        Instantiate( Bullet_Enemy2, Cannon.transform.position, Quaternion.identity );
    }
}