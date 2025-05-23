using UnityEngine;

public class EnemyWalker : MonoBehaviour
{
    [Header("Movimento")]
    [SerializeField] private float speed = 2f;
    private bool movingRight = true;

    [Header("Verificação de chão")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance = 0.2f;

    [Header("Verificação de parede")]
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance = 0.1f;

    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Mover();
        VerificarChao();
        VerificarParede();
    }

    void Mover()
    {
        float direction = movingRight ? 1f : -1f;
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
    }

    void VerificarChao()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
        if (hit.collider == null)
        {
            Flip();
        }
    }

    void VerificarParede()
    {
        Vector2 direction = movingRight ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(wallCheck.position, direction, wallCheckDistance, groundLayer);
        if (hit.collider != null)
        {
            Flip();
        }
    }

    void Flip()
    {
        movingRight = !movingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
        }

        if (wallCheck != null)
        {
            Gizmos.color = Color.red;
            Vector3 dir = movingRight ? Vector3.right : Vector3.left;
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + dir * wallCheckDistance);
        }
    }

    // Aqui entraria o código de dano ao jogador
    // void OnCollisionEnter2D(Collision2D collision) { ... }
}
