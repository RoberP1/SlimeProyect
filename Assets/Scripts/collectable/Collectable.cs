using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collectable : MonoBehaviour,ICollectionable
{
    //public static event Action<GameObject> collect;

    public virtual void Collect()
    {
        //collect?.Invoke(this.gameObject);
        //Destroy(gameObject);
    }
}
