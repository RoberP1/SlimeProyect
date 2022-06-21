using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour,ICollectionable
{
    protected GameManager gameManager;
    public virtual void Collect()
    {
        gameManager = FindObjectOfType<GameManager>();

    }

}
