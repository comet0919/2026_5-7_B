using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float JumpForce = 5f;
    public Transform groundcheck;
    public LayerMask grounddLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float moveInput;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Respawn"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if(collision.CompareTag("Finish"))
        {
            collision.GetComponent<LevelObject>().MoveToNextLevel();
        }
        if(collision.CompareTag("Enemy"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        if (moveInput < 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput > 0)
            transform.localScale = new Vector3(-1, 1, 1);
        isGrounded = Physics2D.OverlapCircle(groundcheck.position, 0.2f, grounddLayer);
    }
    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        moveInput = input.x;
    }
    public void OnJump(InputValue value)
    {
        if(value.isPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }
    }
}
