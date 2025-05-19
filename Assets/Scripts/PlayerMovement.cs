using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private bool _jumpCommand;
    private bool _leftCommand;
    private bool _rightCommand;

    [SerializeField]
    private float _jumpPower = 2.0f;
    [SerializeField]
    private float _runSpeed = 0.5f;
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
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _jumpCommand = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _leftCommand = true;
       
        } else if (Input.GetKey(KeyCode.D)) {
            _rightCommand = true;
           
        }

    }
    private void FixedUpdate()
    {
        _isGrounded = Physics2D.Linecast(_groundTestLineEnd.transform.position,
                                        _groundTestLineStart.transform.position);

        if (_jumpCommand) 
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _jumpPower);
            _jumpCommand = false;
        }
        if (_leftCommand)
        {
            _rb.linearVelocity = new Vector2(-_runSpeed, _rb.linearVelocity.y);
            _leftCommand = false;
            Vector3 scale = transform.localScale;
            scale.x = -5;
            transform.localScale = scale;
        }
        else if (_rightCommand)
        {
            _rb.linearVelocity = new Vector2(_runSpeed, _rb.linearVelocity.y);
            _rightCommand = false;
            Vector3 scale = transform.localScale;
            scale.x = 5;
            transform.localScale = scale;
        }
    }

      
}
    