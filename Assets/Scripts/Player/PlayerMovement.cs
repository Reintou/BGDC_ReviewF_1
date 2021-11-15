using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [HideInInspector]
    public SpriteRenderer sr;
    private Camera cam;
    private PlayerCollision collCheck;
    private Vector2 movementDirection;

    public DashTrail GhostEffect;

    [Space]

    [Header("Stats")]
    [SerializeField]
    private float movementSpeed = 4f;
    [SerializeField]
    private float dashSpeed = 3f;
    [SerializeField]
    private float jumpForce = 2f;

    [Space]

    [Header("Checks")]
    private bool hasDashed = false;
    private bool isDashing = false;
    private bool groundTouch = false;
    [HideInInspector]
    public bool canMove = true;

    [Space]

    [Header("Particle")]
    public ParticleSystem jumpParticle;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collCheck = GetComponent<PlayerCollision>();
        sr = GetComponent<SpriteRenderer>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        var horizontalMove = Input.GetAxisRaw("Horizontal");
        var verticalMove = Input.GetAxisRaw("Vertical");
        movementDirection = new Vector2(horizontalMove, verticalMove);
        movementDirection.Normalize();

        if(Input.GetKeyDown(KeyCode.Z) && collCheck.onGround)
        {
            PlayerJump();
        }

        if(Input.GetKeyDown(KeyCode.X) && !hasDashed)
        {
            if(movementDirection.x != 0 || movementDirection.y != 0)
            {
                Dash();
            }
        }

        if(collCheck.onGround && !groundTouch)
        {
            TouchGround();
            groundTouch = true;
        }

        if (!collCheck.onGround && groundTouch)
        {
            groundTouch = false;
        }

        if (movementDirection.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            jumpParticle.transform.localScale = new Vector3(1f, 1f, 1f);
            collCheck.bottomOffset = collCheck.posBottomOffset;
        }
        else if (movementDirection.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            jumpParticle.transform.localScale = new Vector3(-1f, 1f, 1f);
            collCheck.bottomOffset = collCheck.negBottomOffset;
        }
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    void Dash()
    {
        cam.transform.DOComplete();
        cam.transform.DOShakePosition(.2f, .4f, 6, 90, false, true);
        hasDashed = true;

        rb.velocity = Vector2.zero;
        rb.velocity += movementDirection * dashSpeed;
        StartCoroutine(DashWait());
    }

    void TouchGround()
    {
        hasDashed = false;
        isDashing = false;

        jumpParticle.Play();
    }

    IEnumerator DashWait()
    {
        GhostEffect.StartGhost();
        StartCoroutine(CheckGroundDash());
        DOVirtual.Float(12f, 0f, .8f, RigidbodyDrag);

        rb.gravityScale = 0f;
        isDashing = true;

        yield return new WaitForSeconds(.3f);

        rb.gravityScale = 3f;
        isDashing = false;
    }

    IEnumerator CheckGroundDash()
    {
        yield return new WaitForSeconds(.15f);
        if (collCheck.onGround)
        {
            yield return new WaitForSeconds(.15f);
            hasDashed = false;
        }
    }

    void RigidbodyDrag(float x)
    {
        rb.drag = x;
    }

    void PlayerMove()
    {
        if(isDashing)
            return;

        if(!canMove)
            return;

        rb.velocity = new Vector2(movementDirection.x * movementSpeed, rb.velocity.y);
    }

    void PlayerJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.velocity += new Vector2(movementDirection.x, 1f) * jumpForce;

        jumpParticle.Play();
    }
}
