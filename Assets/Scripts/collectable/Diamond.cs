using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : Collectable
{
    public override void Collect()
    {
        base.Collect();
        //aumentar barra de progrso de vida
        Destroy(gameObject);
    }
}
