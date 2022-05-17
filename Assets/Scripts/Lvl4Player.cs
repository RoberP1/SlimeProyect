using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl4Player : Lvl3Player
{
    
    protected bool canGrab;
    [SerializeField] protected Transform wallGrabPoint;
    [SerializeField] protected bool IsGrabbing;
    [SerializeField] protected float wallJumpTime = .2f;
    protected float wallJumpCounter;


    protected override void Update()
    {
        if (wallJumpCounter <= 0)
        {

            base.Update();
            canGrab = Physics2D.OverlapCircle(wallGrabPoint.position, .2f, graund);

            if (canGrab)
            {
                if ((!isRotated && (Input.GetAxis("Horizontal") > 0 || rb.velocity.x > 0)) || (isRotated && (Input.GetAxis("Horizontal") < 0 ||rb.velocity.x < 0)))
                {
                    IsGrabbing = true;
                    isJumping = false;
                    jump = 0;
                }
            }
            else
            {
                IsGrabbing = false;
            }
            if (IsGrabbing)
            {
                rb.velocity = Vector2.zero;
                rb.gravityScale = 0;
                jumpDirection = (isRotated) ? Vector2.one.normalized : new Vector2(-1f, 1f).normalized;
                Jump();
            }
            else
            {
                rb.gravityScale = gravityStore;
                jumpDirection = Vector2.up;
            }
        }
        else
        {
            wallJumpCounter -= Time.deltaTime;
        }
    }
    protected override void Jump()
    {
        base.Jump();
        if (IsGrabbing && isJumping)
        {
            rb.gravityScale = gravityStore;
            IsGrabbing = false;
        }
    }
}
