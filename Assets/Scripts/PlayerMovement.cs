using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask JumpableGround;
    private float dirX = 0f;
    [SerializeField]private float speed = 7f;
    [SerializeField]private float jumpForce = 14f;

    private enum MovementState {idle, running, jumping, falling}

    [SerializeField] private AudioSource jumpSoundEffect;

    // Start is called before the first frame update
    private void Start()
    {   
        // calling unity methods at the start of the gaame
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        //player movement is x direction
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * speed, rb.velocity.y);

        //player movement in y direction
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimation();
    }

    private  void UpdateAnimation()
    {

        MovementState state;
        if (dirX > 0)           // if player is moving in the right direction
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0)         //if player is moving in the left direction
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else   // if player is idle
        {
            state = MovementState.idle; 
        }

        /*
        // for jumping animation
        if (rb.velocity.y > 0.1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -0.1f)
        {
            state = MovementState.falling;
        }
        **/
        
        anim.SetInteger("state",(int)state);
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, JumpableGround);
    }
}
