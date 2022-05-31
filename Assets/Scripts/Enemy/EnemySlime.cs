using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : MonoBehaviour
{
    Animator animator;
    int collisionPlayerID;
    [SerializeField] float jumpForce;
    [SerializeField] bool feetCollision;
    [SerializeField] bool bodyCollision;
    [SerializeField] bool active;
    [SerializeField] float DamageForceX;
    [SerializeField] float DamageForceY;
    [SerializeField]Player player;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        collisionPlayerID = Animator.StringToHash("collisionPlayer");
        player = FindObjectOfType<Player>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (active)
            {
                feetCollision = true;
                animator.SetBool(collisionPlayerID, true);
                player.rb.velocity = Vector2.up * jumpForce;
            }
            else
            {
                player.Knocked();
                player.rb.velocity = (player.isRotated) ? new Vector2(DamageForceX, DamageForceY) : new Vector2(-DamageForceX, DamageForceY);
                
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
        feetCollision = false;
        active = true;
    }
}
