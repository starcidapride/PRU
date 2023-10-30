using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : Singleton<PlayerManager>
{
    private Rigidbody2D _rigidbody;
    private CapsuleCollider2D _collider;
    private Animator _animator;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float jumpHeight;

    [SerializeField]
    private float shootDelay;

    [SerializeField]
    private LayerMask _layer;

    [SerializeField]
    private Transform bullet;

    private AudioSource source; 

    [SerializeField]
    private AudioClip shootSound;
    public int Health
    {
        get;
        set;
    } = 5;

    public bool IsBackward { get; set; }
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();

        IsBackward = false;
    }

    private bool delay = false;

    void Update()
    {

        var _isGrounded = IsGrounded();

        _animator.SetBool("IsBackward", IsBackward);

        _animator.SetBool("IsGrounded", _isGrounded);

        var moveX = Input.GetAxisRaw("Horizontal");
        if (moveX > 0f)
        {
            IsBackward = false;
        }

        if (moveX < 0f)
        {
            IsBackward = true;
        }

        _animator.SetFloat("Speed", Mathf.Abs(moveX));
        _animator.SetFloat("Horizontal", moveX);

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rigidbody.velocity += new Vector2(0, jumpHeight);
        }

        if (Input.GetMouseButtonDown(0) && !delay)
        {
            StartCoroutine(ShootCoroutine());
        }

        _rigidbody.velocity = new Vector2(moveX * speed, _rigidbody.velocity.y);

    }

    private IEnumerator ShootCoroutine()
    {
        source.clip = shootSound;
        source.Play();

        var bl = Instantiate(bullet);
        bl.position = transform.position + new Vector3(
              IsBackward ? -200 : +200,
              200,
              0);

        delay = true;
        yield return new WaitForSeconds(shootDelay);
        delay = false;
    }

    private bool IsGrounded()
    {
        var rayCastHit2D = Physics2D.BoxCast(
            _collider.bounds.center,
            _collider.bounds.size,
            0f,
            Vector2.down * 0.1f,
            1f,
            _layer
            );
        return rayCastHit2D.collider != null;
    }

    public void Injured()
    {
        if (Health > 1)
        {
            Health -= 1;
            _animator.SetTrigger("Injured");
        } else
        {
            Health = 0;
            _animator.SetTrigger("Dead");
            gameObject.layer = LayerMask.NameToLayer("No Collision");
            SceneManager.LoadScene("GameOver");
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (Health > 0)
    //    {
    //        Health -= 1;
    //    }
    //    _animator.SetTrigger("Injured");
    //}
}
