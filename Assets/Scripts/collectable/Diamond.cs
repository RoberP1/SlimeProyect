using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour, ICollectionable
{
    public static event Action collectDiamond;
    [SerializeField] private AudioClip collectClip;
    public void Collect()
    {
        collectDiamond?.Invoke();
        AudioSource.PlayClipAtPoint(collectClip, transform.position, 1);
        Destroy(gameObject);
    }
}
