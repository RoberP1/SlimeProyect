using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : MonoBehaviour
{
    Animator animator;
    int collisionPlayerID;
    [SerializeField] float jumpForce;
    [SerializeField] bool active;
    [SerializeField] float DamageForceX;
    [SerializeField] float DamageForceY;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        collisionPlayerID = Animator.StringToHash("collisionPlayer");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPlayer player = collision.GetComponentInParent<IPlayer>();
        if (player!=null)
        {
            if (active)
            {
                animator.SetBool(collisionPlayerID, true);
                player.SlimeActive(jumpForce);
            }
            else
            {
                player.SlimeDamage(DamageForceX, DamageForceY);
            }  
        }

    }
    public void AnimationJumpForce()
    {
        active = false;
    }
    public void AnimationJumpFinish()
    {
        animator.SetBool(collisionPlayerID, false);
        active = true;
    }
}
