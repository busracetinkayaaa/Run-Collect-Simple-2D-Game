using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2 : MonoBehaviour
{
    public float jumpForce = 2.0f;
    public float speed = 1.0f;

    private bool jump;
    private bool grounded = true;
    private Rigidbody2D _rigidbody2D;
    private Animator anim;
    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector2(speed * Input.GetAxis("Horizontal"), _rigidbody2D.velocity.y);

        if (jump == true)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpForce);
            jump = false;
        }
    }

    void Update()
    {
        if (grounded == true)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                jump = true;
                grounded = false;
                anim.SetTrigger("jump");
                anim.SetBool("grounded", false);
            }
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            _spriteRenderer.flipX = true;
            anim.SetFloat("speed", speed);
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            _spriteRenderer.flipX = false;
            anim.SetFloat("speed", speed);
        }
        else
        {
            anim.SetFloat("speed", 0.0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("zemin"))
        {
            anim.SetBool("grounded", true);
            grounded = true;
        }
    }
}
