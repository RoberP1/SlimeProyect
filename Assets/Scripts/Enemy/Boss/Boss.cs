using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    public bool CanTakeDamage;
    public GameObject[] hearts;

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
            Destroy(proyectile, 10);
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
        if (hearts.Length > 0)
        {
            hearts[hearts.Length - health -1].SetActive(false);
        }
        if (health <= 0)
        {
            animator.SetBool(DeadID, true);
            GetComponent<Collider2D>().enabled = false;
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
    public void Victory()
    {
        SceneManager.LoadScene("Victory");
    }
        

    private void SetAnimationId()
    {
        HitID = Animator.StringToHash("Hit");
        AttackID = Animator.StringToHash("Attack");
        DeadID = Animator.StringToHash("Dead");
    }

}
