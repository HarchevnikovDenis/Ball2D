using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class BallMovement : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public float speed;             //Movement speed
    private bool isGrounded;
    public Transform groundCheck;   
    public LayerMask whatIsGround;
    private float jumpForce = 15.0f;    
    private Animator animator;
    private bool isAlive = true;
    [SerializeField] private GameObject spriteMask;
    private SceneTransitions sceneTransitions;
    [SerializeField] private Animator cameraAnimator;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sceneTransitions = FindObjectOfType<SceneTransitions>().GetComponent<SceneTransitions>();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, whatIsGround);

        float xMove = Input.GetAxis("Horizontal");
        rb2D.velocity = new Vector2(xMove * speed, rb2D.velocity.y);
    }

    private void Update()
    {
        if (!isGrounded)
            return;

        if(Input.GetAxisRaw("Vertical") > 0  || Input.GetAxis("Jump") != 0)
        {
            rb2D.velocity = Vector2.up * jumpForce;
            isGrounded = false;
            animator.SetTrigger("Jump");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!isAlive)
            return;

        if (other.gameObject.CompareTag("Spikes"))
        {
            rb2D.velocity = new Vector2(0, 15.0f);
            animator.SetTrigger("Death");
            cameraAnimator.SetTrigger("isDead");
            isAlive = false;
            spriteMask.transform.localScale *= 25;
            sceneTransitions.FadeTo(SceneManager.GetActiveScene().buildIndex);
            this.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Finish"))
        {
            int index = SceneManager.GetActiveScene().buildIndex;
            index++;
            sceneTransitions.FadeTo(index);
        }
    }
}
