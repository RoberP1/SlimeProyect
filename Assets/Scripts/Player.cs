using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    protected Rigidbody2D rb;

    [Header("Movement")]
    [SerializeField] protected float speed;
    protected bool isRotated;

    [Header("Jump")]
    [SerializeField] protected float jumpForce;
    [SerializeField] protected float rayCastDistance;
    [SerializeField] protected int maxJump;
    [SerializeField] protected int jump;
    [SerializeField] protected Vector2 jumpDirection = new Vector2(0,1);
    [SerializeField] protected LayerMask graund;
    [SerializeField]protected bool isJumping;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        Movement();
        Jump();
    }

    protected virtual void Movement()
    {
        float inputX = Input.GetAxis("Horizontal");

        transform.Translate(transform.right * inputX * speed * Time.deltaTime);
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
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, rayCastDistance, graund))
        {
            jump = 0;
            isJumping = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, Vector2.down * rayCastDistance);
    }
}
