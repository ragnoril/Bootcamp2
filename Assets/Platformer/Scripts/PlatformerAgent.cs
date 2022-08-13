using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerAgent : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public float moveSpeed;
    public float jumpSpeed;

    public Transform TouchGround;

    public bool isGrounded;

    public LayerMask GroundLayer;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapPoint(TouchGround.position, GroundLayer.value);
        if (isGrounded)
            animator.SetBool("isGround", true);


        float moveX = Input.GetAxis("Horizontal");

        if (moveX < 0f)
           spriteRenderer.flipX = true;
        else if(moveX > 0f)
            spriteRenderer.flipX = false;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            if (isGrounded)
            {
                rigidbody2D.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
                animator.SetBool("isGround", false);
            }
        }

        Vector2 newVel = new Vector2(moveX * moveSpeed * Time.fixedDeltaTime, rigidbody2D.velocity.y);
        rigidbody2D.velocity = newVel;

        if (rigidbody2D.velocity.x != 0f)
            animator.SetBool("isWalking", true);
        else
            animator.SetBool("isWalking", false);
    }

}
