using System.Collections;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GunnerManager : MonoBehaviour
{
    [SerializeField]
    private float gunnerSpeed;

    [SerializeField]
    private float shootingDistance;

    [SerializeField]
    private Transform projectile;

    [SerializeField]
    private float delay;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private CapsuleCollider2D _capsuleCollider;

    [SerializeField]
    private LayerMask _layerMask;

    private bool isBackward;
    private int Health { get; set; } = 2;

    private void Start()
    {   
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
    }
    private bool IsGrounded()
    {
        var rayCastHit2D = Physics2D.BoxCast(
            _capsuleCollider.bounds.center,
            _capsuleCollider.bounds.size,
        0f,
            Vector2.down * 0.1f,
            1f,
            _layerMask
            );
        return rayCastHit2D.collider != null;
    }

    private bool isDelay = false;
    private IEnumerator ShootCoroutine()
    {   
        if (!isDelay && IsGrounded())
        {
            var projectileObject = Instantiate(projectile);

            projectileObject.position = transform.position + new Vector3(
                isBackward ? 100 : - 100,
                100,
                0);
            
            isDelay = true;
            yield return new WaitForSeconds(delay);
            isDelay = false;
        }
    }

    private bool die = false;
    private void Update()
    {
        var playerLocation = PlayerManager.Instance.transform.position;
        var chomperLocation = transform.position;

        var distance = playerLocation - chomperLocation;
        var direction = distance.x > 0;

        float moveX;

        if (Mathf.Sqrt(distance.sqrMagnitude) < shootingDistance && !die)
        {
            moveX = 0;
            StartCoroutine(ShootCoroutine());
            
        } else
        {
            moveX = direction ? 1f : -1f;
        }

        isBackward = direction;

        _animator.SetFloat("Speed", Mathf.Abs(moveX));
        _animator.SetFloat("Horizontal", moveX);
        _animator.SetBool("IsBackward", isBackward);

        _rigidbody.velocity = new Vector2(moveX * gunnerSpeed, _rigidbody.velocity.y);
    }

    public void Injured()
    {
        StartCoroutine(InjuredCoroutine());   
    }

    public IEnumerator InjuredCoroutine()
    {
        if (Health > 1)
        {
            Health -= 1;
            Debug.Log(Health);
        }
        else
        {
            die = true;
            gameObject.layer = LayerMask.NameToLayer("No Collision");
            _animator.SetTrigger("Die");
            yield return new WaitForEndOfFrame();
            yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f);
            Destroy(gameObject);
        }
    }
}