using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : MonoBehaviour, ICollectionable
{
    public int id;
    public static event Action<int> collectLetter;
    [SerializeField] private AudioClip collectClip;

    private void Start()
    {
        if (PlayerPrefs.GetInt("Letter" + id, 0) == 1)
        {
            Debug.Log("No aparezco");
            Destroy(gameObject);
        }

    }
    public void Collect()
    {
        collectLetter?.Invoke(id);
        AudioSource.PlayClipAtPoint(collectClip, transform.position, 1);
        Destroy(gameObject);
    }
}
