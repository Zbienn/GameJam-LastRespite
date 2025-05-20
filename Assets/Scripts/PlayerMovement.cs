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


    private int _jumpCount = 0;
    [SerializeField]
    private int _maxJumps = 2;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _moveInput = 0f;

        if (Input.GetKey(KeyCode.A)) _moveInput = -1f;
        else if (Input.GetKey(KeyCode.D)) _moveInput = 1f;

        if (Input.GetKeyDown(KeyCode.Space) && _jumpCount < _maxJumps)  _jumpCommand = true;
    }

    private void FixedUpdate()
    {
       
        _isGrounded = Physics2D.Linecast(
            _groundTestLineEnd.transform.position,
            _groundTestLineStart.transform.position
        );

        if (_isGrounded)
        {
            _jumpCount = 0;
        }

        _rb.linearVelocity = new Vector2(_moveInput * _runSpeed, _rb.linearVelocity.y);

        if (_jumpCommand)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _jumpPower);
            _jumpCommand = false;
            _jumpCount++;
        }

        if (_moveInput != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(_moveInput) * Mathf.Abs(scale.x); 
            transform.localScale = scale;
        }
    }
}
