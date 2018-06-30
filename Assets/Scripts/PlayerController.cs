using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Rigidbody2D rb;

    private enum PlayerState {IDLE, WALKING, JUMPING, FALLING};
    private int currentState;

    private float maxSpeedX = 6f;
    private float speedX;
    
    private float rayCastDownDist = 0.5f;
    private Collider2D Hit;

    private float jumpForce = 300.0f;
    private bool isGrounded;
    //private bool isFalling;
    //private bool isJumping;
    //private bool isJumpHeld;
    //private bool isSlowingDown;
    //private float speedY;
    //private float maxSpeedY = 6f;
    //private float maxFallSpeed = 6f;
    //private float fullHopJumpHeight = 3f;
    //private float shortHopJumpHeight = 1f;
    //private float slowDownHeight = 0.3f;
    //private Vector3 fullHopPoint;
    //private Vector3 shortHopPoint;
    //private Vector3 slowDownPoint;


    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {

        //speedY = 0;
        JumpUpdate();
        FallUpdate();
        WalkUpdate();
        //Debug.Log(currentState);
        transform.Translate(speedX, 0, 0);
    }

    private void WalkUpdate()
    {
        speedX = Input.GetAxisRaw("Horizontal") * maxSpeedX * Time.deltaTime;
        if(rb.velocity.y == 0)
        {
            if (speedX == 0)
                currentState = (int)PlayerState.IDLE;
            else
                currentState = (int)PlayerState.WALKING;
        }
    }

    private void JumpUpdate()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                Debug.Log("Jumping");
                rb.AddForce(new Vector2(0, jumpForce));
                currentState = (int)PlayerState.JUMPING;
            }
        }
    }

    private void FallUpdate()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Ignore Raycast");
        layerMask = ~layerMask;
        Debug.Log(layerMask);
        Hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.down), rayCastDownDist, layerMask).collider;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down), Color.red);
        Debug.Log(Hit); 
        if (Hit == null)
        {
            isGrounded = false;
            if (rb.velocity.y < 0)
            {
                currentState = (int)PlayerState.FALLING;
            }
            //Debug.Log("Falling");
        }
        else if (currentState != (int)PlayerState.JUMPING)
        {
            isGrounded = true;
            Debug.Log("Grounded");
        }

        //if (isFalling && !isJumping)
        //{
        //    speedY = -(maxFallSpeed + (Input.GetAxisRaw("Vertical") > 0 ? 0 : -Input.GetAxisRaw("Vertical")) * maxSpeedY) * Time.deltaTime;
        //}
    }

    //private void JumpUpdate()
    //{
    //    if (Input.GetButton("Jump"))
    //    {
    //        if (!isJumping && !isFalling && !isJumpHeld)
    //        {
    //            isJumping = true;
    //            isJumpHeld = true;
    //            fullHopPoint = new Vector3(transform.position.x, transform.position.y + fullHopJumpHeight, transform.position.z);
    //            shortHopPoint = new Vector3(transform.position.x, transform.position.y + shortHopJumpHeight, transform.position.z);
    //        }
    //    }
    //    if (isJumping)
    //    {
    //        if(!isSlowingDown)
    //        {
    //            if (isJumpHeld == true || transform.position.y < shortHopPoint.y)
    //            {
    //                speedY = maxSpeedY * Time.deltaTime;
    //                //Debug.Log("Jumping");
    //            }
    //            if (transform.position.y > fullHopPoint.y)
    //            {
    //                SlowDown();
    //                Debug.Log("Full hop");
    //            }
    //            if (transform.position.y > shortHopPoint.y && isJumpHeld == false)
    //            {
    //                SlowDown();
    //                Debug.Log("Short hop");
    //            }
    //        }
    //        else
    //        {
    //            if (transform.position.y > slowDownPoint.y)
    //            {
    //                Debug.Log("Done slowing down");
    //                isSlowingDown = false;
    //                isJumping = false;
    //            }
    //            else
    //            {
    //                speedY = (slowDownPoint.y - transform.position.y + 0.1f) * maxSpeedY * Time.deltaTime;
    //            }

    //        }
    //    }

    //    if (Input.GetButtonUp("Jump"))
    //    {
    //        isJumpHeld = false;
    //        Debug.Log("Let go of jump");
    //    }
    //}

    //private void SlowDown()
    //{
    //    isSlowingDown = true;
    //    slowDownPoint = new Vector3(transform.position.x, transform.position.y + slowDownHeight, transform.position.z);
    //}


}
