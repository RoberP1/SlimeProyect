using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    public bool CanTakeDamage;


    
    [Header("Attack")]
    [SerializeField] GameObject ProyectilePreFab;
    [SerializeField] float ProyectileVelocity;
    [SerializeField] int fireNumber;

    [SerializeField] float jumpForce;
    //animations
    private Animator animator;
    private int HitID;
    private int AttackID;
    private int DeadID;

    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
        health = maxHealth;
        SetAnimationId();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPlayer Player = collision.GetComponent<IPlayer>();
        if (Player == null) return;
        if (CanTakeDamage)
        {
            Player.SlimeActive(jumpForce);
            TakeDamage();
        }
        else
        {
            Player.InstaKillDamage();
        }
    }

    public void Attack()
    {
        if (fireNumber > 0)
        {
            GameObject proyectile = Instantiate(ProyectilePreFab, transform.position , Quaternion.identity);
            proyectile.GetComponent<Rigidbody2D>().velocity = (player.position - (transform.position)) * ProyectileVelocity;
            animator.SetBool(AttackID, true);
            fireNumber--;
        }
        if (fireNumber <= 0)
        {
            fireNumber = maxHealth - health + 1;
            animator.SetBool(AttackID, false);
        }
    }

    public void TakeDamage()
    {
        animator.SetBool(HitID, true);
        health--;
        if (health <= 0)
        {
            animator.SetBool(DeadID, true);
        }
        fireNumber = maxHealth - health + 1;
    }
    public void FinishDamageAnimation()
    {
        animator.SetBool(HitID, false);

    }
    public void cantakeDamage()
    {
        CanTakeDamage = !CanTakeDamage;
    }

    private void SetAnimationId()
    {
        HitID = Animator.StringToHash("Hit");
        AttackID = Animator.StringToHash("Attack");
        DeadID = Animator.StringToHash("Dead");
    }

}