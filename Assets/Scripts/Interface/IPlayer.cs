using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer
{
    public void SlimeActive(float force);
    public void SlimeDamage(float DamageForceX, float DamageForceY);
    public void FlyCollition();
}
