using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player_Movement1 : MonoBehaviour
{
    [Header("Components")]
    Rigidbody2D rb;
    Animator anim;
    public LayerMask groundLayer;
    public bool IsPlayer1 = true;
    public int PlayerHealth;
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI Coins;
    private int coins = 0;

    [Header("Horizontal Movement")]
    public float speed = 20f;
    float horizontalMove = 0f;
    float verticalMove = 0f;
    bool right = true;
    public Vector2 move;

    [Header("Jump")]
    public float jumpSpeed = 15f;
    public float jumpDelay = 0.25f;
    private float jumpTime;
    private bool jump = false;
    private bool jumpPressed = false;

    [Header("Physics")]
    public float maxHorizontalSpeed = 7f;
    public float linDrag = 4f;
    public float gravity = 1f;
    public float fallMultiplier = 5f;
    private bool gravityPower = false;

    [Header("Collision")]
    public bool onGround=false;
    public float groundLength = 0.6f;
    public Vector3 colliderOffset;

    public delegate void WhenPlayerDied();
    public static event WhenPlayerDied WhenPlayerDead;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        groundLayer = LayerMask.GetMask("Ground");
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }
    void MoveCharacter()
    {
        rb.AddForce(Vector2.right * move.x * speed);
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("InAir", !onGround);
        
        if (horizontalMove > 0 && !right || horizontalMove < 0 && right) 
        {
            Flip();
        }
        if (Mathf.Abs(rb.velocity.x) > maxHorizontalSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxHorizontalSpeed, rb.velocity.y);
        }
        if (jumpTime > Time.time && onGround)
        {
            Jump();
        }
        ModifyPhysics();
    }
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpSpeed,ForceMode2D.Impulse);
        jumpTime = 0;
    }
    void ModifyPhysics()
    {
        bool IsChangingDir = (move.x > 0 && rb.velocity.x < 0) || (move.x < 0 && rb.velocity.x > 0);
        if (onGround)
        {
            if (Mathf.Abs(horizontalMove) < 0.4 || IsChangingDir)
            {
                rb.drag = linDrag;
            }
            else
            {
                rb.drag = 0f;
            }
            if (!gravityPower)
            {
                rb.gravityScale = 0;
            }
        }
        else if (!gravityPower)
        {
            rb.gravityScale = gravity;
            rb.drag = linDrag * 0.15f;
            if (rb.velocity.y < 0)
            {
                rb.gravityScale = gravity * fallMultiplier;
            }
            else if (rb.velocity.y > 0 && !jumpPressed)
            {
                rb.gravityScale = gravity * (fallMultiplier / 1.5f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fire") && gameObject.CompareTag("Water"))
        {
            StartCoroutine("Hurting");
        }
        else if (collision.CompareTag("Water") && gameObject.CompareTag("Fire"))
        {
            StartCoroutine("Hurting");
        }
        else if (collision.CompareTag("Toxic"))
        {
            StartCoroutine("Hurting");
        }
        else if (collision.CompareTag("Bullet"))
        {
            StartCoroutine("Hurting");
        }
        else if (collision.CompareTag("Enemy"))
        {
            StartCoroutine("Hurting");
        }
        else if (collision.CompareTag("AntiGravity"))
        {
            StartCoroutine("AntiGravity");
            collision.gameObject.SetActive(false);
        }
        else if (collision.CompareTag("Speed"))
        {
            StartCoroutine("IncreaseSpeed");
            collision.gameObject.SetActive(false);
        }
        else if (collision.CompareTag("Jump"))
        {
            StartCoroutine("IncreaseJump");
            collision.gameObject.SetActive(false);
        }
        else if (collision.CompareTag("Health"))
        {
            if (PlayerHealth < 3) 
            {
                PlayerHealth++;
                collision.gameObject.SetActive(false);
            }
        }
        else if (collision.CompareTag("Time"))
        {
            StartCoroutine("StopEnemies");
            collision.gameObject.SetActive(false);
        }
        else if (((IsPlayer1)? collision.CompareTag("Red Coin"):collision.CompareTag("Blue Coin")))
        {
            coins++;
            GameManager.instance.IncreaseScore();
            collision.gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Moving Platform"))
        {
            gameObject.transform.SetParent(collision.gameObject.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Fire") && gameObject.CompareTag("Water"))
        {
            StopCoroutine("Hurting");
        }
        else if (collision.CompareTag("Water") && gameObject.CompareTag("Fire"))
        {
            StopCoroutine("Hurting");
        }
        else if (collision.CompareTag("Toxic"))
        {
            StopCoroutine("Hurting");
        }
        else if (collision.CompareTag("Bullet"))
        {
            StopCoroutine("Hurting");
        }
        else if (collision.CompareTag("Enemy"))
        {
            StopCoroutine("Hurting");
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Moving Platform"))
        {
            gameObject.transform.SetParent(null); 
        }
    }

    IEnumerator AntiGravity()
    {
        yield return null;
        gravityPower = true;
        rb.gravityScale = -1;
        yield return new WaitForSeconds(5f);
        gravityPower = false;
    }

    IEnumerator IncreaseSpeed()
    {
        yield return null;
        speed *= 2;
        maxHorizontalSpeed *= 2;
        yield return new WaitForSeconds(5f);
        speed /= 2;
        maxHorizontalSpeed /= 2;
    }

    IEnumerator IncreaseJump()
    {
        yield return null;
        jumpSpeed *= 2;
        fallMultiplier /= 2;
        yield return new WaitForSeconds(5f);
        jumpSpeed /= 2;
        fallMultiplier *= 2;
    }

    IEnumerator StopEnemies()
    {
        yield return null;
        //GameObject[] Enemy = GameObject.FindGameObjectsWithTag("Enemy");
        EnemyScript[] Enemies = FindObjectsOfType<EnemyScript>();
        Debug.Log("Size:" + Enemies.Length);
        for (int i = 0; i < Enemies.Length; i++)
        {
            Enemies[i].ShouldMove = false;
        }
        yield return new WaitForSeconds(5f);
        for (int i = 0; i < Enemies.Length; i++)
        {
            Enemies[i].ShouldMove = true; 
        }
    }

    IEnumerator Hurting()
    {
        yield return null;
        Debug.Log("Hurt");
        while (true)
        {
            ReduceHealth();
            yield return new WaitForSeconds(1.0f);
        }
    }

    void ReduceHealth()
    {
        if (PlayerHealth > 0)
        {
            PlayerHealth--;
            anim.Play("Hurt");
            if (PlayerHealth <= 0)
            {
                StopCoroutine("Hurting");
                WhenPlayerDead();
            }
        }
    }

    void Update()
    {
        if (IsPlayer1)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal");
            verticalMove = Input.GetAxisRaw("Vertical");
            jump = Input.GetButtonDown("Jump");
            jumpPressed = Input.GetButton("Jump");
        }
        else
        {
            horizontalMove = Input.GetAxisRaw("Horizontal1");
            verticalMove = Input.GetAxisRaw("Vertical");
            jump = Input.GetButtonDown("Jump1");
            jumpPressed = Input.GetButton("Jump1");
        }
        onGround = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLength, groundLayer) || Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLength, groundLayer);
        HealthText.text = ((IsPlayer1) ? "Red Health is " : "Blue Health is ") + PlayerHealth;
        Coins.text = "Coins = " + coins;
        move = new Vector2(horizontalMove, verticalMove);
        if (jump)
        {
            jumpTime = Time.time + jumpDelay;
        }
    }
    void Flip()
    {
        right = !right;
        transform.rotation =Quaternion.Euler(0, right ? 0 : 180, 0); 
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * groundLength);
        Gizmos.DrawLine(transform.position - colliderOffset, transform.position - colliderOffset + Vector3.down * groundLength);
    }
}
