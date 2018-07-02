
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{

    protected Rigidbody2D RBody;
    public enum State { IDLE, WALKING, JUMPING, FALLING };
    public int currentState;
    public float rayCastDownDist;

    protected float maxSpeedX = 10f;
    protected float accelX = 50f;
    protected float speedX;
    protected float slowDownX = 1.2f;

    protected Collider2D Hit;

    protected float jumpForce = 350f;
    protected float fastFallForce = -50.0f;
    protected float jumpForceHeld = 60.0f;
    protected bool isGrounded;
    protected bool isJumpHeld;
    protected float fullHopJumpHeight = 1.2f;
    protected Vector3 fullHopPoint;



    // Use this for initialization
    protected virtual void Start()
    {
    }

    // Update is called once per frame
    protected virtual void Update()
    {
    }

    protected virtual void WalkUpdate(float InputHorizontal)
    {

        speedX = InputHorizontal * accelX;// * Time.deltaTime;
        if (isGrounded)
        {
            if (speedX == 0)
            {
                currentState = (int)State.IDLE;
                RBody.velocity = new Vector2(RBody.velocity.x / slowDownX, RBody.velocity.y);
            }
            else
            {
                currentState = (int)State.WALKING;
            }
        }
        if (System.Math.Abs(RBody.velocity.x) > maxSpeedX)
        {
            RBody.velocity = new Vector2(maxSpeedX * System.Math.Sign(RBody.velocity.x), RBody.velocity.y);
        }
        else
        {
            RBody.AddForce(new Vector2(speedX, 0));
        }
    }

    protected virtual void JumpUpdate(bool InputJumpDown, bool InputJumpUp, bool InputJumpHeld)
    {
        if (InputJumpDown && isGrounded)
        {
            isJumpHeld = true;
            fullHopPoint = new Vector3(transform.position.x, transform.position.y + fullHopJumpHeight, transform.position.z);
            RBody.AddForce(new Vector2(0, jumpForce));
            currentState = (int)State.JUMPING;
        }
        else if (InputJumpUp || transform.position.y >= fullHopPoint.y || RBody.velocity.y < 0)
        {
            isJumpHeld = false;
        }
        else if (InputJumpHeld && isJumpHeld && transform.position.y < fullHopPoint.y && RBody.velocity.y > 0)
        {
            RBody.AddForce(new Vector2(0, jumpForceHeld));
        }
    }

    protected virtual void FallUpdate(float InputVertical)
    {
        int layerMask = 1 << LayerMask.NameToLayer("Ignore Raycast");
        layerMask = ~layerMask;
        Hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.down), rayCastDownDist, layerMask).collider;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * rayCastDownDist, Color.red);
        if (Hit == null)
        {
            isGrounded = false;
            if (RBody.velocity.y < 0)
            {
                currentState = (int)State.FALLING;
                if (InputVertical < 0)
                {
                    RBody.AddForce(new Vector2(0, fastFallForce));
                }
            }
            else if (RBody.velocity.y > 0)
            {
                currentState = (int)State.JUMPING;
            }
        }
        else// if(RBody.velocity.y == 0)
        {
            isGrounded = true;
        }
    }


    public float GetSpeedX()
    {
        return RBody.velocity.x;
    }
}
