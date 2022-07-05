using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstaKill : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPlayer Player = collision.GetComponent<IPlayer>();
        Player?.InstaKillDamage();
    }
}
