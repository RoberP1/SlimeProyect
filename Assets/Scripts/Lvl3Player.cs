using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl3Player : Lvl2Player
{
    protected float gravityStore;
    [Header("Dash")]
    [SerializeField] protected float dashImpulse;
    [SerializeField] protected float dashCD;
    [SerializeField] protected float dashTime;
    [SerializeField] protected bool candash;
    [SerializeField] protected bool isDashing;

    protected override void Start()
    {
        base.Start();
        gravityStore = rb.gravityScale;
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
            StartCoroutine(DashTime(dashTime));
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
    public IEnumerator DashTime(float dashTime)
    {
        isDashing = true;
        yield return new WaitForSeconds(dashTime);
        rb.velocity = Vector2.zero;
        rb.gravityScale = gravityStore;
        isDashing = false;
    }

}
