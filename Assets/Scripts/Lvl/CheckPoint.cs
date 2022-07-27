using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CheckPoint : MonoBehaviour
{
    private Animator animator;
    private int id;
    public static event Action<Vector3> OnCheckPoint;

    [SerializeField] private AudioClip checkPointClip;
    private void Start()
    {
        animator = GetComponent<Animator>();
        id = Animator.StringToHash("CheckPoint");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPlayer player = collision.GetComponent<IPlayer>();
        if (player != null)
        {
            AudioSource.PlayClipAtPoint(checkPointClip, transform.position);
            OnCheckPoint?.Invoke(transform.position);
            GetComponent<Collider2D>().enabled = false;
            animator.SetTrigger(id);
        }
    }
}
