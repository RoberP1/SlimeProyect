using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour, ICollectionable
{
    public static event Action<GameObject> collectDiamond;
    public void Collect()
    {
        collectDiamond?.Invoke(this.gameObject);
    }
}
