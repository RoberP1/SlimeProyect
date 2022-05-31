using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
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


    protected virtual void Start()
    {
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
            
            rb.velocity = jumpDirection * jumpForce;
            jump++;
            isJumping = true;
            animator.SetBool(jumpID, isJumping);
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        

        if (collision.gameObject.CompareTag("Pinchos"))
        {
            // Ta Mal escrito, pero no se si puedo cambiarlo sin que pase algo malo en el git
            SceneManager.LoadScene("LevelDisgn");
        }
    }
    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        IsGraunded();
    }

    private void IsGraunded()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, rayCastDistance, graund))
        {
            jump = 0;
            isJumping = false;
            animator.SetBool(jumpID, isJumping);
            HitFinish();
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
    }
}
