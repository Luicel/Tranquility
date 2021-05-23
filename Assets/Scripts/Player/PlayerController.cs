using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour {
    private bool isGrounded;

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float movementSmoothing = 0.05f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;

    private new Rigidbody2D rigidbody2D;
    private float groundedRadius = 0.2f;
    private Vector3 velocity = Vector2.zero;

    private void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        isGrounded = false;

        // Check is player is touching Ground layer and set isGrounded to true if true
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++) {
            if (colliders[i].gameObject != gameObject) {
                isGrounded = true;
            }
        }
        PlayerAnimations.isGrounded = isGrounded;
    }

    public void Move(float move, bool jump) {
        Vector2 targetVelocity = new Vector2(move * 10f * speed * Time.deltaTime, rigidbody2D.velocity.y);
        rigidbody2D.velocity = Vector3.SmoothDamp(rigidbody2D.velocity, targetVelocity, ref velocity, movementSmoothing);

        if (isGrounded && jump)
            Jump();

        UpdateSpriteDirection(move);
        UpdateAnimations(move, jump);

        // Physics logic preventing the player from sliding down slopes
        if (isGrounded && move == 0)
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        else
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Jump() {
        isGrounded = false;
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
        rigidbody2D.AddForce(new Vector2(0, jumpForce));
    }

    private void UpdateSpriteDirection(float move) {
        if (move > 0 && !PlayerAnimations.isFacingRight)
            Flip();
        else if (move < 0 && PlayerAnimations.isFacingRight)
            Flip();
    }

    private void Flip() {
        PlayerAnimations.isFacingRight = !PlayerAnimations.isFacingRight;

        Vector2 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    private void UpdateAnimations(float move, bool jump) {
        PlayerAnimations.horizontalMovement = move;
        PlayerAnimations.isJumping = jump;
    }
}
