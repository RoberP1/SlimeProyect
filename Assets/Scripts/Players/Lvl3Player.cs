using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl3Player : Lvl2Player
{
    protected float gravityStore;
    [Header("Dash")]
    [SerializeField] protected float dashImpulse;
    [SerializeField] protected float dashCD;
    [SerializeField] protected bool candash;
    [SerializeField] protected bool isDashing;
    int dashID;

    protected override void Start()
    {
        base.Start();
        gravityStore = rb.gravityScale;
        dashID = Animator.StringToHash("Dash");
    }
    protected override void Update()
    {
        base.Update();
        Dash();
        
    }

    private void Dash()
    {
        if (Input.GetButtonDown("Dash") && candash && !isDashing)
        {
            StartCoroutine(DashCD(dashCD));
            isDashing = true;
            animator.SetBool(dashID, isDashing);
        }
        if (isDashing)
        {
            rb.gravityScale = 0;
            rb.velocity = (isRotated) ? Vector2.left * dashImpulse : Vector2.right * dashImpulse;
        }            
    }
    public IEnumerator DashCD(float dashCD)
    {
        candash = false;
        yield return new WaitForSeconds(dashCD);
        candash = true;
    }

    protected override void SetPlayer()
    {
        base.SetPlayer();
        dashImpulse = playerScriptableObject.dashImpulse;
        dashCD = playerScriptableObject.dashCD;
    }

    //animation events
    public void DashFinish()
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = gravityStore;
        isDashing = false;
        animator.SetBool(dashID, isDashing);
    }
    public override void Knocked()
    {
        base.Knocked();
        DashFinish();
    }
}
