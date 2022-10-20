using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Rigidbody2D body;
    public GameObject Enemy_Common1;
    public GameObject Enemy_Common2;
    public GameObject Enemy_Common3;
    private int flipper = 0;
    private int delay = 0;
    private float hspeed = 1;
    
    void Awake()
    {
        body = GetComponent< Rigidbody2D >();
    }

    void FixedUpdate() {
        Vector2 view = transform.position;
        
        view.x = Mathf.Clamp( view.x, -2.5f, 2.5f );
        transform.position = view;

        if ( transform.position.x <= -2.5f || transform.position.x >= 2.5f ) {
            hspeed = -hspeed;
        }
        
        body.velocity = new Vector2( hspeed, 0 );
        if ( delay >= 50 ) {
            switch ( flipper ) {
            case 0:
                flipper = 1;
                delay = 0;
                Instantiate( Enemy_Common1, transform.position, Quaternion.identity );
                break;
            case 1:
                flipper = 2;
                delay = 0;
                Instantiate( Enemy_Common2, transform.position, Quaternion.identity );
                break;
            case 2:
                flipper = 0;
                delay = 0;
                Instantiate( Enemy_Common3, transform.position, Quaternion.identity );
                break;
            }
        }
        delay++;
    }
}
