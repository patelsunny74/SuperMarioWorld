using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Transform groundCheck;
    public LayerMask isGroundLayer;
    public Animator animator;
    public Rigidbody2D rigidbody;
    
    public Transform projectileSpawn;
    public GameObject projectilePrefab;
    public float projectileSpeed;


    public float speed = 1.0f;
    public int jumpForce = 3;
    public float groundCheckRadius = 0.2f;

    private bool isRight = true;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        if(jumpForce <=0)
        {
            jumpForce = 2;
        }
        if(speed <= 0)
        {
            speed = 1;
        }
        if(groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.2f;
        }
        if(!rigidbody)
        {
            GetComponent<Rigidbody2D>();
        }
        if(!animator)
        {
            GetComponent<Animator>();
        }

    }

    // Update is called once per frame
    void Update()

    {
        float moveValue = Input.GetAxisRaw("Horizontal");
        Vector2 xMovement = new Vector2(moveValue * speed, rigidbody.velocity.y);
        //transform.Translate(Input.GetAxis("Horizontal") , 0 , 0 );
        Debug.Log(Input.GetAxisRaw("Horizontal"));
        rigidbody.velocity = xMovement;
        animator.SetFloat("Speed", Mathf.Abs(moveValue));

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);

        if(!isGrounded)
        {
            animator.SetBool("isJumping", false);
        }
        else
        {
            animator.SetBool("isJumping", true);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Debug.Log("Jump");
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            if (Input.GetButtonDown("Fire1"))
            {
                fire();
            }
        }

       if (Input.GetButtonDown("Fire1"))
        {
            fire();
        } 

        if (moveValue < 0 && isRight || moveValue > 0 && !isRight)
        {
            flip();
        }
        
    }

    void fire()
    {
        animator.SetTrigger("isAttacking");
        GameObject tempProjectile = Instantiate(projectilePrefab, projectileSpawn.position,projectileSpawn.rotation);
        tempProjectile.GetComponent<Projectile>().speed = projectileSpeed;
        animator.SetTrigger("isAttacking");
    }
    void flip()
    {
        isRight = !isRight;
        Vector3 scaleValue = transform.localScale;
        scaleValue.x = -scaleValue.x;
        //scaleValue.x *= -1;
        transform.localScale = scaleValue;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Collectible")
        {
            Debug.Log("Collected");
            Destroy(collision.gameObject);
        }
    }
}
