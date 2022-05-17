using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    protected Rigidbody2D rb;

    [Header("Movement")]
    [SerializeField] protected float speed;

    [Header("Jump")]
    [SerializeField] protected float jumpForce;
    [SerializeField] protected float rayCastDistance;
    [SerializeField] protected int maxJump;
    [SerializeField] protected int jump;
    [SerializeField] protected LayerMask graund;


    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected void Update()
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

        }
        if (inputX > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);

        }
    }

    protected void Jump()
    {

        if (Input.GetButtonDown("Jump") && jump < maxJump)
        {
            Vector2 vel = rb.velocity;
            vel.y = 0;
            rb.velocity = vel;
            //transform.Translate(Vector3.up * jumpForce * Time.deltaTime);
            rb.AddRelativeForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jump++;
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, rayCastDistance, graund))
        {
            jump = 0;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, Vector2.down * rayCastDistance);
    }
}
