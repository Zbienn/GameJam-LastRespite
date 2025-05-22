using UnityEngine;
using TMPro;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float _moveInput;
    private bool _jumpCommand;
    private Animator _animator;

    [Header("Run Section")]
    [SerializeField]
    private float _runSpeed = 5.0f;

    [Header("Ground Section")]
    [SerializeField]
    private bool _isGrounded;
    [SerializeField]
    private GameObject _groundTestLineStart;
    [SerializeField]
    private GameObject _groundTestLineEnd;

    [Header("Campfire Section")]
    [SerializeField]
    private GameObject _campfirePoint;
    [SerializeField]
    private GameObject _campfire;
    [SerializeField]
    private TextMeshProUGUI _campfireText;


    [Header("Jump Section")]
    [SerializeField] private float _jumpPower = 5.0f;
    private int _jumpCount = 0;
    [SerializeField]
    private int _maxJumps = 2;

    [Header("Audio Section")]
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _campSpawnSound;
    [SerializeField] private AudioClip _campBreakSound;
    private AudioSource _audioSource;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        _moveInput = 0f;


        if (Input.GetKey(KeyCode.A)) _moveInput = -1f;
        else if (Input.GetKey(KeyCode.D)) _moveInput = 1f;

        if (_animator != null)
        {
            _animator.SetBool("isRunning", _moveInput != 0f);
        }

        if (Input.GetKeyDown(KeyCode.Space) && _jumpCount < _maxJumps)  _jumpCommand = true;
    }

    private void FixedUpdate()
    {
       
        _isGrounded = Physics2D.Linecast(
            _groundTestLineEnd.transform.position,
            _groundTestLineStart.transform.position
        );

        if (_isGrounded) _jumpCount = 0;

        if (Input.GetKey(KeyCode.C) && _isGrounded)
        {
            if (!_campfire.activeSelf && _campfireText.text != "0")
            {
                if (_campSpawnSound != null && _audioSource != null) _audioSource.PlayOneShot(_campSpawnSound);
                _campfire.transform.position = _campfirePoint.transform.position;
                _campfire.SetActive(true);
                _campfireText.text = "0";
            }
        }

        if (_animator != null)
        {
            _animator.SetBool("isJumping", !_isGrounded);
        }

        _rb.linearVelocity = new Vector2(_moveInput * _runSpeed, _rb.linearVelocity.y);

        if (_jumpCommand)
        {
            if (_jumpSound != null && _audioSource != null)  _audioSource.PlayOneShot(_jumpSound);
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.R))
        {
            if (_campBreakSound != null && _audioSource != null) _audioSource.PlayOneShot(_campBreakSound);
            _campfire.SetActive(false);
        }
    }
}
