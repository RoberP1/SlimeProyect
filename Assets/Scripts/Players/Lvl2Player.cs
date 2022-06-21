using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl2Player : Player
{
    protected override void Start()
    {
        base.Start();
        maxJump = 2;
    }   
}
