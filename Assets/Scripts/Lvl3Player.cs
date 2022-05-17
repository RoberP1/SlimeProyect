using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl3Player : Lvl2Player
{
    [SerializeField] protected float dashImpulse;
    protected override void Movement()
    {
        base.Movement();
        if (Input.GetButtonDown("Dash"))
        {
            Debug.Log("Fiashfius");
            rb.AddRelativeForce(transform.right * dashImpulse, ForceMode2D.Impulse);
        }
    }
}
