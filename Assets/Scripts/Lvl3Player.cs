using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl3Player : Lvl2Player
{
    [Header("Dash")]
    [SerializeField] protected float dashImpulse;
    [SerializeField] protected float dashCD;
    [SerializeField] protected bool candash;
    protected override void Movement()
    {
        base.Movement();
        Dash();
    }

    private void Dash()
    {
        if (Input.GetButtonDown("Dash") && candash)
        {
            rb.AddRelativeForce(transform.right * dashImpulse, ForceMode2D.Impulse);
            StartCoroutine(DashCD(dashCD));
        }
    }
    public IEnumerator DashCD(float dashCD)
    {
        candash = false;
        yield return new WaitForSeconds(dashCD);
        candash = true;
    }

}
