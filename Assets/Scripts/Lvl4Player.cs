using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl4Player : Lvl3Player
{
    protected float gravityStore;
    protected bool canGrab;
    [SerializeField] protected Transform wallGrabPoint;
    [SerializeField] protected bool IsGrabbing;

    protected override void Start()
    {
        base.Start();
        gravityStore = rb.gravityScale;
    }
    protected override void Update()
    {
        base.Update();
        JumpWalls();
    }

    private void JumpWalls()
    {
        canGrab = Physics2D.OverlapCircle(wallGrabPoint.position, .2f, graund);

        if (canGrab)
        {

            jumpDirection = (isRotated) ? new Vector2(0.5f, 0.5f) : new Vector2(-0.5f, 0.5f);
            if (rb.gravityScale != 0 || Input.GetAxis("Horizontal") != 0) rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
            jump = 1;
        }
        else
        {
            jumpDirection = new Vector2(0, 1);
            rb.gravityScale = gravityStore;
        }
    }
}
