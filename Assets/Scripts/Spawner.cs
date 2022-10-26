using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public int type;
    //public int variant;
    public int position;
    public int offset;
    public int direction;
    public int quantity;
    public int distance;

    private int delay = 0;
    private Rigidbody2D body;
    public GameObject Enemy_Common1;
    public GameObject Enemy_Common2;
    public GameObject Enemy_Common3;
    
    void Awake() {
        body = GetComponent< Rigidbody2D >();

        switch ( position ) {
            case 0:
                transform.position = new Vector2( ( 0f + offset ), 5.5f );
                break;
            case 1:
                transform.position = new Vector2( ( 0f + offset ), -5.5f );
                break;
            case 2:
                transform.position = new Vector2( -3.5f, ( 0f + offset ) );
                break;
            case 3:
                transform.position = new Vector2( 3.5f, ( 0f + offset ) );
                break;
        }

        switch ( type ) {
            case 0:
                Instantiate( Enemy_Common1, transform.position, Quaternion.identity );
                break;
            case 1:
                Instantiate( Enemy_Common2, transform.position, Quaternion.identity );
                break;
            case 2:
                Instantiate( Enemy_Common3, transform.position, Quaternion.identity );
                break;
        }
        quantity--;
    }

    // Update is called once per frame
    void FixedUpdate() {
        switch ( position ) {
            case 0:
            case 1:
                body.velocity = new Vector2( direction, 0 );
                break;
            case 2:
            case 3:
                body.velocity = new Vector2( 0, direction );
                break;
        }

        if ( quantity != 0 ) {
            if ( delay >= distance ) {
                switch ( type ) {
                case 0:
                    Instantiate( Enemy_Common1, transform.position, Quaternion.identity );
                    break;
                case 1:
                    Instantiate( Enemy_Common2, transform.position, Quaternion.identity );
                    break;
                case 2:
                    Instantiate( Enemy_Common3, transform.position, Quaternion.identity );
                    break;
                }
                delay = 0;
                quantity--;
            }
            delay++;
        } else {
            Destroy( gameObject );
        }

        if ( transform.position.x > 3.5f || transform.position.x < -3.5f || transform.position.y > 5.5f || transform.position.y < -5.5f ) {
            Destroy( gameObject );
        }
    }

    /*
    distance:
        int
    quantity:
        int
    distance:
        int
    direction:
        left
        right
        up
        down
    position:
        left
        right
        up
        down
    type ( enemy definition ):

        movement behavior|cast|nonstop|
        sideways         |
        turn             |
        pathed           |
        random           |
        kamikaze         |

        ( straights are 0 value turns )

        final cast:
            bool
            ( wether or not they cast on death )
        weak will:
            bool
            ( wheter or not their bullets turn into bullet score on death )

        shooting behavior   |straight|spread|spiral|
        aimed               |
        explosive           |
        roundabout          |
        laser               |

        ( circles are 360 spreads
        straights are 0 spreads
        explosives are bullets holding bullets )

        bullet type|orb|knife|star|fire|
        small      |
        medium     |
        big        |
        jumbo      |

        bullet speed:
            int
        
        drop |regular|big |
        power|1      |5   |
        score|1000   |5000|

        ( power drops at full power are converted into half score
        half scores give half what would have been * 1000
        bullet scores give 250 )

        sprite:
            file
    
        color variant|white|blue|red|green|yellow|
        health       |base |* 2 |* 4|* 8  |* 16  |

    */
}
