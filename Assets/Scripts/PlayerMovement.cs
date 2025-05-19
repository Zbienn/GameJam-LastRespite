using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float _moveInput;
    private bool _jumpCommand;

    [SerializeField]
    private float _jumpPower = 5.0f;
    [SerializeField]
    private float _runSpeed = 5.0f;
    [SerializeField]
    private bool _isGrounded;
    [SerializeField]
    private GameObject _groundTestLineStart;
    [SerializeField]
    private GameObject _groundTestLineEnd;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Movimento horizontal (input cont�nuo)
        _moveInput = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            _moveInput = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _moveInput = 1f;
        }

        // Pulo
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _jumpCommand = true;
        }
    }

    private void FixedUpdate()
    {
        // Verificar se est� no ch�o
        _isGrounded = Physics2D.Linecast(
            _groundTestLineEnd.transform.position,
            _groundTestLineStart.transform.position
        );

        // Movimento horizontal controlado
        _rb.linearVelocity = new Vector2(_moveInput * _runSpeed, _rb.linearVelocity.y);

        // Pulo
        if (_jumpCommand)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _jumpPower);
            _jumpCommand = false;
        }

        // Virar personagem (direita/esquerda)
        if (_moveInput != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(_moveInput) * 5f; // 5f = sua escala original
            transform.localScale = scale;
        }
    }
}
