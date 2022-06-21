using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFly : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPlayer player = collision.GetComponentInParent<IPlayer>();
        if (player != null)
        {
            player.FlyCollition();
            Destroy(gameObject);
        }
    }
}
