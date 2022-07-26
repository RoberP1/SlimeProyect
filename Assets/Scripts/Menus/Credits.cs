using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField]
    public float velocidad;

    void FixedUpdate()
    {
        transform.Translate(0, velocidad, 0);
    }
}
