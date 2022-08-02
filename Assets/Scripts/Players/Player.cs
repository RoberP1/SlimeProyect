using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour,IPlayer
{
    public Rigidbody2D rb;
    [SerializeField] protected PlayerScriptableObject playerScriptableObject;

    [Header("Movement")]
    [SerializeField] protected float speed;
    public bool isRotated;
    public bool knocked;

    [Header("Jump")]
    [SerializeField] protected float jumpForce;
    [SerializeField] protected float rayCastDistance;
    [SerializeField] protected int maxJump;
    [SerializeField] protected int jump;
    [SerializeField] protected Vector2 jumpDirection = new Vector2(0,1);
    [SerializeField] protected LayerMask graund;
    [SerializeField] protected bool isJumping;

    [Header("Animation")]
    [SerializeField] protected Animator animator;
    int walkID;
    int jumpID;
    int hitID;

    [Header("Audio")]
    protected AudioSource audioSource;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip takeDamgeClip;

    

    public static event Action OnTakeDamage;
    public static event Action OnHitFinish;
    public static event Action OnInstaDead;


    protected virtual void Start()
    {
        audioSource = GetComponent<AudioSource>();

        animator = GetComponent<Animator>();
        walkID = Animator.StringToHash("Walk");
        jumpID = Animator.StringToHash("Jump");
        hitID = Animator.StringToHash("Hit");

        rb = GetComponent<Rigidbody2D>();

        SetPlayer();
    }

    protected virtual void Update()
    {

        if (knocked)
        {
            GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
            return;
        }
        Movement();
        Jump();
        
    }

    protected virtual void Movement()
    {
        float inputX = Input.GetAxis("Horizontal");
        if(inputX != 0)
        {
            animator.SetBool(walkID, true);
            transform.Translate(transform.right * inputX * speed * Time.deltaTime);
        }
        else
        {
            animator.SetBool(walkID, false);
        }
        if (inputX < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            isRotated = true;

        }
        if (inputX > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            isRotated = false;
   

        }
    }

    protected virtual void Jump()
    {

        if (Input.GetButtonDown("Jump") && jump < maxJump)
        {
            audioSource.clip = jumpClip;
            audioSource.Play();
            rb.velocity = jumpDirection * jumpForce;
            jump++;
            isJumping = true;
            animator.SetBool(jumpID, isJumping);
        }
    }
    protected void IsGraunded()
    {       
        jump = 0;
        isJumping = false;
        animator.SetBool(jumpID, isJumping);
        HitFinish();
    }

    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, rayCastDistance, graund))
        {
            IsGraunded();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, rayCastDistance, graund))
        {
            IsGraunded();
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {

        ICollectionable collectionable = collision.GetComponent<ICollectionable>();
        if (collectionable != null)
        {
            collectionable.Collect();
        }
    }
    //scriptableobject
    protected virtual void SetPlayer() //sets all variables from the scriptableObject
    {
        speed = playerScriptableObject.speed;
        jumpForce = playerScriptableObject.jumpForce;
        rayCastDistance = playerScriptableObject.rayCastDistance;
        maxJump = playerScriptableObject.maxJump;
        jump = playerScriptableObject.jump;
        graund = playerScriptableObject.graund;

        jumpClip = playerScriptableObject.jumpClip;
        takeDamgeClip = playerScriptableObject.takeDamgeClip;
    }

    //draw line
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, Vector2.down * rayCastDistance);
    }

    //animation events
    public virtual void Knocked()
    {
        knocked = true;
        animator.SetBool(hitID, true);
        animator.SetBool(walkID, false);
    }
    public virtual void HitFinish()
    {
        knocked = false;
        animator.SetBool(hitID, false);
        GetComponent<SpriteRenderer>().enabled = true;
        OnHitFinish?.Invoke();
    }

    public void SlimeActive(float force)
    {
        rb.velocity = Vector2.up * force;
    }

    public void SlimeDamage(float DamageForceX,float DamageForceY)
    {
        audioSource.clip = takeDamgeClip;
        audioSource.Play();
        Knocked();
        rb.velocity = (isRotated) ? new Vector2(DamageForceX, DamageForceY) : new Vector2(-DamageForceX, DamageForceY);
        Debug.Log("Damage");
        OnTakeDamage?.Invoke();
    }

    public void FlyCollition()
    {
        IsGraunded();
    }

    public void InstaKillDamage()
    {
        audioSource.clip = takeDamgeClip;
        audioSource.Play();
        OnInstaDead?.Invoke();
    }
    public void Die()
    {
        Debug.Log("Dead");
        rb.velocity = Vector3.zero;
    }
    private void OnEnable()
    {
        HealthController.OnDead+=Die;
    }
    private void OnDisable()
    {
        HealthController.OnDead -= Die;
    }
       
}
