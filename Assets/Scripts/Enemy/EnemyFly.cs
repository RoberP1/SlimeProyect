using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyFly : MonoBehaviour
{
    bool flyActive = true;
    Animator anim;
    private int animTrigger;
    [SerializeField] private AudioClip flyClip;
    private void Start()
    {
        anim = GetComponent<Animator>();
        animTrigger = Animator.StringToHash("Trigger");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!flyActive) return;
        IPlayer player = collision.GetComponentInParent<IPlayer>();
        if (player != null)
        {
            player.FlyCollition();
            flyActive = false;
            anim.SetBool(animTrigger, true);
            AudioSource.PlayClipAtPoint(flyClip, transform.position);
        }
    }
    public void animationFinish()
    {
        anim.SetBool("Trigger", false);
        flyActive = true;
    }
}
