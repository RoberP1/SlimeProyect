using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CheckPoint : MonoBehaviour
{
    public static event Action<Vector3> OnCheckPoint;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPlayer player = collision.GetComponent<IPlayer>();
        if (player != null)
        {
            OnCheckPoint?.Invoke(transform.position);
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
