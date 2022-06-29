using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour, ICollectionable
{
    public static event Action collectDiamond;

    public void Collect()
    {
        collectDiamond?.Invoke();
        Destroy(gameObject);
    }
}
