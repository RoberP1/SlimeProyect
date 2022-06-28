using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : MonoBehaviour, ICollectionable
{
    public int id;
    public static event Action<int> collectLetter;
    public void Collect()
    {
        collectLetter?.Invoke(id);
        Destroy(gameObject);
    }
}
